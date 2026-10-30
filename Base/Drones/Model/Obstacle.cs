using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_320_POO_CALI.Model
{
    internal class Obstacle
    {
        public int obstacleState = 0;
        public int _posX;
        public int _posY;

        public int width = 100;
        public int height = 50;

        private int lifeObstacle;

        Image[] TabOfGrillage =
        {
            Properties.Resources.grillage_bon_etat,
            Properties.Resources.grillage_casse
        };

        public Obstacle(int x, int y)
        {

            obstacleState = 1;
            lifeObstacle = 100;
            _posX = x;
            _posY = y;
        }

        public void Degats()
        {
            lifeObstacle = lifeObstacle - 50;

        }

        public void Render(BufferedGraphics drawingSpace)
        {
            if (lifeObstacle == 100)
            {
                drawingSpace.Graphics.DrawImage(TabOfGrillage[0], (int)_posX, (int)_posY, width, height);
            }
            else if (lifeObstacle == 50)
            {
                drawingSpace.Graphics.DrawImage(TabOfGrillage[1], (int)_posX, (int)_posY, 45, 100);
            }
        }
    }
}


