using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_320_POO_CALI.Model;
using ShootEmUp;

namespace TestUnitaires_projet_poo
{
    [TestClass]
    public class Test1
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
    }
}