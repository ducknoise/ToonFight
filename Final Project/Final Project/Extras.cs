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
    class Extras
    {
        public enum GameState
        {
            Initialize,IntroScreen, CreationScreen, LoadScreen, FIGHT, EndScreen
        }
        public GameState State = GameState.Initialize;
        public SpriteFont TitleFont;
        public SpriteFont NormalFont;
        public Texture2D Pixel;
        public int NumOfToons;
        public Texture2D FemalePic;
        public Texture2D MalePic;
        public Texture2D GenderSelectionScreen;
        public Texture2D AttributesScreen;
        public Texture2D AttributesInt;
        public Texture2D AttributesStr;
        public Texture2D AttributesAg;
        public Texture2D DefenseHpPic;
        public Texture2D MoveTypesScreen;
        public Texture2D ToonSelectionScreen;
        public Texture2D FightScreen;
        public Texture2D SOPBuffPic;
        public Texture2D AbyssPic;
        public Texture2D CurseofWhitchesPic;
        public Texture2D ConcussionPic;
        public Texture2D RainOfArrowsPic;
        public Texture2D StealthPic;
        public Texture2D ChinkPic;
        public void Load(ContentManager Content)
    {
        FemalePic = Content.Load<Texture2D>("Female");
    }
       
    }
}
