/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System.Drawing;

namespace SpaceInvaders
{
    public class Ship : Invader 
    {
        // punti vita dell'invader finale SpaceShip
        public int Life = 100;

        public Ship(GameController controller, GameView view, Point location) : base(controller, view, location)
        {
            /*
             * caratteristiche che variano da un classico invader
             * Speed=2 => l'invader finale vanta di un movimento lineare continuo
             * Points=100 => punti da aggiungere allo score del player in caso di vittoria
             * FramesToSkip=10 => parametro necessario per il movimento 
             */
            Speed = 2;
            Points = 110;
            FramesToSkip = 10;

            // assegnazione immagine, dimensioni e tag
            entity.Image = Properties.Resources.spaceship;
            entity.Size = new Size(88, 38);
            entity.Tag = "Ship";

            // plot a schermo
            _view.Controls.Add(entity);
            entity.BringToFront();
        }

        public override void Next()
        {
            // cambio di rotta raggiunti i limiti DX e SX dell'area di gioco
            if (entity.Left >= _view.ClientRectangle.Width * 0.8 ||
                entity.Left < _view.ClientRectangle.Width * 0.05)
            {
                Speed *= -1;
            }

            // spostamento
            entity.Left += Speed;
        }
    }
}
