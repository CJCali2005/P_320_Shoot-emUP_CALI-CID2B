using ShootEmUp.Helpers;
using P_320_POO_CALI.Properties;

namespace ShootEmUp
{
    // Cette classe représente le joueur principal
    public class Guitariste
    {
        public string name;                           // Un nom
        public int x;                                 // Position en X depuis la gauche de l'espace aérien
        public int y;                                 // Position en Y depuis le haut de l'espace aérien
        public int speed_x = 10;                           // Déplacement horizontal

        public int width = 60 ;
        public int height = 105 ;

        public bool vivant = true;

        private Random _alea = new Random();



        // Constructeur
        public Guitariste(int x, int y, string name)
        {
            Random alea = new Random();
            this.x = x;
            this.y = y;
            this.name = name;
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
            if (x + speed_x >= SalleConcert.WIDTH - 60)
            {

                x += 0;

            }
            else { x += speed_x; }
        }

        /// //////////////////////////////////////////////////////////////////////////////
        //  
        //  Ce qui suit appartient à la vue, pas au modèle.
        //  Il aurait été préférable de séparer la déclaration de la classe Guitariste en deux,
        //  Nous regroupons tout ici pour simplifier
        //  
        /// //////////////////////////////////////////////////////////////////////////////


        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {

            drawingSpace.Graphics.DrawImage(Resources.guitariste, x, y, 60, 105);
        }





    }
}
