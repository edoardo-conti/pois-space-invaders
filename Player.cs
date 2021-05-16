/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace SpaceInvaders
{
    public class Player
    {
        // istanza classe principale
        private GameView _view;
        private GameController _controller;
        // classe windows form per gestire lo sprite
        private PictureBox entity;
        // parametro funzionale dipendente dal timer
        private static int FramesToSkip = 2;
        // velocità di spostamento dipendenti dalla tipologia d'input
        private static int SpeedModeOne = 15;
        private static int SpeedModeTwo = 12;
        // flag per la gestione dei limiti di movimento
        private bool Stuck;
        // velocità
        private int Speed = 0;
        // punteggio
        private int score = 0;
        // vite a disposizione
        private int lives = 3;

        public PictureBox Entity { get => entity; set => entity = value; }
        public int Score { get => score; set => score = value; }
        public int Lives { get => lives; set => lives = value; }

        public Player(GameController controller, GameView view)
        {
            // assegnazioni standard
            _view = view;
            _controller = controller;

            // generazione sprite tramite View
            entity = _view.SpritePlayer();
        }

        public void Next()
        {
            /*
            * senza tale limitazione si va incontro ad un uso eccessivo delle risorse
            * della macchina host e troppe occorrenze di controlli non necessari.
            */
            if (_controller.Steps % FramesToSkip == 0 &&
                _controller.PlayerMoveMode == GameController.PMD.Continuo)
            {
                /*
                 * Movimento Continuo Lineare [ONLY]
                 * il movimento del player viene confermato solamente dopo 
                 * aver verificato di rientrare nei limiti della finestra
                 */
                if(entity.Left <= _view.ClientRectangle.Width * 0.88 && 
                   entity.Left >= _view.ClientRectangle.Width * 0.04)
                {
                    /*
                     * la posizione del player viene calcolata ed aggiornata 
                     * secondo velocità e direzione di quest'ultimo.
                     */
                    this.entity.Left += Speed * SpeedModeTwo;
                } else
                {
                    // flag attivo => player si trova in una "posizione di stallo"
                    Stuck = true;
                }
            }
        }

        public void Move(Keys direction, GameController.PMD mode)
        {
            // comportamento differente in base alle 2 casistiche 
            switch (direction.ToString())
            {
                case "Right":
                    // modalità a pressione singola
                    if (mode == GameController.PMD.Push &&
                        entity.Left < _view.ClientRectangle.Width * 0.88)
                    {
                        // aggiornamento posizione player
                        entity.Left += SpeedModeOne;
                    } else {
                        // modalità a pressione continua
                        if (GetDirection() != "R")
                        {
                            /*
                             * assegnamento che definisce la direzione del player:
                             * 1 => movimento verso destra
                             * 2 => "_____________" sinistra
                             */
                            this.Speed = 1;
                            if (Stuck)
                            {
                                // aggiornamento posizione player in caso di "stallo"
                                this.entity.Left += SpeedModeTwo;
                            }
                        }
                    }
                    break;
                case "Left":
                    // modalità a pressione singola
                    if (mode == GameController.PMD.Push &&
                        this.entity.Left > _view.ClientRectangle.Width * 0.04)
                    {
                        // aggiornamento posizione player
                        this.entity.Left -= SpeedModeOne;
                    } else {
                        // modalità a pressione continua
                        if (GetDirection() != "L")
                        {
                            // commento analogo linee [96-100]
                            this.Speed = -1;
                            if (Stuck)
                            {
                                this.entity.Left -= SpeedModeTwo;
                            }
                        }
                    }
                    break;
            }
        }

        /* 
         * metodo utile per ricavare la direzione del player.
         * Si valuta il valore Speed il quale se:
         * positivo => direzione destra
         * negativo => direzione sinistra
         */
        public string GetDirection()
        {
            string d;

            if (this.Speed < 0)
            {
                d = "L";
            } else if(this.Speed > 0)
            {
                d = "R";
            } else
            {
                d = "U";
            }

            return d;
        }

        // Metodo destinato alla gestine dello sparo di un bullet da parte del Player
        public void Shoot(System.Windows.Forms.Timer PlayerTimerShots)
        {
            // verifico che il player possa sparare un nuovo colpo
            if (_controller.PlayerCanShoot)
            {
                // calcolo le coordinate dove spawnare il nuovo bullet
                int coordx = entity.Left + (entity.Bounds.Width / 2),
                    coordy = entity.Top - (entity.Bounds.Height / 2);

                // instazio un oggetto della classe interessata il quale si occuperà dello spawn
                Bullet bullet = new Bullet(_controller, _view, new Point(coordx, coordy), 15, Bullet.Shooter.Player);
                _controller.Bullets.Add(bullet);

                // abbasso il flag (false) che tornerà attivo allo scadere del timer avviato
                _controller.PlayerCanShoot = false;
                PlayerTimerShots.Start();
            }
        }

        // Metodo invocato quando il player collide con un bullet nemico Invader/SpaceShip
        public void Hit()
        {
            // riduzione numero vite
            Lives--;

            // demando alla classe Activity la rimozione di una vita (immagine alto-dx)
            _view.RemoveLife(Lives);

            // alterno sprite (simulazione effetto colpito)
            entity.Image = Properties.Resources.shot;
            entity.Refresh();
            Thread.Sleep(150);
            entity.Image = Properties.Resources.player;

            // riposizionamento Player in mezzo alla lane
            entity.Left = (_view.ClientRectangle.Width / 2) - (entity.Size.Width / 2);

            // attesa
            Thread.Sleep(250);
        }

        // Metodo utilizzato per la rilevazione di collisione con bullets
        public bool Collide(Bullet bullet)
        {
            return entity.Bounds.IntersectsWith(bullet.Entity.Bounds);
        }
    }
}
