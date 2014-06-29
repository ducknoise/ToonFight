using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    class AgMoves
    {
      public   Move[] Ability = new Move[5];
        public int NumOfMoves = 5;
        public void InitializeMoves(Toon OtherToon,Extras extra, Toon ActiveToon)
        {
            Ability0(OtherToon);
            Ability1(extra);
            Ability2(OtherToon);
            Ability3(extra, ActiveToon);
            Ability[4] = new Move();
            Ability[4].Resource = "Energy";
            Ability[4].Name = "Exploit Chink";
            Ability[4].Description = "You find a chink in your opponents armor \nallowing you to bypass 50% of \ndefenses for 2 turns";
            Ability[4].Duration = 3;
            Ability[4].BeenActive = 5;
            Ability[4].CoolDown = 5;
            Ability[4].Damage = 0;
            Ability[4].Cost = 60;
            Ability[4].Effect = ActiveToon.Name + " found a chink \nin " + OtherToon.Name + "'s armor!";
            Ability[4].BuffIcon = extra.ChinkPic;
        }

        private void Ability3(Extras extra, Toon ActiveToon)
        {
            Ability[3] = new Move();
            Ability[3].Resource = "Energy";
            Ability[3].Name = "Stealth";
            Ability[3].Description = "You walk into the shadows, \nbecoming untargetable \nfor the next 3 turns.";
            Ability[3].Duration = 3;
            Ability[3].BeenActive = 5;
            Ability[3].CoolDown = 7;
            Ability[3].Damage = 0;
            Ability[3].Cost = 85;
            Ability[3].Effect = ActiveToon.Name + " has walked into the shadows.";
            Ability[3].BuffIcon = extra.StealthPic;
        }

        private void Ability2(Toon OtherToon)
        {
            Ability[2] = new Move();
            Ability[2].Name = "Air Dive";
            Ability[2].Resource = "Energy";
            Ability[2].Description = "You grab your opponent and jump \nin the air diving onto the \nground, releasing in the last \nmoment to minimize your \ndamage. Although both you and your \nopponent are hurt, your \nopponent recieves twice as \nmuch damage.";
            Ability[2].Duration = 1;
            Ability[2].CoolDown = 5;
            Ability[2].Damage = 0;
            Ability[2].BeenActive = 5;
            Ability[2].Cost = 25;
            Ability[2].Effect = "Both you and " + OtherToon.Name + " go down in pain!";
        }

        private void Ability1(Extras extra)
        {
            Ability[1] = new Move();
            Ability[1].Resource = "Energy";
            Ability[1].Name = "Rain Of Arrows";
            Ability[1].Description = "You shoot 4 arrows into the \nair which will come down one at a time and \nhit over the next 4 turns.";
            Ability[1].Duration = 4;
            Ability[1].BeenActive = 5;
            Ability[1].CoolDown = 0;
            Ability[1].Damage = 15;
            Ability[1].Cost = 38;
            Ability[1].Effect = "Four arrows have been launched into \nthe air";
            Ability[1].BuffIcon = extra.RainOfArrowsPic;
        }

        private void Ability0(Toon OtherToon)
        {
            Ability[0] = new Move();
            Ability[0].Resource = "Energy";
            Ability[0].Name = "Kick in the Groin";
            Ability[0].Description = "The most basic of Energy atacks. You run up \nto your opponent as if you are going to \npunch them, and when they go to defend \nand you quickly kick him or her in the \ngroin";
            Ability[0].Duration = 1;
            Ability[0].CoolDown = 0;
            Ability[0].Damage = 25;
            Ability[0].Cost = 7;
            Ability[0].Effect = OtherToon.Name + " kneels over in pain...cheap shot \nman...uncool";
        }
    }
}
