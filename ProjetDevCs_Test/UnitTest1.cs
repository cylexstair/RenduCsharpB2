using System;
using Xunit;
using ProjetDevCs;
using System.Collections.Generic;

namespace ProjetDevCs_Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestArrondi()
        {
            //Act
            float arrondiTop =1.346F;
            float arrondiBot = 1.343F;
            float exceptedTop = 1.35F;
            float exceptedBot = 1.34F;
            //Arrange
            float resultTop=Program.Arrondi(arrondiTop);
            float resultBot = Program.Arrondi(arrondiBot);
            //Assert
            Assert.Equal(exceptedTop, resultTop);
            Assert.Equal(exceptedBot, resultBot);
        }
        [Fact]
        public void TestAjoutPersonne()
        {
            string nom = "Etienne";
            int argent = 30;
            int taille = Program.ToutLeMonde.Count;

            Program.AjouterPersonne(nom, argent);

            Assert.Equal(taille + 1, Program.ToutLeMonde.Count);//un Humain a bien été créer
            Assert.Equal(argent, Program.total);//son argent a bien été ajouté au total du groupe
        }
    }
}
