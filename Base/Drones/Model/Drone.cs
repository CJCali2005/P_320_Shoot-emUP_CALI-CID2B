using Drones.Helpers;
using P_320_POO_CALI.Properties;

namespace Drones
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public class Drone
    {
        public string name;                           // Un nom
        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y;                                 // Position en Y depuis le haut de l'espace aérien
        public int speed_x = 10;                           // Déplacement horizontal

        private Random _alea = new Random();



        // Constructeur
        public Drone(int x, int y, string name)
        {
            Random alea = new Random();
            this.x = x;
            this.y = y;
            this.name = name;
        }

        // Cette méthode calcule le nouvel état dans lequel le drone se trouve après
        // que 'interval' millisecondes se sont écoulées
        public void Update(int interval)
        {

        }

        public void keyLeftMotion()
        {
            if (x - speed_x <= 0)
            {
                x -= 0;
            }
            else
            {
                x -= speed_x ;
            }
        }

        public void keyRightMotion()
        {

            // - 60 est la largeur de mon joueur
            if (x + speed_x >= AirSpace.WIDTH - 60)
            {

                x += 0;

            }
            else { x += speed_x; }
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

            drawingSpace.Graphics.DrawImage(Resources.guitariste, x, y, 60, 105);
        }





    }
}
