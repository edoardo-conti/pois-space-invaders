/*
 * Edoardo Conti [278718]
 * Università degli Studi di Urbino Carlo Bo
 * Corso di Laurea in Informatica Applicata
 * Programmazione ad Oggetti e Ingegneria del Software
 */

using System;
using System.Windows.Forms;

namespace SpaceInvaders
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameView());
        }
    }
}
