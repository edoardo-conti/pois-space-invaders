/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System.Drawing;

namespace SpaceInvaders
{
    public class Crab : Invader
    {
        public Crab(GameController controller, GameView view, Point location) : base(controller, view, location)
        {
            // punti per l'uccisione di invader di tipo 1
            Points = 20;

            // assegnazione immagine e tag per invader di tipo 1
            entity.Image = Properties.Resources.invader1;
            entity.Tag = "Invader_1";

            // plot a schermo
            _view.Controls.Add(this.entity);
            entity.BringToFront();
        }
    }
}
