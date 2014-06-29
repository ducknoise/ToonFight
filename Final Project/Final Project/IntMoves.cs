using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    class IntMoves
    {
        public Move[] Ability = new Move[4];
        public int NumOfMoves = 4;
        public void InitializeMoves(Toon OtherToon, Toon ActiveToon,Extras extra)
        {
            Ability0(OtherToon);
            Ability1(extra);
            Ability2(OtherToon);
            Ability3(OtherToon, ActiveToon, extra);

        }

        private void Ability3(Toon OtherToon, Toon ActiveToon, Extras extra)
        {
            Ability[3] = new Move();
            Ability[3].Name = "Abyssal Expedition";
            Ability[3].Resource = "Mana";
            Ability[3].Description = "You send your opponent into \nan abyss for 2 turns in which \nhe cant attack and attains damage \nequal to 60% of \n the Mana cost";
            Ability[3].Duration = 2;
            Ability[3].CoolDown = 7;
            Ability[3].Damage = 0;
            Ability[3].BeenActive = 5;
            Ability[3].Cost = (int)(ActiveToon.MaxMana * 0.9);
            if (Ability[3].Cost < 290)
                Ability[3].Cost = 290;
            Ability[3].Effect = "You have sent " + OtherToon.Name + " into the Abyss";
            Ability[3].DamageType = 1;
            Ability[3].BuffIcon = extra.AbyssPic;
        }

        private void Ability2(Toon OtherToon)
        {
            Ability[2] = new Move();
            Ability[2].Name = "Soul Link";
            Ability[2].Resource = "Mana";
            Ability[2].Description = "You place a soul link \nbetween you and your opponent and \ncommit self-harm for a \nrandom amount of damage, causing damage \nto yourself but 2 times \nthe damage to your \nopponent.";
            Ability[2].Duration = 1;
            Ability[2].CoolDown = 5;
            Ability[2].Damage = 0;
            Ability[2].BeenActive = 5;
            Ability[2].Cost = 75;
            Ability[2].Effect = "Both you and " + OtherToon.Name + " go down in pain!";
        }

        private void Ability1(Extras extra)
        {
            Ability[1] = new Move();
            Ability[1].Name = "Curse of Witches";
            Ability[1].Resource = "Mana";
            Ability[1].Description = "You cast a curse upon your opponent \nwhich will inflict damage every turn for \nthe next 3 turns.";
            Ability[1].Duration = 4;
            Ability[1].CoolDown = 0;
            Ability[1].Damage = 15;
            Ability[1].BeenActive = 5;
            Ability[1].Cost = 150;
            Ability[1].Effect = "The curse has been cast!";
            Ability[1].BuffIcon = extra.CurseofWhitchesPic; 
        }

        private void Ability0(Toon OtherToon)
        {
            Ability[0] = new Move();
            Ability[0].Name = "Mana Blast";
            Ability[0].Resource = "Mana";
            Ability[0].Description = "The most basic of Mana atacks. You focus \nthe Mana in your body into your hands \nto give it a physical form, at which point \nyou launch it at your enemy.";
            Ability[0].Duration = 1;
            Ability[0].CoolDown = 0;
            Ability[0].Damage = 25;
            Ability[0].Cost = 50;
            Ability[0].Effect = "The blast went straight through " + OtherToon.Name + "! \nImpressive!";
        }
    }
}
