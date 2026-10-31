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

        private int _score = 0;
        private int _nbKill = 0;

        private List<Ennemis> foule = new List<Ennemis>();
        Random rdmApparitionEnnemis = new Random();
        public int intervalEnnemis = 0;

        private int indicePosX = 10;

        // j'ai créé cette variable pour ralentir la fréquence de tir
        private int ralentisseurTir = 0;

        private List<bulletPlayer> balles = new List<bulletPlayer>();
        private List<bulletEnnemis> ballesEnnemis = new List<bulletEnnemis>();
        private Random rdmTirEnnemi = new Random();

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


            if (_player.vivant)
            {
                _player.Render(airspace);
            }

            foreach (Ennemis ennemi in foule)
            {
                ennemi.Render(airspace);
            }

            foreach (bulletPlayer balle in balles)
            {
                balle.Render(airspace);
            }
            foreach (bulletEnnemis balle in ballesEnnemis)
            {
                balle.Render(airspace);
            }

            foreach (Obstacle barriere in barrieres)
            {
                barriere.Render(airspace);
            }

            airspace.Graphics.DrawString("score : " + _score  + "    NB ENNEMIS KILL : " + _nbKill,new Font("Arial", 20), Brushes.Red, WIDTH / 2 - 50 , HEIGHT - 35);

            airspace.Render();

        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            // Pour supprimer les barrières dés qu'elles n'ont plus de vie
            for (int j = barrieres.Count - 1; j >= 0; j--)
            {
                if (barrieres[j].LifeObstacle <= 0)
                {
                    barrieres.RemoveAt(j);
                }
            }

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

                if (rdmTirEnnemi.Next(100) == 0)
                {
                    ballesEnnemis.Add(
                        new bulletEnnemis(foule[i].x, foule[i].y)
                    );
                }

                if (foule[i].y >= HEIGHT + 50)
                {
                    foule.RemoveAt(i);
                }

                
                foreach (Obstacle grillage in barrieres)
                {
                    bool collision =
                                    foule[i].x < grillage._posX + grillage.width &&
                                    foule[i].x + foule[i].width > grillage._posX &&
                                    foule[i].y < grillage._posY + grillage.height &&
                                    foule[i].y + foule[i].height > grillage._posY;

                    if (collision)
                    {

                        foule.RemoveAt(i);
                        grillage.Degats();
                        break;
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

            for (int i = ballesEnnemis.Count - 1; i >= 0; i--)
            {
                ballesEnnemis[i].Update(interval);

                bool balleSupprimee = false;

                foreach (Obstacle grillage in barrieres)
                {
                    bool collision =
                        ballesEnnemis[i].posX < grillage._posX + grillage.width &&
                        ballesEnnemis[i].posX + ballesEnnemis[i].width > grillage._posX &&
                        ballesEnnemis[i].posY < grillage._posY + grillage.height &&
                        ballesEnnemis[i].posY + ballesEnnemis[i].height > grillage._posY;

                    if (collision)
                    {
                        grillage.Degats();
                        ballesEnnemis.RemoveAt(i);

                        balleSupprimee = true;
                        break;
                    }
                }

                if (balleSupprimee)
                {
                    continue;
                }

                if (ballesEnnemis[i].posY >= HEIGHT + 50)
                {
                    ballesEnnemis.RemoveAt(i);
                }
            }

            // on compare chaque balle du joueur avec chaque balle ennemie
            for (int i = balles.Count - 1; i >= 0; i--)
            {
                for (int j = ballesEnnemis.Count - 1; j >= 0; j--)
                {
                    bool collision =
                        balles[i].posX < ballesEnnemis[j].posX + ballesEnnemis[j].width &&
                        balles[i].posX + balles[i].width > ballesEnnemis[j].posX &&
                        balles[i].posY < ballesEnnemis[j].posY + ballesEnnemis[j].height &&
                        balles[i].posY + balles[i].height > ballesEnnemis[j].posY;

                    if (collision)
                    {
                        // suppression de la balle du joueur dans la list 
                        balles.RemoveAt(i);

                        // suppression aussi dans la liste
                        ballesEnnemis.RemoveAt(j);

                        _score += 5;

                        break;
                    }
                }
            }

            // collision entre les balles du joueur et les ennemis
            for (int i = balles.Count - 1; i >= 0; i--)
            {
                for (int j = foule.Count - 1; j >= 0; j--)
                {
                    bool collision =
                        balles[i].posX < foule[j].x + foule[j].width &&
                        balles[i].posX + balles[i].width > foule[j].x &&
                        balles[i].posY < foule[j].y + foule[j].height &&
                        balles[i].posY + balles[i].height > foule[j].y;

                    if (collision)
                    {
                        // suppression de la balle
                        balles.RemoveAt(i);

                        // suppression de l ennemi
                        foule.RemoveAt(j);

                        _score += 10;
                        _nbKill++;

                        break;
                    }
                }
            }

            // collision entre le joueur et les balles ennemies
            for (int i = ballesEnnemis.Count - 1; i >= 0; i--)
            {
                bool collision =
                    ballesEnnemis[i].posX < _player.x + _player.width &&
                    ballesEnnemis[i].posX + ballesEnnemis[i].width > _player.x &&
                    ballesEnnemis[i].posY < _player.y + _player.height &&
                    ballesEnnemis[i].posY + ballesEnnemis[i].height > _player.y;

                if (collision)
                {
                    ballesEnnemis.RemoveAt(i);
                    break;
                }
            }

            // collision entre le joueur et les ennemis
            for (int i = foule.Count - 1; i >= 0; i--)
            {
                bool collision =
                    foule[i].x < _player.x + _player.width &&
                    foule[i].x + foule[i].width > _player.x &&
                    foule[i].y < _player.y + _player.height &&
                    foule[i].y + foule[i].height > _player.y;

                if (collision)
                {
                    foule.RemoveAt(i);
                    _player.vivant = false;
                    break;
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
            if (!_player.vivant)
            {
                return;
            }


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