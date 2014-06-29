using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Final_Project
{
    class Toon
    {
        Random rand = new Random();
        public int Intellect= 0;
        public int Strength = 0;
        public int Agility = 0;
        public int Defense = 0;
        public int HP = 0;
        public string Name = "";
        public string Gender = "";
        public Texture2D sprite;
        public int AvailablePoints = 20;
        public int Mana = 0;
        public int Rage = 0;
        public int Energy = 0;
        public string Pronoun;
        public double MaxRage = 1;
        public double MaxEnergy = 1;
        public double MaxMana = 1;
        public double MaxHP;
        public int MaxDef;
        public int DodgeChance = 100;
        public int HitChance;
        public string[] moveClass = { "", "", "", "" };
        public int [] CD = new int [4];
        public Move[] Ability = new Move[4];
        double boost;
        public bool KeepGoing = true;

        public bool InAbyss = false;
        public bool PowerShout = false;
        public bool Stealthed = false;
        public bool ChinkFound = false;

        int Reason; //reason for not being able to keep going
        public void MoveCost(Move SelectedMove, int MoveIndex)
        {
            switch (SelectedMove.Resource)
            {
                case "Mana":
                    Mana -= SelectedMove.Cost;
                    break;
                case "Rage":
                    Rage -= SelectedMove.Cost;
                    break;
                case "Energy":
                    Energy -= SelectedMove.Cost;
                    break;
            }
            CD[MoveIndex] = SelectedMove.CoolDown;
        }

        public void EnergyRegen()
        {
            Energy+= (int)((Agility *5 +15) * (0.2));
            if (Energy > MaxEnergy)
                Energy = (int)MaxEnergy;
        }
        public void RageRegen( int DamageRecieved)
        {
            if (DamageRecieved != 0 && Strength != 0)
            {
                double increase = ((((DamageRecieved * Strength) * ((21 - (double)Strength) / 21))) / ((double)DamageRecieved / 17));
                if ((int)increase < 5)
                    increase = 5;
                Rage += (int)increase;
                if (Rage > MaxRage)
                    Rage = (int)MaxRage;

            }
            else if (Strength != 0)
                Rage += 5;
            
        }
        public void ManageRegen()
        {
            Mana += 60;
            if (Mana > MaxMana)
                Mana = (int)MaxMana;
        }
        public void DamageTaken(double DamageRecieved, out double DAMAGE)
        {
            int Damage = (int)DamageRecieved;
            double DefenseEffectiveness = rand.NextDouble();
            DefenseEffectiveness = Math.Pow(DefenseEffectiveness, 2);
            if (DefenseEffectiveness !=0 && Damage !=0)
            Damage = (int)(DamageRecieved - ((DamageRecieved/2) * ((double)Defense/1500) * DefenseEffectiveness));
            if (Damage < 0)
                Damage = 0;
             HitChance = rand.Next(0, DodgeChance);
            if (HitChance == 0)
                Damage = 0;
            HP -= Damage;
            RageRegen( Damage);
            DAMAGE =Damage;
        }
        public void DamageSent(Move SelectedMove, out double Damage, int[] MoveDamage, Toon OtherToon)
        {
            Damage = 0;
                       
            for (int i = 0; i < 4; i++)
            {
                boost = 0;
                DamageAddition(SelectedMove, out  Damage, ref Damage, i, MoveDamage, OtherToon);
            }


          
        }

        private void DamageAddition(Move SelectedMove, out double Damage, ref double damage, int OnAbility, int[] MoveDamage, Toon OtherToon)
        {
            Damage = damage;
            
            if (Ability[OnAbility].BeenActive < Ability[OnAbility].Duration)
            {
                
                int Effectiveness = rand.Next(50, 101);
                switch (Ability[OnAbility].Resource)
                {
                    case "Mana":
                        boost = (SelectedMove.Damage / (21 - Intellect)) * Effectiveness / 100;
                        Damage += (int)(Ability[OnAbility].Damage + boost);
                        Ability[OnAbility].BeenActive++;
                        ExtraGoodies(OnAbility, OtherToon);
                        Damage += boost;
                        MoveDamage[OnAbility] = (int)(Ability[OnAbility].Damage + boost);
                        break;
                    case "Rage":
                        boost = (Ability[OnAbility].Damage / (21 - Strength)) * Effectiveness / 100;
                        Damage += Ability[OnAbility].Damage + boost;
                        Ability[OnAbility].BeenActive++;
                        ExtraGoodies(OnAbility, OtherToon);
                        Damage += boost;
                        MoveDamage[OnAbility] = (int)(Ability[OnAbility].Damage + boost);
                        break;
                    case "Energy":
                        boost = (Ability[OnAbility].Damage / (21 - Agility)) * Effectiveness / 100;
                        Damage += Ability[OnAbility].Damage + boost;
                        Ability[OnAbility].BeenActive++;
                        ExtraGoodies(OnAbility, OtherToon);
                        Damage += boost;
                        MoveDamage[OnAbility] = (int)(Ability[OnAbility].Damage + boost);
                        break;
                    case "None":
                        ExtraGoodies(OnAbility, OtherToon);
                        Ability[OnAbility].BeenActive++;
                        Damage += boost;
                        MoveDamage[OnAbility] = (int)(Ability[OnAbility].Damage + boost);
                        break;
                }
            }
        }

        public void ExtraGoodies(int OnAbility, Toon OtherToon)
        {
            int Highest;
            switch (Ability[OnAbility].Name)
            {
                case "Heal":
                    Ability[OnAbility].HealCode(out HP, ref HP, Name);
                    if (HP > MaxHP)
                        HP = (int)MaxHP;
                    break;
                case "Coin Flip":
                    Highest = Math.Max(Intellect,Math.Max(Agility,Strength));
                    Ability[OnAbility].CoinFlipCode(Highest, out boost, out HP, ref HP);
                    break;
                case "Regen":
                    int manaRegen;int energyRegen;int rageRegen;
                    Ability[OnAbility].RegenCode(ref MaxMana, ref MaxEnergy, ref MaxRage, out manaRegen, out rageRegen, out energyRegen);
                    Mana += manaRegen;
                    Rage += rageRegen;
                    Energy += energyRegen;
                    break;
                case "Soul Link":
                    Ability[OnAbility].DualDamageCode(out HP, ref HP, out boost);
                    break;
                case"Head Butt":
                    Ability[OnAbility].DualDamageCode(out HP, ref HP, out boost);
                    break;
                case "Air Dive":
                    Ability[OnAbility].DualDamageCode(out HP, ref HP, out boost);
                    break;
                case "Abyssal Expedition":
                    Ability[OnAbility].AbyssalExpeditionCode(out HP, ref HP, out boost, ref MaxMana, OtherToon);
                    break;
                case "Shout Of Power":
                    Ability[OnAbility].GenericBuffCode(out PowerShout);
                        break;
                case "Stealth":
                        Ability[OnAbility].StealthCode(out Stealthed, out DodgeChance);
                    break;
                case "Exploit Chink":
                    Ability[OnAbility].GenericBuffCode(out ChinkFound);
                    break;
            }
        }

        public void KeepGoingCheck(Toon OtherToon)
        {
            if (OtherToon.InAbyss == true)
            {
                KeepGoing = false;
                Reason = 0;
            }
            if (InAbyss == true)
            {
                KeepGoing = false;
                Reason = 1;
            }
        }

        public void SpecialDraw(SpriteBatch spriteBatch, Extras extra, Toon OtherToon)
        {
            switch (Reason)
            {
                case 0:
                spriteBatch.DrawString(extra.NormalFont, Name +" Could not do anything \nbecause " +OtherToon.Name + " is in the Abyss", new Vector2(590, 465), Color.White);
            break;
                case 1:
            spriteBatch.DrawString(extra.NormalFont, Name + " Could not do anything \nbecause "+ Pronoun +" is in \nthe Abyss", new Vector2(590, 465), Color.White);
            break;
            }
        }

        public void Buffs(Toon OtherToon, out double Damage, ref double damage, Move SelectedMove)
        {
            Damage = damage;
            if (PowerShout == true)
            {
                Damage = Damage * 3;
            }

            if (ChinkFound == true)
            {
                OtherToon.Defense = OtherToon.MaxDef / 2;
            }
            else
            {
                OtherToon.Defense = OtherToon.MaxDef;
            }
        
        }
        public void BuffDraw(SpriteBatch spriteBatch,Extras extra, int X)
        {
            int NumOfRects = 0;
            Rectangle[] BuffRects = new Rectangle[4];

            for (int i = 0; i < 4; i++)
            {
                if (Ability[i].BuffIcon != null && Ability[i].Duration > 1 && Ability[i].BeenActive < Ability[i].Duration)
                {
                    BuffRects[NumOfRects] = new Rectangle(X, (70 + 35 * NumOfRects), 30, 30);
                    spriteBatch.Draw(Ability[i].BuffIcon, BuffRects[NumOfRects], Color.White);
                    NumOfRects++;
                }
            }

        }

       }
}
