/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System.Drawing;

namespace SpaceInvaders
{
    public class Octopus : Invader
    {
        public Octopus(GameController controller, GameView view, Point location) : base(controller, view, location)
        {
            // punti per l'uccisione di invader di tipo 2
            Points = 10;

            // assegnazione immagine e tag per invader di tipo 2
            entity.Image = Properties.Resources.invader2;
            entity.Tag = "Invader_2";

            // plot a schermo
            _view.Controls.Add(entity);
            entity.BringToFront();
        }
    }
}
