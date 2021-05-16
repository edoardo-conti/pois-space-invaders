/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders
{
    public class GameController
    {
        // definizione metodo d'input per spostamento del Player
        public enum PMD
        {
            Continuo,
            Push,
            Undefined
        }
        // Passi => +1 ad ogni tick di timer (20s)
        public int Steps = 0;
        // margin e padding per lo spawn iniziale degli invaders
        public int margin = 30;
        public int padding = 35;
        // classe necessaria per prelevare randomicamente un invaders dal pool
        public Random Random = new Random();
        // classe Player
        public Player Player;
        // clase Invaders finale, SpaceShip
        public Ship SpaceShip;
        // lista degli invaders in campo
        public List<Invader> Invaders = new List<Invader>();
        // lista dei bullet (spari) in campo
        public List<Bullet> Bullets = new List<Bullet>();
        // metodo d'input per movimento PLayer
        public PMD PlayerMoveMode = PMD.Undefined;
        // flag => definisce la possibilità o meno di sparare del Player
        public bool PlayerCanShoot = true;
        // flag => definisce in quale dei 2 livelli si trova il Player
        public bool LastLevel = false;
        // contatore tentativi di gioco
        private int games = 0;
        // classe destinata alla gestione dell'UI
        GameView _view;

        public int Games { get => games; set => games = value; }

        // costruttore 
        public GameController(GameView view)
        {
            // assegnazione classica
            _view = view;
        }

        // Metodo per lo spawn di sprites Invaders e Player nel campo di gioco
        public void SpawnSprites()
        {
            int posx = 0;
            int posy = 0;

            // incremento tentativo di gioco
            Games++;

            // Init Giocatore
            Player = new Player(this, _view);

            // Init Invaders
            for (int i = 0; i < 55; i++)
            {
                if (i < 11)
                {
                    posx = (padding * i) + margin;
                    posy = 100;
                    Invaders.Add(new Squid(this, _view, new Point(posx, posy)));
                }
                else if (i >= 11 && i < 22)
                {
                    posx = (padding * (i - 11)) + margin;
                    posy = 140;
                    Invaders.Add(new Crab(this, _view, new Point(posx, posy)));
                }
                else if (i >= 22 && i < 33)
                {
                    posx = (padding * (i - 22)) + margin;
                    posy = 180;
                    Invaders.Add(new Crab(this, _view, new Point(posx, posy)));
                }
                else if (i >= 33 && i < 44)
                {
                    posx = (padding * (i - 33)) + margin;
                    posy = 220;
                    Invaders.Add(new Octopus(this, _view, new Point(posx, posy)));
                }
                else
                {
                    posx = (padding * (i - 44)) + margin;
                    posy = 260;
                    Invaders.Add(new Octopus(this, _view, new Point(posx, posy)));
                }
            }

            // aggiornamento UI
            _view.UIAfterSpawn();
        }

        // Gestisce il game frame-by-frame dipendendo dal tick del timer di 20ms
        public void GameForward()
        {
            // incremento passi ad ogni tick del timer (20ms)
            Steps++;

            // ogni 1.5s (=> [75*20]/1000) invader scelto casualmente spara un colpo verso il player
            if (Steps % 75 == 0)
            {
                // seleziono invader casualmente tra i rimasti in gioco
                int CountInvaders = Invaders.Count();
                if (CountInvaders > 0)
                {
                    Invader RI = Invaders[Random.Next(CountInvaders)];

                    // ricavo coordinate invader selezionato
                    int coordx = RI.Entity.Left + (RI.Entity.Bounds.Width / 2),
                        coordy = RI.Entity.Top - (RI.Entity.Bounds.Height / 2);

                    // creo istanza bullet e la aggiungo alla lista
                    Bullet bullet = new Bullet(this, _view, new Point(coordx, coordy), 10, Bullet.Shooter.Invader);
                    Bullets.Add(bullet);
                }
            }

            // comportamento del player in funzione del tick del timer
            Player.Next();

            // comportamento degli invaders in funzione del timer
            var InvadersFixed = Invaders.ToList();
            // verifico che la flotta non collida con i bordi della finestra
            if (CheckBorderCollision(InvadersFixed))
            {
                // se così fosse avanzano verso il basso e si inverte rotta
                foreach (var invader in InvadersFixed)
                {
                    invader.Entity.Top += 50;
                    invader.Speed *= -1;
                    invader.Entity.Left += invader.Speed;
                }
            }
            else
            {
                // si prosegue con il comportamento standard in funzione del tick del timer
                InvadersFixed.ForEach(invader => invader.Next());
            }

            // comportamento dei bullets (spari) in funzione del tick del timer
            var BulletsFixed = Bullets.ToList();
            BulletsFixed.ForEach(bullet => bullet.Next());

            // comportamento dell'invader finale (Ship) in funzione del tick del timer
            if (LastLevel)
            {
                SpaceShip.Next();

                // ogni 1s (=> [50*20]/1000) invader finale (Ship) spara un colpo verso il player
                if (Steps % 50 == 0)
                {
                    // ricavo coordinate SpaceShip
                    int coordx = SpaceShip.Entity.Left + (SpaceShip.Entity.Bounds.Width / 2),
                        coordy = (SpaceShip.Entity.Top - (SpaceShip.Entity.Bounds.Height / 2)) + 30;

                    // creo istanza bullet e la aggiungo alla lista
                    Bullet bullet = new Bullet(this, _view, new Point(coordx, coordy), 12, Bullet.Shooter.Ship);
                    Bullets.Add(bullet);
                }
            }

        }

        // Metodo addetto alla rilevazione di collisioni della flotta con i limiti della finestra
        private bool CheckBorderCollision(List<Invader> invaders)
        {
            bool status = false;

            foreach (var invader in invaders)
            {
                // se anche un solo invader si trova al di fuori dell'area consensita 
                // si alza il flag booleano di collisione
                if (invader.Entity.Left < _view.ClientRectangle.Width * 0.05 ||
                    invader.Entity.Left > _view.ClientRectangle.Width * 0.9)
                {
                    status = true;
                }
            }

            return status;
        }

        // Definisce il passaggio al livello Finale con SpaceShip
        public void NextLevel()
        {
            // attivazione flag
            LastLevel = true;

            // aggiunta invader finale: SpaceShip
            int CoordX = (_view.ClientRectangle.Width / 2) - 22;
            int CoordY = (int)(_view.ClientRectangle.Height * 0.2);
            SpaceShip = new Ship(this, _view, new Point(CoordX, CoordY));

            // aggiornamento UI
            _view.UISpaceShip();
        }

        /*
         * Metodo destinato alla rimozione di tutti gli invaders meno 1
         * Tale funzione è destinata a procedure di debugging, permette di accedere 
         * velocemente al secondo livello del gioco. TODO
         */
        public void KillInvaders()
        {
            int InvCounts = 0;
            var InvadersFixed = Invaders.ToList();
            InvadersFixed.ForEach(invader =>
            {
                if (InvCounts == InvadersFixed.Count - 1)
                {
                    return;
                }
                _view.Controls.Remove(invader.Entity);
                Invaders.Remove(invader);
                InvCounts++;
            });
        }

        // Metodo da richiamare quando il player richiede una nuova partita (N)
        public void RestoreGameWindow()
        {
            // aggiornamento UI
            _view.RestoreGameWindowUI();

            // abbasso flag livello SpaceShip
            LastLevel = false;

            // ripristio numero vite player
            Player.Lives = 3;

            // spawn invaders, player...
            SpawnSprites();
        }

        // Listener del bottone principale per avviare il game
        public void StartButtonClicked(Boolean mode)
        {
            // impostazione modalità movimento Player
            if (mode)
            {
                PlayerMoveMode = PMD.Push;
            }
            else
            {
                PlayerMoveMode = PMD.Continuo;
            }

            // inizio spawn delle entità...
            SpawnSprites();
        }

    }
}
