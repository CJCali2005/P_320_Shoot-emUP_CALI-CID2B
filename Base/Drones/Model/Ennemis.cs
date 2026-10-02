using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace P_320_POO_CALI.Model
{
    internal class Ennemis
    {
        Image rdmEnnemis;

        Random indexEnnemis = new Random();
        // Image Ennemis
        Image[] tabOfEnnemis =
        {
            Properties.Resources.EnnemiN1,
            Properties.Resources.EnnemiN2,
            Properties.Resources.EnnemiN3,
            Properties.Resources.EnnemiN4,
            Properties.Resources.EnnemiN5,
            Properties.Resources.EnnemiN6
        };

        Image[] tabOfBullets =
        {
            Properties.Resources.EnnemiN1,
            Properties.Resources.EnnemiN2,
            Properties.Resources.EnnemiN3,
            Properties.Resources.EnnemiN4,
            Properties.Resources.EnnemiN5,
            Properties.Resources.EnnemiN6
        };

        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y;                                 // Position en Y depuis le haut de l'espace aérien
        public int speed_y;
        // Déplacement vertical
        private Random _rdm = new Random();

        // Constructeur
        public Ennemis(int x, int y)
        {
            this.x = x;
            this.y = y;
            rdmEnnemis = tabOfEnnemis[indexEnnemis.Next(tabOfEnnemis.Length)];
        }

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {
             
            y += speed_y;
        }

        // Choisit une nouvelle vitesse aléatoirement
        public void EnnemisRandom()
        {


        }

        /// //////////////////////////////////////////////////////////////////////////////
        //  
        //  Ce qui suit appartient à la vue, pas au modèle.
        //  Il aurait été préférable de séparer la déclaration de la classe Drone en deux,
        //  Nous regroupons tout ici pour simplifier
        //  
        /// //////////////////////////////////////////////////////////////////////////////

        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
                drawingSpace.Graphics.DrawImage(rdmEnnemis, x, y, 45, 100);
            
        }


        
    }
}
