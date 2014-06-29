using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    class StrMoves
    {
       public  Move[] Ability = new Move[4];
        public int NumOfMoves = 4;
        public void InitializeMoves(Toon OtherToon, Toon ActiveToon,Extras extra)
        {
            Ability0(OtherToon);
            Ability1(OtherToon,extra);
            Ability2(OtherToon);
            Ability[3] = new Move();
            Ability[3].Name = "Shout Of Power";
            Ability[3].Resource = "Rage";
            Ability[3].Description = "You let out a terrible shout \nwhich multiplies all of \nyour damage by 3 for 2 turns";
            Ability[3].Duration = 3;
            Ability[3].CoolDown = 5;
            Ability[3].Damage = 0;
            Ability[3].BeenActive = 5;
            Ability[3].Cost = (int)(ActiveToon.MaxRage * .9);
            if (Ability[3].Cost < 110)
                Ability[3].Cost = 110;
            Ability[3].Effect = "Both you and " + OtherToon.Name + " go down in pain!";
            Ability[3].BuffIcon = extra.SOPBuffPic;
        }

        private void Ability2(Toon OtherToon)
        {
            Ability[2] = new Move();
            Ability[2].Name = "Head Butt";
            Ability[2].Resource = "Rage";
            Ability[2].Description = "You Head Butt your \nopponent hurting yourself, \nbut sending over twice \nas much damage.";
            Ability[2].Duration = 1;
            Ability[2].CoolDown = 5;
            Ability[2].Damage = 0;
            Ability[2].BeenActive = 5;
            Ability[2].Cost = 20;
            Ability[2].Effect = "Both you and " + OtherToon.Name + " go down in pain!";
        }

        private void Ability1(Toon OtherToon,Extras extra)
        {
            Ability[1] = new Move();
            Ability[1].Name = "Concussion";
            Ability[1].Resource = "Rage";
            Ability[1].Description = "A severe Strike to the head will \ngive " + OtherToon.Name + " head trauma and \nwill cause damage over the next 4 turns";
            Ability[1].Duration = 4;
            Ability[1].CoolDown = 0;
            Ability[1].BeenActive = 5;
            Ability[1].Damage = 15;
            Ability[1].Cost = 25;
            Ability[1].Effect = OtherToon.Name + " is bleeding internally";
            Ability[1].BuffIcon = extra.ConcussionPic;
        }

        private void Ability0(Toon OtherToon)
        {
            Ability[0] = new Move();
            Ability[0].Name = "Strike";
            Ability[0].Resource = "Rage";
            Ability[0].Description = "The most basic of Strength atacks. You \nfocus the Rage in your body into your hands \nmaking them strong like Iron, and strike \nyour enemy down";
            Ability[0].Duration = 1;
            Ability[0].CoolDown = 0;
            Ability[0].BeenActive = 2;
            Ability[0].Damage = 26;
            Ability[0].Cost = 15;
            Ability[0].Effect = OtherToon.Name + " got hit hard...ouch!";
        }
    }
}
