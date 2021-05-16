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
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class GameView : Form
    {
        // barra della vita dell'invader finale (SpaceShip)
        public ProgressBar ShipLife;
        // testo Game Over e descrizione
        Label Alert = new Label();
        Label AlertDesc = new Label();
        // linea di fine campo sottostante al Player
        public PictureBox Line = new PictureBox();
        // classe Controller
        private GameController _gc;

        public GameView()
        {
            // istanza Controller
            _gc = new GameController(this);

            InitializeComponent();
        }

        // demando al Controller
        private void Timer_Tick(object sender, EventArgs e) => _gc.GameForward();

        // Metodo che si occupa di "pulire" la finestra di gioco
        public void ResetActivity(bool win)
        {
            // Invaders
            var fixedSize1 = _gc.Invaders.ToList();
            fixedSize1.ForEach(invader =>
            {
                // rimozione form Invader
                Controls.Remove(invader.Entity);
                // rimozione invader da lista Invaders
                _gc.Invaders.Remove(invader);
            });

            // Bullets
            var fixedSize2 = _gc.Bullets.ToList();
            fixedSize2.ForEach(bullet =>
            {
                // rimozione form bullet
                Controls.Remove(bullet.Entity);
                // rimozione bullet da lista Bullets
                _gc.Bullets.Remove(bullet);
            });

            // Player
            Controls.Remove(_gc.Player.Entity);

            // Ship
            if(_gc.LastLevel) {
                // rimozione form SpaceShip
                Controls.Remove(_gc.SpaceShip.Entity);
                // rimozione barra della vita di SpaceShip
                Controls.Remove(ShipLife);
            }
            
            // nel caso in cui il player abbia perso
            if (!win)
            {
                // Lifes
                /*
                 * nel caso in cui il player perda per via di una collisione con un invader
                 * e nel corso del gioco non aveva perso tutte le vite.
                 */
                pictureBox2.Hide();
                pictureBox3.Hide();
                pictureBox1.Hide();

                // cambio colore linea di fondo campo 
                Line.BackColor = Color.Red;
            }
        }

        /*
         * Metodo richiamato per inizializzare l'area di gioco:
         * => attivazione timer globale
         * => rimozione elementi UI introduttivi
         * => spawn linea di fondo campo
         * => "___" Player
         * => "___" Invaders (55)
         */
        public void UIAfterSpawn()
        {
            // attivazione timer
            timer.Enabled = true;

            // rimozione testo introduttivo
            Controls.Remove(label1);
            Controls.Remove(label5);

            // plot linea verde di fondo campo 
            Line.BackColor = Color.Green;
            Line.Size = new Size(ClientRectangle.Width, 10);
            Line.Tag = "Line";
            Line.Left = 0;
            Line.Top = (int)(ClientRectangle.Height * 0.92);
            Controls.Add(Line);
            Line.BringToFront();
        }

        // Metodo per il passaggio dal primo al secondo livello di gioco
        public void UISpaceShip()
        {
            // aggiunta barra vita SpaceShip
            ShipLife = new ProgressBar
            {
                Value = 100,
                ForeColor = Color.Red,
                Size = new Size(ClientRectangle.Width / 2, 20),
                Tag = "ShipLife",
                Left = ClientRectangle.Width / 4,
                Top = (int)(ClientRectangle.Height * 0.95)
            };
            Controls.Add(ShipLife);
            ShipLife.BringToFront();
        }

        // Metodo per l'ascolto nativo della pressione di tasti
        private void KeyIsPressed(object sender, KeyEventArgs e)
        {   
            // inizio l'ascolto solamente a timer avviato
            if(timer.Enabled)
            {
                switch (e.KeyCode)
                {
                    case Keys.Right:
                    case Keys.Left:
                        // demando al player il movimento ricevuto in input
                        _gc.Player.Move(e.KeyCode, _gc.PlayerMoveMode);
                        break;
                    case Keys.Space:
                        // demando al player lo sparo di un nuovo bullet
                        _gc.Player.Shoot(PlayerTimerShots);
                        break;
                    case Keys.C:
                        /*
                         * DEBUG => eliminazione invaders tranne l'ultimo 
                         * utile per passare velocemente al secondo livello di gioco (SpaceShip)
                         * abilitare SOLO per fasi di testing.
                         */
                        // _gc.KillInvaders();
                        break;
                }
            } else
            {
                // nel caso in cui non sia la prima partita
                if (_gc.Games > 0)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        // alla pressione del tasto "N" si avvierà una nuova partita
                        _gc.RestoreGameWindow();
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        // TODO: verificare se permetterlo o meno
                        Application.Exit();
                    }
                }
            }
        }

        // Metodo utile per avviare una nuova partita finita la precedente
        public void RestoreGameWindowUI()
        {
            // ripristino vite player
            pictureBox2.Show();
            pictureBox3.Show();
            pictureBox1.Show();

            // azzero punteggio player
            label4.Text = "0";

            // rimozione label vittoria/sconfitta
            Controls.Remove(Alert);
            Controls.Remove(AlertDesc);         
        }

        // Game Over
        public void GameOver()
        {
            // gioco in pausa
            timer.Enabled = false;
            
            /*
             * reset campo di gioco, il parametro indica:
             * true => in caso di vittoria 
             * false => altrimenti
             */
            ResetActivity(false);

            // Messaggio GAME OVER e proposta nuova partita
            Alert.Text = "GAME OVER";
            Alert.Tag = "GAMEOVER_LABEL";
            Alert.Size = new Size(300, 100);
            Alert.Left = (ClientRectangle.Width / 2) - (Alert.Size.Width / 2);
            Alert.Top = ((ClientRectangle.Height / 2) - (Alert.Size.Height / 2)) - 55;
            Alert.Font = new Font("Courier New", 30, FontStyle.Bold);
            Alert.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(Alert);
            Alert.BringToFront();
            AlertDesc.Text = "PRESS 'N' TO RESTART THE GAME.";
            AlertDesc.Tag = "GAMEOVER_DESC_LABEL";
            AlertDesc.Size = new Size(500, 100);
            AlertDesc.Left = (ClientRectangle.Width / 2) - (AlertDesc.Size.Width / 2);
            AlertDesc.Top = ((ClientRectangle.Height / 2) - (AlertDesc.Size.Height / 2)) + 15;
            AlertDesc.Font = new Font("Courier New", 20, FontStyle.Bold);
            AlertDesc.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(AlertDesc);
            Alert.BringToFront();
        }

        // Win
        public void Win()
        {
            // gioco in pausa
            timer.Enabled = false;

            // commento analogo linee [365-369]
            ResetActivity(true);

            // messaggio WIN e proposta nuova partita
            Alert.Text = "WIN! THE SPACE IS SAFE!";
            Alert.Tag = "GAMEWIN_LABEL";
            Alert.Size = new Size(500, 80);
            Alert.Left = (ClientRectangle.Width / 2) - (Alert.Size.Width / 2);
            Alert.Top = ((ClientRectangle.Height / 2) - (Alert.Size.Height / 2)) - 50;
            Alert.Font = new Font("Courier New", 20, FontStyle.Bold);
            Alert.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(Alert);
            Alert.BringToFront();
            AlertDesc.Text = "PRESS 'N' TO RESTART THE GAME.";
            AlertDesc.Tag = "GAMEOVER_DESC_LABEL";
            AlertDesc.Size = new Size(500, 80);
            AlertDesc.Left = (ClientRectangle.Width / 2) - (AlertDesc.Size.Width / 2);
            AlertDesc.Top = ((ClientRectangle.Height / 2) - (AlertDesc.Size.Height / 2)) + 20;
            AlertDesc.Font = new Font("Courier New", 20, FontStyle.Bold);
            AlertDesc.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(AlertDesc);
            Alert.BringToFront();
        }

        // Metodo per ridurre vite a disposizione del player
        public void RemoveLife(int life)
        {
            // rimozione sprite "vita" in alto a destra
            if (life == 2)
            {
                pictureBox2.Hide();
            } else if(life == 1)
            {
                pictureBox3.Hide();
            } else if(life == 0)
            {
                pictureBox1.Hide();
                // se l'ultima => GAME OVER
                GameOver();
            }
        }

        // Metodo per l'aggiornamento del punteggio del Player
        public void RefreshScore()
        {
            label4.Text = _gc.Player.Score.ToString();
        }

        // Metodo invocato dopo il click del pulsante 'Start' iniziale
        private void StartButton_Click(object sender, EventArgs e)
        {
            // rimozione settings iniziali da screen
            Controls.Remove(ModeOne);
            Controls.Remove(ModeTwo);
            Controls.Remove(StartButton);

            _gc.StartButtonClicked(ModeOne.Checked);
        }

        // Metodo che libera il flag che permette al giocatore di sparare nuovamente
        public void PlayerTimerShots_Tick(object sender, EventArgs e)
        {
            PlayerTimerShots.Stop();
            _gc.PlayerCanShoot = true;
        }

        // Generazione sprite cannone mobile comandato dal Giocatore
        public PictureBox SpritePlayer()
        {
            // sprite
            PictureBox Sprite = new PictureBox
            {
                Image = Properties.Resources.player,
                Size = new Size(47, 28),
                Tag = "Player"
            };
            Sprite.Left = (ClientRectangle.Width / 2) - (Sprite.Size.Width / 2);
            Sprite.Top = (int)(ClientRectangle.Height * 0.85);

            // plot a schermo
            Controls.Add(Sprite);
            Sprite.BringToFront();

            return Sprite;
        }
    }
}