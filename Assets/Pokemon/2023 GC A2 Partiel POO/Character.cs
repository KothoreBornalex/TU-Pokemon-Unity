using System;
using UnityEngine;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;


        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive { get; private set; }


        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;

            if(_baseHealth > 0 )IsAlive = true;
            else
            {
                IsAlive = false;
            }

            SetHealth(baseHealth);
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; private set; }
        public TYPE BaseType { get => _baseType;}


        // Set Health through this function
        public void SetHealth(int value)
        {
            if (value < 0)
            {
                throw new Exception("Life cannot be negative: " + value);
            }

            CurrentHealth = Mathf.Clamp(value, 0, MaxHealth);

        }

        public void DecreaseHealth(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value Not Receivable !");
            }

            CurrentHealth = Mathf.Clamp(CurrentHealth - value, 0, MaxHealth);

            if(CurrentHealth <= 0 && IsAlive)
            {
                Death();
            }
        }


        public void IncreaseHealth(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value Not Receivable !");
            }

            CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
        }

        public void Death()
        {
            IsAlive = false;
        }
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return _baseHealth + (CurrentEquipment != null ? CurrentEquipment.BonusHealth : 0);
            }

        }

        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get
            {
                return _baseAttack + (CurrentEquipment != null ? CurrentEquipment.BonusAttack : 0);
            }
        }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get
            {
                return _baseDefense + (CurrentEquipment != null ? CurrentEquipment.BonusDefense : 0);
            }
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get
            {
                return _baseSpeed + (CurrentEquipment != null ? CurrentEquipment.BonusSpeed : 0);
            }
        }
        


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill attack)
        {
            if (attack != null)
            {
                if ((attack.Power - Defense) <= 0) return;
                DecreaseHealth(attack.Power - Defense);
            }
            else
            {
                throw new NotImplementedException("Character cannot receive null attack !");
            }
        }

        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if(newEquipment != null)
            {
                CurrentEquipment = newEquipment;
            }
            else
            {
                throw new ArgumentNullException("Character cannot equip null equipement !");
            }
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            CurrentEquipment = null;

            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        }

    }
}
