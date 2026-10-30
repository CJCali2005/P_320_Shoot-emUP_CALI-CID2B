using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_320_POO_CALI.Model
{
    internal class bulletEnnemis
    {
        public int speed_y = 500;
        public int posX;
        public int posY;

        public int height = 75 ;
        public int width = 45 ;

        Random indexBullet = new Random();

        Image chosenPics;

        Image[] tabOfBullet =
        {
            Properties.Resources.EnnemisBullet1,
            Properties.Resources.EnnemisBullet2,
            Properties.Resources.EnnemisBullet3,
            Properties.Resources.EnnemisBullet4,
            Properties.Resources.EnnemisBullet5,
            Properties.Resources.EnnemisBullet6,
            Properties.Resources.EnnemisBullet7,
            Properties.Resources.EnnemisBullet8,
            Properties.Resources.EnnemisBullet9,
            Properties.Resources.EnnemisBullet10,
            Properties.Resources.EnnemisBullet11,
            Properties.Resources.EnnemisBullet12,
            Properties.Resources.EnnemisBullet13,
            Properties.Resources.EnnemisBullet14,
            Properties.Resources.EnnemisBullet15,

        };

        public bulletEnnemis(int x, int y)
        {
            posX = x;
            posY = y;
            chosenPics = tabOfBullet[indexBullet.Next(0, tabOfBullet.Length - 1)];
        }

        public void Update(int interval)
        {
            posY += speed_y * interval / 1000;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(chosenPics, (int)posX, (int)posY, 45, 75);
        }
    }
}
