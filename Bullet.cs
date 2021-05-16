/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System.Drawing;
using System.Threading.Tasks;
using SpaceInvaders.Properties;
using System.Windows.Forms;
using System.Linq;

namespace SpaceInvaders
{
    public class Bullet
    { 
        // definizione utile per distinguere chi ha sparato tale bullet
        public enum Shooter
        {
            Player,
            Invader,
            Ship
        }
        // istanza classe principale
        private GameController _controller;
        private GameView _view;
        // classe windows form per gestire lo sprite
        private PictureBox entity;
        // dimensioni sprite
        private Size Sizes = new Size(3, 18);
        // parametro funzionale dipendente dal timer
        private static int FramesToSkip = 2;
        // velocità dello sparo
        private int Speed;
        // oggetto che ha sparato il bullet
        public Shooter shooter;

        public PictureBox Entity { get => entity; set => entity = value; }

        public Bullet(GameController controller, GameView view, Point coords, int speed, Shooter who)
        {
            // assegnazioni standard
            _controller = controller;
            _view = view;
            Speed = speed;

            // definizioni sprite bullet
            entity = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = this.Sizes,
                Tag = "Bullet",
                Left = coords.X,
                Top = coords.Y
            };
            shooter = who;

            // assegnamento immagine in base al proprietario ( colori differenti )
            switch(who)
            {
                case Shooter.Player:
                    entity.Image = Properties.Resources.bullet1;
                    break;
                case Shooter.Ship:
                    entity.Image = Properties.Resources.bullet2;
                    break;
                default:
                    entity.Image = Properties.Resources.bullet3;
                    break;
            }

            // plot a schermo
            _view.Controls.Add(entity);
            entity.BringToFront();
        }

        public void Next()
        {
            /*
             * senza tale limitazione si va incontro ad un uso eccessivo delle risorse
             * della macchina host e troppe occorrenze di controlli non necessari
             */
            if (_controller.Steps % FramesToSkip == 0)
            {
                // gestione comportamento colpo sparato da Player
                if (shooter == Shooter.Player)
                {
                    // aggiornamento posizione in funzione del tempo 
                    this.Entity.Top -= this.Speed;

                    // se il colpo è prossimo all'estremo superiore dell'area di gioco viene rimosso
                    if (this.Entity.Top < _view.ClientRectangle.Height * 0.12)
                    {
                        _view.Controls.Remove(this.Entity);
                        _controller.Bullets.Remove(this);
                    }

                    // collisione invader con bullet sparato da Player
                    _controller.Invaders.ForEach(async invader =>
                    {
                        if (invader.Collide(this))
                        {
                            // rimozione bullet dalla scena e lista
                            _view.Controls.Remove(this.Entity);
                            _controller.Bullets.Remove(this);

                            // aggiunta punti ed aggiornamento grafico
                            _controller.Player.Score += invader.Points;
                            _view.RefreshScore();

                            // cambio sprite invader
                            invader.Entity.Image = Resources.shot;

                            // attesa
                            await Task.Delay(150);

                            // rimozione invader dall'area di gioco e da lista
                            _view.Controls.Remove(invader.Entity);
                            _controller.Invaders.Remove(invader);

                            // se sono stati colpiti tutti gli invader si passa al livello finale
                            if (_controller.Invaders.Count == 0)
                            {
                                // Final Level!
                                this._controller.NextLevel();
                            }
                        }
                    });

                    // collisione bullet Player vs bullet Invader
                    var BulletsFixed = _controller.Bullets.ToList();
                    BulletsFixed.ForEach(bullet =>
                    {
                        if (Collide(bullet) && bullet != this)
                        {
                            // rimozione bullet dalla scena e lista
                            _view.Controls.Remove(Entity);
                            _view.Controls.Remove(bullet.Entity);
                            _controller.Bullets.Remove(this);
                            _controller.Bullets.Remove(bullet);
                        }
                    });

                    // collisione SpaceShip con bullet sparato da Player
                    if (_controller.LastLevel)
                    {
                        if (_controller.SpaceShip.Collide(this))
                        {
                            // rimozione bullet dalla scena e da lista
                            _view.Controls.Remove(this.Entity);
                            _controller.Bullets.Remove(this);

                            // riduzione vita SpaceShip
                            _controller.SpaceShip.Life -= 10;
                            // aggiornamento ShipLife
                            _view.ShipLife.Value = _controller.SpaceShip.Life;

                            // distruzione SpaceShip
                            if (_controller.SpaceShip.Life == 0)
                            {
                                // aggiunta punti sconfitta SpaceShip
                                _controller.Player.Score += 200;
                                _view.RefreshScore();

                                // Yup!
                                _view.Win();
                            }
                        }
                    }
                } else {
                    // gestione comportamento colpo sparato da Invaders/Ship
                    // aggiornamento posizione in funzione del tempo 
                    this.Entity.Top += this.Speed;

                    // collisione player con bullet sparato da invader
                    if (_controller.Player.Collide(this))
                    {
                        // rimozione bullet dalla scena e lista
                        _view.Controls.Remove(this.Entity);
                        _controller.Bullets.Remove(this);

                        // demando alla classe Activity ->
                        _controller.Player.Hit();
                    }

                    // se il colpo è prossimo all'estremo inferiore dell'area di gioco viene rimosso
                    if (this.Entity.Top > this._view.ClientRectangle.Height * 0.88)
                    {
                        _view.Controls.Remove(this.Entity);
                        _controller.Bullets.Remove(this);
                    }
                }
            }
        }

        // metodo utilizzato per la rilevazione di collisione con altri bullets
        private bool Collide(Bullet bullet)
        {
            return Entity.Bounds.IntersectsWith(bullet.Entity.Bounds);
        }
    }
}