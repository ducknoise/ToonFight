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
    class LoadScreen
    {
        
        enum LoadState
        {
            Initialize, Present, Chosen
        }
        LoadState State = LoadState.Initialize;
        int OnToon = 0;
        Random rand = new Random();
        
        public void Code(Toon[] Character,Extras extra,KeyboardState keyboard, KeyboardState oldkeyboard, Toon ChosenOne, Toon OpponentToon, MouseState mouse, Point mousepoint)
        {
            switch (State)
            {
                case LoadState.Initialize:
                ToonLoading(Character, extra);
                    break;
                case LoadState.Present:
                    PresentCode(keyboard, oldkeyboard, Character, ChosenOne,OpponentToon, extra, mouse,mousepoint);
                    break;
                case LoadState.Chosen:
                    ChosenCode(Character, ChosenOne, OpponentToon, extra);
                    break;
            }
            
        }
        public void Draw(Toon[] Character, SpriteBatch spriteBatch, Extras extra)
        {
            switch (State)
            {
                case LoadState.Initialize:
                    break;
                case LoadState.Present:
                    PresentDraw(spriteBatch, extra, Character);
                    break;
            }
        }

        private  void ToonLoading(Toon[] Character, Extras extra)
        {
            
            for (int i = 0; i < extra.NumOfToons; i++)
            {
                Character[i] = new Toon();
                string CharacterInfo = File.ReadAllText(@"Content\TextFiles\Toon" + (i+1).ToString() + ".txt");
                string text;
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].Name = text;
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].Gender = text;
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].Agility = int.Parse(text);
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].Intellect = int.Parse(text);
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].Strength = int.Parse(text);
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].HP = int.Parse(text);
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].Defense = int.Parse(text);
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].moveClass[0] = text;
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].moveClass[1] = text;
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].moveClass[2] = text;
                FindText(ref CharacterInfo, out CharacterInfo, out text);
                Character[i].moveClass[3] = text;
                if (Character[i].Gender == "M")
                {
                    Character[i].sprite = extra.MalePic;
                    Character[i].Pronoun = "he";
                }
                if (Character[i].Gender == "F")
                {
                    Character[i].sprite = extra.FemalePic;
                    Character[i].Pronoun = "she";
                }
                State = LoadState.Present;
                Character[i].Mana = 400 * (Character[i].Intellect) / 10 + 10;
                Character[i].Rage = Character[i].Strength * 10 + 10;
                Character[i].Energy = Character[i].Agility * 5 + 10;
                if (Character[i].Intellect == 0)
                    Character[i].Mana = 1;
                if (Character[i].Agility == 0)
                    Character[i].Energy = 1;
                if (Character[i].Strength == 0)
                    Character[i].Rage = 1;
                
            }
            OnToon = 0;

        }
        private void FindText(ref string INfullText, out string OUTfullText, out string Info)
        {
            Info = "";
            string text = INfullText;
            int k = 0;
            int textLength = text.Length;
            for (int i = 0; i < textLength; i++)
            {
                if (text[i].ToString() == ";")
                {
                    Info = text.Substring(0, i);
                    k = i + 1;
                    i = textLength + 1;
                }

            }
            OUTfullText = text.Substring(k, textLength - k);
        }

        private void PresentCode(KeyboardState keyboard, KeyboardState oldkeyboard, Toon [] Character, Toon ChosenOne, Toon OpponentToon, Extras extra, MouseState mouse, Point mousepoint)
        {
            PresentNavigation(ref keyboard, ref oldkeyboard, extra);
            if (keyboard.IsKeyDown(Keys.Enter) && oldkeyboard.IsKeyUp(Keys.Enter))
            {
                State = LoadState.Chosen;
            }
            Rectangle DeleteRect = new Rectangle(950, 680, 87, 30);
            if (DeleteRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed)
            {

                for (int i = OnToon + 1; i < (extra.NumOfToons); i++)
                {
                    string ToonInfo = File.ReadAllText(@"Content\TextFiles\Toon" + (i + 1).ToString() + ".txt");
                    File.WriteAllText(@"Content\TextFiles\Toon" + i.ToString() + ".txt", ToonInfo);
                }
                File.Delete(@"Content\TextFiles\Toon" + extra.NumOfToons.ToString() + ".txt");
                extra.NumOfToons--;
                File.WriteAllText(@"Content\TextFiles\Character Count.txt", extra.NumOfToons.ToString());
                State = LoadState.Initialize;
                extra.State = Extras.GameState.IntroScreen;
            }

        }
        private void PresentNavigation(ref KeyboardState keyboard, ref KeyboardState oldkeyboard, Extras extra)
        {
            if (keyboard.IsKeyDown(Keys.Left) && oldkeyboard.IsKeyUp(Keys.Left))
            {
                if (OnToon == 0)
                    OnToon = (extra.NumOfToons - 1);
                else
                    OnToon--;
            }
            if (keyboard.IsKeyDown(Keys.Right) && oldkeyboard.IsKeyUp(Keys.Right))
            {
                if ((OnToon + 1) == extra.NumOfToons)
                    OnToon = 0;
                else
                    OnToon++;
            }
        }
        private void PresentDraw(SpriteBatch spriteBatch,Extras extra, Toon [] Character)
        {
            spriteBatch.Draw(extra.ToonSelectionScreen, new Rectangle(0, 0, 1080, 720), Color.White);
            if (extra.NumOfToons >= 3)
            {
                int Prev;
                if (OnToon == 0)
                    Prev = extra.NumOfToons -1;
                else
                    Prev = OnToon - 1;
                int Next;
                if (OnToon == (extra.NumOfToons-1))
                    Next = 0;
                else
                    Next = OnToon + 1;
                spriteBatch.Draw(Character[Prev].sprite, new Rectangle(275, 60, 100, 225), Color.White);
                spriteBatch.DrawString(extra.NormalFont, Character[Prev].Name, new Vector2(295, 290), Color.White);
                spriteBatch.Draw(Character[Next].sprite, new Rectangle(715, 60, 100, 225), Color.White);
                spriteBatch.DrawString(extra.NormalFont, Character[Next].Name, new Vector2(730, 290), Color.White);
                spriteBatch.DrawString(extra.NormalFont, "[DELETE]", new Vector2(950, 680), Color.White);
                

            }
            spriteBatch.Draw(Character[OnToon].sprite, new Rectangle(460, 20, 200, 450), Color.White);
            spriteBatch.DrawString(extra.TitleFont, Character[OnToon].Name, new Vector2(230, 605), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Use Arrow Keys To Navigate", new Vector2(30, 30), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Intellect: " + Character[OnToon].Intellect.ToString(), new Vector2(30, 100), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Strength: " + Character[OnToon].Strength.ToString(), new Vector2(30, 130), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Agility: " + Character[OnToon].Agility.ToString(), new Vector2(30, 160), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "HP: " + Character[OnToon].HP.ToString(), new Vector2(30, 190), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Defense: " + Character[OnToon].Defense.ToString(), new Vector2(30, 220), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Mana: " + Character[OnToon].Mana.ToString(), new Vector2(30, 250), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Rage: " + Character[OnToon].Rage.ToString(), new Vector2(30, 280), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Energy: " + Character[OnToon].Energy.ToString(), new Vector2(30, 310), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Class 1: " + Character[OnToon].moveClass[0], new Vector2(30, 340), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Class 2: " + Character[OnToon].moveClass[1], new Vector2(30, 370), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Class 3: " + Character[OnToon].moveClass[2], new Vector2(30, 400), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Class 4: " + Character[OnToon].moveClass[3], new Vector2(30, 430), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "[DELETE]", new Vector2(950, 680), Color.White);
        }

        private void ChosenCode(Toon [] Character, Toon ChosenOne, Toon OpponentToon, Extras extra)
        {
            TransferAttributes(Character, ChosenOne, OnToon);
            int OpponentNum = rand.Next(0, extra.NumOfToons);
            if (OpponentNum != OnToon)
            {
                TransferAttributes(Character, OpponentToon, OpponentNum);
                extra.State = Extras.GameState.FIGHT;
                State = LoadState.Initialize;
                
            } 
        }
        private void TransferAttributes(Toon[] Character, Toon Blank, int Index)
        {
            Blank.Ability = Character[Index].Ability;
            Blank.Agility = Character[Index].Agility;
            Blank.AvailablePoints = Character[Index].AvailablePoints;
            Blank.Defense = Character[Index].Defense;
            Blank.Gender = Character[Index].Gender;
            Blank.HP = Character[Index].HP;
            Blank.Intellect = Character[Index].Intellect;
            Blank.Mana = Character[Index].Mana;
            Blank.Name = Character[Index].Name;
            Blank.Rage = Character[Index].Rage;
            Blank.Energy = Character[Index].Energy;
            Blank.Strength = Character[Index].Strength;
            Blank.sprite = Character[Index].sprite;
            Blank.moveClass[0] = Character[Index].moveClass[0];
            Blank.moveClass[1] = Character[Index].moveClass[1];
            Blank.moveClass[2] = Character[Index].moveClass[2];
            Blank.moveClass[3] = Character[Index].moveClass[3];
            Blank.MaxEnergy = Character[Index].Energy;
            Blank.MaxMana = Character[Index].Mana;
            Blank.MaxRage = Character[Index].Rage;
            Blank.MaxHP = Character[Index].HP;
            Blank.Pronoun = Character[Index].Pronoun;
            Blank.MaxDef = Character[Index].Defense;
            
        }
    }
}
