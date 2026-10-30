using P_320_POO_CALI.Model;
using P_320_POO_CALI.Properties;
using System.Net;
namespace Drones
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class AirSpace : Form
    {
        public static readonly int WIDTH = 1200;        // Dimensions of the airspace
        public static readonly int HEIGHT = 600;
        private List<Ennemis> foule = new List<Ennemis>();
        Random rdmApparitionEnnemis = new Random();
        public int intervalEnnemis = 0;

        private int indicePosX = 10;

        // j'ai créé cette variable pour ralentir la fréquence de tir
        private int ralentisseurTir = 0;

        private List<bulletPlayer> balles = new List<bulletPlayer>();
        private List<Obstacle> barrieres = new List<Obstacle>();



        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        // représente le lien avec la class dronne
        private Guitariste _player;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;
        private BufferedGraphics drawingSpace;

        // Initialisation de l'espace aérien avec un certain nombre de drones
        public AirSpace(Guitariste player)
        {
            InitializeComponent();
            ClientSize = new Size(WIDTH, HEIGHT);

            // une nouvelle frame 5x plus rapide qu'avant car avant c'était 100 par defaut et j'ai fais pour la fluidité des déplacement
            ticker.Interval = 20;

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this._player = player;


            // pour form accepte les touche du clavier
            this.KeyPreview = true;

            this.KeyDown += AirSpace_KeyDown;


            for (int i = 0; i < 11; i++)
            {
                barrieres.Add(new Obstacle(indicePosX, HEIGHT - 210));
                indicePosX += 108;
            }

        }

        // Affichage de la situation actuelle
        private void Render()
        {
            // Affichage du fond d'écran
            airspace.Graphics.DrawImage(P_320_POO_CALI.Properties.Resources.salle_concert, 0, 0, WIDTH, HEIGHT);

            _player.Render(airspace);

            foreach (Ennemis ennemi in foule)
            {
                ennemi.Render(airspace);
            }

            foreach (bulletPlayer balle in balles)
            {
                balle.Render(airspace);
            }

            foreach (Obstacle barriere in barrieres)
            {
                barriere.Render(airspace);
            }

            airspace.Render();

        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {

            // ajout de la valeur de interval "timer" dans intervalennemis qui est la valeur qui va enclencher la condition lorsque elle atteint 1000 (1seconde) 
            intervalEnnemis += interval;
            if (intervalEnnemis >= 2000)
            {
                foule.Add(new Ennemis(rdmApparitionEnnemis.Next(0, WIDTH), -50));

                intervalEnnemis = 0;
            }

            if (ralentisseurTir > 0)
            {
                ralentisseurTir -= interval;
            }


            // à chaque fois que un ennemi sors de l'écran contre le bas, il est supprimé de la liste
            for (int i = foule.Count - 1; i >= 0; i--)
            {
                foule[i].Update(interval);

                if (foule[i].y >= HEIGHT + 50)
                {
                    foule.RemoveAt(i);
                }

                foreach(Obstacle grillage in barrieres)
                {
                    if ((foule[i].y <= grillage._posY && foule[i].y + foule[i].height >= grillage._posY) &&( (foule[i].x <= grillage._posX && foule[i].x + foule[i].width >= grillage._posX) || (foule[i].x >= grillage._posX && (foule[i].x + foule[i].width) <= (grillage._posX + grillage.width)) || (foule[i].x >= grillage._posX && foule[i].x + foule[i].width >= (grillage._posX + grillage.width))))
                    {
                        foule.RemoveAt(i);
                    }
                }
            }

            // transformé en for au lieu d'un foreach car avant j'avais une erreur.
            for (int i = balles.Count - 1; i >= 0; i--)
            {
                balles[i].Update(interval);

                if (balles[i].posY <= -50)
                {
                    balles.RemoveAt(i);
                }
            }

            
        }

        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }

        private void AirSpace_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // dans le cas ou je clique fleche droite
                case Keys.Right:
                    _player.keyRightMotion();
                    break;

                // dans le cas ou je clique fleche gauche
                case Keys.Left:
                    _player.keyLeftMotion();
                    break;
                case Keys.Space:

                    if (ralentisseurTir <= 0)
                    {
                        balles.Add(new bulletPlayer(_player.x, _player.y));
                        ralentisseurTir = 200; // sa met 200 ms entre chaque tir
                    }

                    break;

            }
        }
    }
}