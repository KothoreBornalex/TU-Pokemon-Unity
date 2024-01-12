
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            if (character1 == null || character2 == null) throw new ArgumentNullException("No Characters should be null in the fight !");

            Character1 = character1;
            Character2 = character2;
        }

        public Character Character1 { get; }
        public Character Character2 { get; }
        public Character Winner { get; private set; }

        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished => IsCurrentFightFinished();

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {

            bool characterOneHasAttackPriority = false;
            bool characterTwoHasAttackPriority = false;

            if (Character1.CurrentEquipment != null)
            {
                if (Character1.CurrentEquipment.AttackPriority) characterOneHasAttackPriority = true;
            }

            if (Character2.CurrentEquipment != null)
            {
                if (Character2.CurrentEquipment.AttackPriority) characterTwoHasAttackPriority = true;
            }

            //Character 1 Prioritaire
            if (characterOneHasAttackPriority && !characterTwoHasAttackPriority)
            {
                Character2.ReceiveAttack(skillFromCharacter1);
                if (CheckCallEndFight()) return;

                Character1.ReceiveAttack(skillFromCharacter2);
                if (CheckCallEndFight()) return;
            }

            //Character 2 Prioritaire
            if (!characterOneHasAttackPriority && characterTwoHasAttackPriority)
            {
                Character1.ReceiveAttack(skillFromCharacter2);
                if (CheckCallEndFight()) return;

                Character2.ReceiveAttack(skillFromCharacter1);
                if (CheckCallEndFight()) return;
            }



            if (!characterOneHasAttackPriority && !characterTwoHasAttackPriority || characterOneHasAttackPriority && characterTwoHasAttackPriority)
            {
                if (Character1.Speed > Character2.Speed)
                {
                    Character2.ReceiveAttack(skillFromCharacter1);
                    if (CheckCallEndFight()) return;

                    Character1.ReceiveAttack(skillFromCharacter2);
                    if (CheckCallEndFight()) return;
                }
                else if (Character1.Speed < Character2.Speed) { }
                {
                    Character1.ReceiveAttack(skillFromCharacter2);
                    if (CheckCallEndFight()) return;

                    Character2.ReceiveAttack(skillFromCharacter1);
                    if (CheckCallEndFight()) return;
                }
            }

            bool CheckCallEndFight()
            {
                if (!Character1.IsAlive || !Character2.IsAlive)
                {
                    EndFight();
                    return true;
                }
                else
                {
                    return false;
                }
            }
                

        }

        public void EndFight()
        {
            if (Character1.IsAlive)
            {
                Character1.SetHealth(Character1.MaxHealth);
                Winner = Character1;
            }
            else if (Character2.IsAlive)
            {
                Character2.SetHealth(Character2.MaxHealth);
                Winner = Character2;
            }
        }

        public bool IsCurrentFightFinished()
        {
            if(Character1.IsAlive == false || Character2.IsAlive == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
