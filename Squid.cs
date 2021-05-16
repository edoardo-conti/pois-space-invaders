/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System.Drawing;

namespace SpaceInvaders
{
    public class Squid : Invader
    {
        public Squid(GameController controller, GameView view, Point location) : base(controller, view, location)
        {
            // punti per l'uccisione di invader di tipo 3
            Points = 30;

            // assegnazione immagine e tag per invader di tipo 3
            entity.Image = Properties.Resources.invader3;
            entity.Tag = "Invader_3";
            
            // plot a schermo
            _view.Controls.Add(entity);
            entity.BringToFront();
        }
    }
}
