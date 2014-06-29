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
    class CreationScreen
    {
        Toon toon = new Toon();
        Color color1; //used for selection
        Color color2;
        Texture2D AttributesScreen;
        enum ScreenState
        {
            Initialize,Name,Gender,Attributes, DefenseHP, MoveTypes,Done
        }
        int HPDefX = 523;
        bool clicked = false; 
        
        ScreenState State = ScreenState.Initialize;
        public void Code(Extras extra, KeyboardState keyboard, KeyboardState oldkeyboard, MouseState mouse, MouseState oldmouse, Point mousepoint, Toon [] Character)
        {
            switch (State)
            {
                case ScreenState.Initialize:
                    Initialize();
                    break;
                case ScreenState.Name:
                    NamingCode(extra, keyboard, oldkeyboard);
                    break;
                case ScreenState.Gender:
                    GenderCode(mouse, oldmouse, mousepoint);
                    break;
                case ScreenState.Attributes:
                    AttributesCode(mouse, oldmouse, mousepoint, extra);
                    break;
                case ScreenState.DefenseHP:
                    DefenseHPCode(mouse, oldmouse, mousepoint);
                    break;
                case ScreenState.MoveTypes:
                    MoveTypesCode(mouse, oldmouse, mousepoint);
                    break;
                case ScreenState.Done:
                    DoneCode(extra, Character);
                        break;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Extras extra)
        {
            switch (State)
            {
                case ScreenState.Name: 
                     NamingDraw(spriteBatch, extra);
                    break;
              case ScreenState.Gender:
                    GenderDraw(spriteBatch, extra);
                    break;
                case ScreenState.Attributes:
                    AttributesDraw(spriteBatch, extra, toon);
                    break;
                case ScreenState.DefenseHP:
                    DefenseHPDraw(spriteBatch, extra);
                    break;
                case ScreenState.MoveTypes:
                    MoveTypesDraw(spriteBatch, extra);
                    break;
                    
            }
        }

        private void Initialize()
        {
            toon.Name = "";
            toon.AvailablePoints = 20;
            toon.Agility = 0;
            toon.Intellect = 0;
            toon.Strength = 0;
            State = ScreenState.Name;
            toon.moveClass[0] = "";
            toon.moveClass[1] = "";
            toon.moveClass[2] = "";
            toon.moveClass[3] = "";
            
        }

        private void NamingCode(Extras extra, KeyboardState keyboard, KeyboardState oldkeyboard)
        {
            AttributesScreen = extra.AttributesScreen; //this is for later
            if (keyboard.IsKeyDown(Keys.Enter) && oldkeyboard.IsKeyUp(Keys.Enter))
                State = ScreenState.Gender;
            if (toon.Name.Length == 6)
                toon.Name = toon.Name.Substring(0, toon.Name.Length - 1);
            bool shift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);
            Keys key = Keys.None;
            if (keyboard.GetPressedKeys().Count() > 0)
                key = keyboard.GetPressedKeys()[0];
            if (key != Keys.None && oldkeyboard.IsKeyUp(key))
            {
                switch (key)
                {
                    case Keys.A:
                        toon.Name += shift ? "A" : "a";
                        break;
                    case Keys.B:
                        toon.Name += shift ? "B" : "b";
                        break;
                    case Keys.C:
                        toon.Name += shift ? "C" : "c";
                        break;
                    case Keys.D:
                        toon.Name += shift ? "D" : "d";
                        break;
                    case Keys.E:
                        toon.Name += shift ? "E" : "e";
                        break;
                    case Keys.F:
                        toon.Name += shift ? "F" : "f";
                        break;
                    case Keys.G:
                        toon.Name += shift ? "G" : "g";
                        break;
                    case Keys.H:
                        toon.Name += shift ? "H" : "h";
                        break;
                    case Keys.I:
                        toon.Name += shift ? "I" : "i";
                        break;
                    case Keys.J:
                        toon.Name += shift ? "J" : "j";
                        break;
                    case Keys.K:
                        toon.Name += shift ? "K" : "k";
                        break;
                    case Keys.L:
                        toon.Name += shift ? "L" : "l";
                        break;
                    case Keys.M:
                        toon.Name += shift ? "M" : "m";
                        break;
                    case Keys.N:
                        toon.Name += shift ? "N" : "n";
                        break;
                    case Keys.O:
                        toon.Name += shift ? "O" : "o";
                        break;
                    case Keys.P:
                        toon.Name += shift ? "P" : "p";
                        break;
                    case Keys.Q:
                        toon.Name += shift ? "Q" : "q";
                        break;
                    case Keys.R:
                        toon.Name += shift ? "R" : "r";
                        break;
                    case Keys.S:
                        toon.Name += shift ? "S" : "s";
                        break;
                    case Keys.T:
                        toon.Name += shift ? "T" : "t";
                        break;
                    case Keys.U:
                        toon.Name += shift ? "U" : "u";
                        break;
                    case Keys.V:
                        toon.Name += shift ? "V" : "v";
                        break;
                    case Keys.W:
                        toon.Name += shift ? "W" : "w";
                        break;
                    case Keys.X:
                        toon.Name += shift ? "X" : "x";
                        break;
                    case Keys.Y:
                        toon.Name += shift ? "Y" : "y";
                        break;
                    case Keys.Z:
                        toon.Name += shift ? "Z" : "z";
                        break;

                    case Keys.Space:
                        toon.Name += " ";
                        break;
                    case Keys.Back:
                        if (toon.Name.Length != 0)
                            toon.Name = toon.Name.Substring(0, toon.Name.Length - 1);
                        break;
                }
            }
        }
        private void NamingDraw(SpriteBatch spriteBatch, Extras extra)
        {
            spriteBatch.Draw(extra.Pixel, new Rectangle(0, 0, 1080, 720), Color.Black);
            spriteBatch.DrawString(extra.NormalFont, "What Shall Your Name Be?", new Vector2(400, 30), Color.White);
            spriteBatch.DrawString(extra.TitleFont, toon.Name, new Vector2(400, 500), Color.White);
        }

        private void GenderCode(MouseState mouse, MouseState oldmouse, Point mousepoint)
        {

            Rectangle MaleRect = new Rectangle(0, 0, 540, 720);
            Rectangle FemaleRect = new Rectangle(540, 0, 540, 720);
            if (MaleRect.Contains(mousepoint))
            {
                color1 = Color.Yellow;
                if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                {
                    toon.Gender = "M";
                    State = ScreenState.Attributes;
                }
            }
            else
            {
                color1 = Color.Black;
            }
            if (FemaleRect.Contains(mousepoint))
            {
                color2 = Color.Yellow;
                if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                {
                    toon.Gender = "F";
                    State = ScreenState.Attributes;
                }
            }
            else
            {
                color2 = Color.Black;
            }
        }
        private void GenderDraw(SpriteBatch spriteBatch, Extras extra)
        {
          
            spriteBatch.Draw(extra.Pixel, new Rectangle(0, 0, 1080, 720), Color.Black);
            spriteBatch.Draw(extra.Pixel, new Rectangle(0, 0, 540, 720), color1);
            spriteBatch.Draw(extra.Pixel, new Rectangle(540, 0, 540, 720), color2);
            spriteBatch.Draw(extra.GenderSelectionScreen, new Rectangle(0, 0, 1080, 720), Color.White);

        }

        private void AttributesCode(MouseState mouse, MouseState oldmouse, Point mousepoint,  Extras extra)
        {
            Rectangle AgilityRect = new Rectangle(50, 560, 240, 85);
            Rectangle InteRect = new Rectangle(310, 565, 330,80 );
            Rectangle StrengthRect = new Rectangle(640, 560, 340, 85);
            if (StrengthRect.Contains(mousepoint))
            {
                AttributesScreen = extra.AttributesStr;
                if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                {
                    toon.Strength++;
                    toon.AvailablePoints--;
                }
            }
            else if (InteRect.Contains(mousepoint))
            {
                AttributesScreen = extra.AttributesInt;
                if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                {
                    toon.Intellect++;
                    toon.AvailablePoints--;
                }
            }
            else if (AgilityRect.Contains(mousepoint))
            {
                AttributesScreen = extra.AttributesAg;
                if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                {
                    toon.Agility++;
                    toon.AvailablePoints--;
                }
            }
            else
            {
                AttributesScreen = extra.AttributesScreen;
            }
            if (toon.AvailablePoints == 0)
                State = ScreenState.DefenseHP;
        }
        private void AttributesDraw(SpriteBatch spriteBatch, Extras extra, Toon toon)
        {
            spriteBatch.Draw(AttributesScreen, new Rectangle(0, 0, 1080, 720), Color.White);
            spriteBatch.DrawString(extra.TitleFont, toon.Agility.ToString(), new Vector2(105, 650), Color.Brown);
            spriteBatch.DrawString(extra.TitleFont, toon.Intellect.ToString(), new Vector2(420, 640), Color.Blue);
            spriteBatch.DrawString(extra.TitleFont, toon.Strength.ToString(), new Vector2(755, 650), Color.Red);
            spriteBatch.DrawString(extra.TitleFont, toon.AvailablePoints.ToString(), new Vector2(40, 270), Color.Green);
            
        }

        private void DefenseHPCode(MouseState mouse, MouseState oldmouse, Point mousepoint)
        {
            Rectangle NextRect = new Rectangle(30, 675, 300, 35);
            Rectangle HpDefenseRect = new Rectangle(500, 285, 50, 140);
            if (HpDefenseRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed)
                clicked = true;
           if (mouse.LeftButton == ButtonState.Released)
                clicked = false;
            if (clicked == true)
            { 
                HPDefX = mouse.X;
                if (mouse.X < 123)
                {
                    HPDefX = 125;
                }
                if (mouse.X > 923)
                {
                    HPDefX = 921;
                }
            }
            toon.Defense = 4*(HPDefX - 123);
            toon.HP = 4*(923 - HPDefX);
            if (NextRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed)
                State = ScreenState.MoveTypes;
                
        }
        private void DefenseHPDraw(SpriteBatch spriteBatch, Extras extra)
        {
            spriteBatch.Draw(extra.DefenseHpPic, new Rectangle(0, 0, 1080, 720), Color.White);
            spriteBatch.Draw(extra.Pixel, new Rectangle((HPDefX + 25), 303, (923 - HPDefX), 110), Color.Red);
            spriteBatch.Draw(extra.Pixel, new Rectangle(123, 303, (HPDefX - 123),  110), Color.Blue);
            spriteBatch.Draw(extra.Pixel,new Rectangle(HPDefX, 285, 50, 140), Color.Violet);
            spriteBatch.DrawString(extra.TitleFont, toon.Defense.ToString(), new Vector2(105, 115), Color.Blue);
            spriteBatch.DrawString(extra.TitleFont, toon.HP.ToString(), new Vector2(630, 115), Color.Red);
            spriteBatch.DrawString(extra.NormalFont, "[Click Here When Finished]", new Vector2(40, 680), Color.White);
        }

        private void MoveTypesCode(MouseState mouse, MouseState oldmouse, Point mousepoint)
        {
            Rectangle ManaRect = new Rectangle(80, 320, 370, 150);
            Rectangle EnergyRect = new Rectangle(70, 485, 380, 200);
            Rectangle RageRect = new Rectangle(500, 310, 385, 170);
            Rectangle LuckRect = new Rectangle(500, 495, 425, 200);
            Rectangle ONErect = new Rectangle(200, 30, 65, 30);
            Rectangle TWOrect = new Rectangle(272, 30, 55, 30);
            Rectangle THREErect = new Rectangle(340, 30, 75, 30);
            Rectangle FOURrect = new Rectangle(427, 30, 65, 30);
            Rectangle ALLrect = new Rectangle(500,30,63,30);
            Rectangle DONErect = new Rectangle(775, 695, 300, 30);
            MoveClassRectBuisness(ref mouse, ref oldmouse, ref mousepoint, ref ManaRect, ref EnergyRect, ref RageRect, ref LuckRect);
            if (ONErect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                toon.moveClass[0] = "";
            if (TWOrect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                toon.moveClass[1] = "";
            if (THREErect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                toon.moveClass[2] = "";
            if (FOURrect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                toon.moveClass[3] = "";
            if (ALLrect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            { toon.moveClass[3] = ""; toon.moveClass[2] = ""; toon.moveClass[1] = ""; toon.moveClass[0] = ""; }
            if (DONErect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                if (toon.moveClass[0] != "" && toon.moveClass[1] != "" && toon.moveClass[2] != "" && toon.moveClass[3] != "")
                {
                    State = ScreenState.Done;
                }
            }
        }
        private void MoveClassRectBuisness(ref MouseState mouse, ref MouseState oldmouse, ref Point mousepoint, ref Rectangle ManaRect, ref Rectangle EnergyRect, ref Rectangle RageRect, ref Rectangle LuckRect)
        {
            if (ManaRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                if (toon.moveClass[0] == "")
                    toon.moveClass[0] = "Mana";
                else if (toon.moveClass[1] == "")
                    toon.moveClass[1] = "Mana";
                else if (toon.moveClass[2] == "")
                    toon.moveClass[2] = "Mana";
                else if (toon.moveClass[3] == "")
                    toon.moveClass[3] = "Mana";
            }
            if (EnergyRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                
                if (toon.moveClass[0] == "")
                    toon.moveClass[0] = "Energy";
                else if (toon.moveClass[1] == "")
                    toon.moveClass[1] = "Energy";
                else if (toon.moveClass[2] == "")
                    toon.moveClass[2] = "Energy";
                else if (toon.moveClass[3] == "")
                    toon.moveClass[3] = "Energy";
                
            }
            if (RageRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                 if (toon.moveClass[0] == "")
                    toon.moveClass[0] = "Rage";
               else if (toon.moveClass[1] == "")
                    toon.moveClass[1] = "Rage";
                else if (toon.moveClass[2] == "")
                    toon.moveClass[2] = "Rage";
                else if (toon.moveClass[3] == "")
                    toon.moveClass[3] = "Rage";
               
            }
            if (LuckRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
               if (toon.moveClass[0] == "")
                    toon.moveClass[0] = "Luck";
               else if (toon.moveClass[1] == "")
                    toon.moveClass[1] = "Luck";
                else if (toon.moveClass[2] == "")
                    toon.moveClass[2] = "Luck";
                else if (toon.moveClass[3] == "")
                    toon.moveClass[3] = "Luck";
                 
            }
        }
        private void MoveTypesDraw(SpriteBatch spriteBatch, Extras extra)
        {
            spriteBatch.Draw(extra.MoveTypesScreen,new Rectangle(0,0,1080,720), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Classes Chosen: " + toon.moveClass[0] + " " + toon.moveClass[1] + " " + toon.moveClass[2] + " " + toon.moveClass[3] + " ", new Vector2(30, 690), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Undo Selection: [ONE] [TWO] [THREE] [FOUR] [ALL]", new Vector2(30, 30), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "[Click Here When Finished]", new Vector2(780, 695), Color.White);
           
        }

        private void DoneCode(Extras extra, Toon [] Character)
        {
            string filepath = @"Content\TextFiles\Toon" + (extra.NumOfToons + 1).ToString() + ".txt";
            
            extra.NumOfToons++;
            string ToonInfo = toon.Name + ";" + toon.Gender + ";" + toon.Agility + ";" + toon.Intellect + ";" + toon.Strength + ";" + toon.HP + ";" + toon.Defense + ";" + toon.moveClass[0] + ";" + toon.moveClass[1] + ";" + toon.moveClass[2] + ";" + toon.moveClass[3] + ";";
            File.WriteAllText(filepath, ToonInfo);
            File.WriteAllText(@"Content\TextFiles\Character Count.txt", extra.NumOfToons.ToString());
            extra.State = Extras.GameState.IntroScreen;
            Character = new Toon[extra.NumOfToons];
            State = ScreenState.Initialize;
        }
    }
}
