/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public abstract class Invader
    {
        // istanza classe principale
        protected GameView _view;
        protected GameController _controller;
        // classe windows form per gestire lo sprite
        protected PictureBox entity = new PictureBox();
        // dimensioni sprite
        protected Size Sizes = new Size(25, 25);
        // velocità dell'invader
        protected int speed = 20;
        // parametro funzionale dipendente dal timer
        protected int FramesToSkip = 40;
        // punteggio rispettivo all'uccisione di tale invader
        protected int points;

        protected Invader(GameController controller, GameView view, Point location)
        {
            // assegnazione standard
            _view = view;
            _controller = controller;

            // impostazioni sprite invader
            entity.SizeMode = PictureBoxSizeMode.StretchImage;
            entity.Size = Sizes;
            entity.Left = location.X;
            entity.Top = location.Y;
        }

        public int Speed { get => speed; set => speed = value; }
        public int Points { get => points; set => points = value; }
        public PictureBox Entity { get => entity; set => entity = value; }

        // metodo che gestisce l'avanzamento dell'invader 
        public virtual void Next()
        {
            // avanzamento invaders ogni 800ms (40*20)
            if (_controller.Steps % FramesToSkip == 0)
            {
                // aggiornamento posizione in funzione del tempo 
                this.entity.Left += this.Speed; 
            }

            // verifica collisione con player o linea di fine campo
            if (Collide(_controller.Player.Entity) || Collide(_view.Line))
            {
                // DOH!
                _view.GameOver();
            }
        }

        /*
         * I due seguenti metodi in Overloading sono utili a rilevare eventuali
         * collisioni degli invader sia con i bullet che con il player stesso.
         */
        public bool Collide(Control cstrl)
        {
            return entity.Bounds.IntersectsWith(cstrl.Bounds);
        }

        public bool Collide(Bullet bullet)
        {
            return entity.Bounds.IntersectsWith(bullet.Entity.Bounds);
        }

    }
}
