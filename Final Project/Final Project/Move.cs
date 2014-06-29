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
    class Move
    {
        public string Name;
        public int Cost;
        public double Damage;
        public string Description;
        public int Duration;
        public int BeenActive = 1;
        public int CoolDown;
        public string Effect;
        public string Resource;
        public int DamageType = 0;
        public Texture2D BuffIcon;
        Random rand = new Random();
        public void HealCode(out int HP, ref int hp, string Name)
        {
            HP = hp;
            int amountHealed = rand.Next(5, 15);
            double Healed = ((double)amountHealed / 100) * HP;
            HP += (int)Healed;
            Effect = Name+  " has healed for " + amountHealed.ToString() + "% \nof your health!...or " + Healed.ToString() + " HP!";
        }
        public void CoinFlipCode(int Highest, out double Damage, out int HP, ref int hp)
        {
            Damage = 0;
            HP = hp;
            int Coin = rand.Next(0, 2);
            Coin = 0;
            if (Coin ==0)
            {
                Damage = Highest * 10;
                Effect = "Luck is in your favor! Your opponent \nhas lost " + Damage + " health!";
            }
            if (Coin ==1)
            {
                Damage = (int)((Highest * 10) / 2);
                HP -= (int)Damage;
                Effect = "OUCH...Luck was not in your favor, you have \nlost " + Damage + " health. \nYou are now at " + HP + " HP";
            }
            
        }
        public void RegenCode(ref double maxMana, ref double maxEnergy, ref double maxRage, out int manaRegen, out int rageRegen, out int energyRegen)
        {
            manaRegen = 0;
            rageRegen = 0;
            energyRegen = 0;
            double RegenPercent = rand.Next(15, 31);
            bool Done = false;
            while(Done == false)
            {
            int Resource = rand.Next(0, 3);
            switch (Resource)
            {
                case 0:
                    if (maxMana != 1)
                    {
                        manaRegen = (int)(maxMana * RegenPercent) / 100;
                        Done = true;
                        Effect = manaRegen + " Mana was regenerated!"; 
                    }
                    break;
                case 1:
                    if (maxRage != 1)
                    {
                        rageRegen = (int)(maxRage * RegenPercent) / 100;
                        Done = true;
                        Effect = rageRegen + " Rage was regenerated!";
                    }
                    break;
                case 2:
                    if (maxEnergy != 1)
                    {
                        energyRegen = (int)(maxEnergy * RegenPercent) / 100;
                        Done = true;
                        Effect = energyRegen + " Energy was regenerated!";
                    }
                    break;
            }
            }

        }
        public void DualDamageCode(out int HP, ref int hp,out double Damage)
        {
            HP = hp;
            Damage = Math.Sqrt(rand.Next(1000, 100000));
            double PlayerLose = Damage /2;
            HP -= (int)PlayerLose;
            Effect = "You have \nlost " + (int)PlayerLose + " HP, \nbut have sent over \n" + (int)Damage + " damage!";
        }
        public void AbyssalExpeditionCode(out int HP, ref int hp, out double Damage, ref double MaxMana, Toon OtherToon)
        {
            OtherToon.InAbyss = true;
            if (BeenActive == 2)
                OtherToon.InAbyss = false;
            Damage = 0;
            HP = hp;
            Damage = (MaxMana) * 0.3;       
        }
       
        public void StealthCode(out bool Stealthed, out int HitChance)
        {
            Stealthed = true;
            HitChance = 0;
            if (BeenActive == 3)
            {
                HitChance = 1000;
                Stealthed = false;
            }
        }
        public void GenericBuffCode(out bool BuffBool)
        {
            BuffBool = true;
            if (BeenActive == 3)
                BuffBool = false;
        }
     }
}
