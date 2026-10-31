using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_320_POO_CALI.Model
{
    internal class bulletPlayer
    {
        public int speed_y = 500;
        public int posX;
        public int posY;

        public int width = 45 ;
        public int height = 75 ;

        Random indexBullet = new Random();

        Image chosenPics;

        Image [] tabOfBullet =
        {
            Properties.Resources.bullet_player_1,
            Properties.Resources.bullet_player_2,
            Properties.Resources.bullet_player_3,
            Properties.Resources.bullet_player_4,
            Properties.Resources.bullet_player_5,
            Properties.Resources.bullet_player_6,
            Properties.Resources.bullet_player_7,
            Properties.Resources.bullet_player_8,
            Properties.Resources.bullet_player_9,
            Properties.Resources.bullet_player_10,
            Properties.Resources.bullet_player_11,
            Properties.Resources.bullet_player_12,

        };
        
        public bulletPlayer(int x, int y )
        {
           posX = x;
           posY = y;
           chosenPics = tabOfBullet[indexBullet.Next(0, tabOfBullet.Length - 1)];
        }

        public void Update(int interval)
        {
            posY -= speed_y * interval / 1000;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(chosenPics, (int)posX, (int)posY, 45, 75);
        }
    }
}
