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
    class EndScreen
    {
        Toon Winner = new Toon();
        Toon Loser = new Toon();
        public void EndScreenCode(Toon ChosenOne, Toon OpponentToon, Extras extra, KeyboardState keyboard, KeyboardState oldkeyboard)
        {
            if (ChosenOne.HP <= 0)
            {
                Winner = OpponentToon;
                Loser = ChosenOne;
            }
            else
            {
                Winner = ChosenOne;
                Loser = OpponentToon;
            }
            if (keyboard.IsKeyDown(Keys.Enter) && oldkeyboard.IsKeyUp(Keys.Enter))
            {
                extra.State = Extras.GameState.Initialize;
            }
        }
        public void EndScreenDraw(SpriteBatch spriteBatch, Extras extra)
        { 
            spriteBatch.DrawString(extra.TitleFont, Loser.Name +" LOST TO "+ Winner.Name +"\nWHAT A MATCH!!!!!", new Vector2(20,20), Color.White);
        }
    }
}
