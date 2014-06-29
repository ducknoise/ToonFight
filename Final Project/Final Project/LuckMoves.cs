using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    class LuckMoves : Move
    {
        public Move[] Ability = new Move[4];
        public int NumOfMoves = 4;
        public void InitializeMoves(Toon OpponentToon)
        {
            Ability0();
            Ability1();
            Ability2();
            Ability3();
        }

        private void Ability3()
        {
            Ability[3] = new Move();
            Ability[3].Name = "Regen";
            Ability[3].Description = "Regenerates 15-30% of the \nresource connected to one \nnon-zero attribute \nat random";
            Ability[3].Duration = 1;
            Ability[3].CoolDown = 6;
            Ability[3].BeenActive = 2;
            Ability[3].Damage = 0;
            Ability[3].Cost = 0;
            Ability[3].Resource = "None";
            Ability[3].Effect = "";
        }

        private void Ability2()
        {
            Ability[2] = new Move();
            Ability[2].Name = "Coin Flip";
            Ability[2].Description = "You flip a magical coin which will either \ndeal damage equal to 10* your \nhighest attribute to your opponent, \nor half of that to yourself. \nGood Luck!";
            Ability[2].Duration = 1;
            Ability[2].CoolDown = 1;
            Ability[2].BeenActive = 2;
            Ability[2].Damage = 0;
            Ability[2].Cost = 0;
            Ability[2].Resource = "None";
            Ability[2].Effect = "";
        }
        private void Ability1()
        {
            Ability[1] = new Move();
            Ability[1].Name = "Heal";
            Ability[1].Description = "Heal yourself for a random amount \nbetween 5-15% of your Health!";
            Ability[1].Duration = 1;
            Ability[1].CoolDown = 6;
            Ability[1].BeenActive = 2;
            Ability[1].Damage = 0;
            Ability[1].Cost = 0;
            Ability[1].Resource = "None";
            Ability[1].Effect = "";
        }
        private void Ability0()
        {
            Ability[0] = new Move();
            Ability[0].Name = "None";
            Ability[0].Description = "You took a chance on Luck and it failed you. \nMaybe you should rely more on skill? \nCause this move will not be helping you \nAT ALL. Click on it. I dare you. \nSee what happens. (It'll be nothing)";
            Ability[0].Duration = 1;
            Ability[0].CoolDown = 0;
            Ability[0].BeenActive = 2;
            Ability[0].Damage = 0;
            Ability[0].Cost = 0;
            Ability[0].Resource = "None";
            Ability[0].Effect = "Nothing Happened. What'd you expect?";
        }
      }
}
