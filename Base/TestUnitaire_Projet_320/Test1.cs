using P_320_POO_CALI.Model;
using ShootEmUp;

namespace TestUnitaire_Projet_320
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void KeyRightMotion_DeplaceLeJoueurVersLaDroite()
        {
            // Arrange
            Guitariste joueur = new Guitariste(100, 100, "test");

            // Act
            joueur.keyRightMotion();

            // Assert
            Assert.AreEqual(110, joueur.x);
        }

        [TestMethod]
        public void KeyLeftMotion_DeplaceLeJoueurVersLaGauche()
        {
            // Arrange
            Guitariste joueur = new Guitariste(100, 100, "test");

            // Act
            joueur.keyLeftMotion();

            // Assert
            Assert.AreEqual(90, joueur.x);
        }

        [TestMethod]
        public void Degats_Retire50PointsDeVie()
        {
            // Arrange
            Obstacle obstacle = new Obstacle(100, 100);

            // Act
            obstacle.Degats();

            // Assert
            Assert.AreEqual(50, obstacle.LifeObstacle);
        }
    }
}
