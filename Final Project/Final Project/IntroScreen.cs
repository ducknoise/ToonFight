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
    class IntroScreen
    {
        Rectangle LoadRect = new Rectangle(600, 500, 125, 30);
        Rectangle NewRect = new Rectangle(600, 530, 125, 30);
        public Texture2D BG;
        
        public void Code(MouseState mouse, MouseState oldmouse, Point mousepoint,Extras extra)
        {
            if (LoadRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
               if (extra.NumOfToons != 0)
                extra.State = Extras.GameState.LoadScreen;
            }

            if (NewRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                extra.State = Extras.GameState.CreationScreen;
                
            }
            
        }
        public void Draw(SpriteBatch spriteBatch, Extras extra)
        {
            spriteBatch.Draw(BG, new Rectangle(0, 0, 1080, 720), Color.White);
            spriteBatch.DrawString(extra.TitleFont, "Welcome To ToonFight!", new Vector2(10, 05), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "[Load Toon]", new Vector2(600, 500), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "[New Toon]", new Vector2(600, 530), Color.White);

            
            
        }
    }
}
