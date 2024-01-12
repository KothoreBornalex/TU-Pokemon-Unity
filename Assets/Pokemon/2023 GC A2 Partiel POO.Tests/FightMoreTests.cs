
using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer les TU sur le reste et de les implémenter

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // - un heal ne régénère pas plus que les HP Max
        // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type

        [Test]
        public void Heal()
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);

            c.IncreaseHealth(2000);
            Assert.That(c.MaxHealth, Is.EqualTo(c.CurrentHealth));
        }

        [Test]
        public void HPMaxAppliedToCurrentHealth()
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);
            var e = new Equipment(100, 90, 70, 12);

            c.Equip(e);
            Assert.That(c.MaxHealth, Is.EqualTo(100 + e.BonusHealth));
            c.IncreaseHealth(2000);
            Assert.That(c.CurrentHealth, Is.EqualTo(100 + e.BonusHealth));

            c.Unequip();

            Assert.That(c.CurrentHealth, Is.EqualTo(c.MaxHealth));
        }


        [Test]
        public void FightAttackPrioritaire()
        {
            // In this test I'm proving that unregarding of wich order I sent the characters, the character with attack priority will win.
            Character pikachu = new Character(100, 100, 0, 20, TYPE.NORMAL);
            var e = new Equipment(0, 0, 0, 0, true);
            pikachu.Equip(e);

            Character mewtwo = new Character(70, 100, 0, 20, TYPE.NORMAL);


            Fight f = new Fight(pikachu, mewtwo);
            Punch p = new Punch();
            MegaPunch mP = new MegaPunch();

            f.ExecuteTurn(p, mP);

            Assert.That(f.Character1, Is.EqualTo(pikachu));
            Assert.That(f.Character2, Is.EqualTo(mewtwo));
            Assert.That(f.IsFightFinished, Is.EqualTo(true));
            Assert.That(f.Winner, Is.EqualTo(pikachu));





            // Ici j'inverse l'ordre

            Character Bulbi = new Character(100, 100, 0, 20, TYPE.NORMAL);
            Bulbi.Equip(e);

            Character Draco = new Character(70, 100, 0, 20, TYPE.NORMAL);

            Fight newFight = new Fight(Draco, Bulbi);
            newFight.ExecuteTurn(mP, p);

            Assert.That(newFight.Character1, Is.EqualTo(Draco));
            Assert.That(newFight.Character2, Is.EqualTo(Bulbi));
            Assert.That(newFight.IsFightFinished, Is.EqualTo(true));
            Assert.That(newFight.Winner, Is.EqualTo(Bulbi));





            // Ici les deux ont l'attack priority, donc l'algo se basera sur les stats de vitesses pour choisir qui attaquera en premier.

            Character Tortank = new Character(50, 100, 0, 20, TYPE.NORMAL);
            Tortank.Equip(e);

            Character Xerneas = new Character(50, 100, 0, 30, TYPE.NORMAL);
            Xerneas.Equip(e);

            Fight newNewFight = new Fight(Xerneas, Tortank);
            newNewFight.ExecuteTurn(p, p);
            newNewFight.ExecuteTurn(p, p);

            Assert.That(newNewFight.Character1, Is.EqualTo(Xerneas));
            Assert.That(newNewFight.Character2, Is.EqualTo(Tortank));
            Assert.That(newNewFight.IsFightFinished, Is.EqualTo(true));
            Assert.That(newNewFight.Winner, Is.EqualTo(Xerneas));
        }

    }
}
