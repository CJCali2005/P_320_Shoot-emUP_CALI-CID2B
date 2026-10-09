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



        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien
        // représente le lien avec la class dronne
        private Drone _player;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;
        private BufferedGraphics drawingSpace;

        // Initialisation de l'espace aérien avec un certain nombre de drones
        public AirSpace(Drone player)
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


        }

        // Affichage de la situation actuelle
        private void Render()
        {
            // Affichage du fond d'écran
            airspace.Graphics.DrawImage(P_320_POO_CALI.Properties.Resources.salle_concert, 0, 0, WIDTH, HEIGHT);

            _player.Render(airspace);

            foreach(Ennemis ennemi in foule)
            {
                ennemi.Render(airspace);
            }  

            airspace.Render();

        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            _player.Update(interval);

            // ajout de la valeur de interval "timer" dans intervalennemis qui est la valeur qui va enclencher la condition lorsque elle atteint 1000 (1seconde) 
            intervalEnnemis += interval;
            if (intervalEnnemis >= 2000)
            {
                foule.Add(new Ennemis(rdmApparitionEnnemis.Next(0, WIDTH), -50));

                intervalEnnemis = 0;
            }

            

            foreach (Ennemis i in foule)
            {
                i.Update(interval);

                if( i.y == HEIGHT + 50)
                {
                    foule.Remove(i);
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


            }
        }
    }
}