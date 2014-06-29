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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Extras extra = new Extras();
        IntroScreen introScreen = new IntroScreen();
        CreationScreen creationScreen = new CreationScreen();
        Toon[] Character;
        MouseState mouse;
        MouseState oldmouse;
        KeyboardState keyboard;
        KeyboardState oldkeyboard;
        Point mousepoint;
        LoadScreen loadScreen = new LoadScreen();
        Toon ChosenOne = new Toon();
        Toon OpponentToon = new Toon();
        FightScreen fightScreen = new FightScreen();
        IntMoves ManaMoves = new IntMoves();
        StrMoves RageMoves = new StrMoves();
        LuckMoves luckMoves = new LuckMoves();
        AgMoves EnergyMoves = new AgMoves();
        EndScreen endScreen = new EndScreen();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1080;
        }

        protected override void Initialize()
        {

            
            string CharacterCount = File.ReadAllText(@"Content\TextFiles\Character Count.txt");
            extra.NumOfToons = int.Parse(CharacterCount);
            Character = new Toon[extra.NumOfToons];            
            extra.State = Extras.GameState.IntroScreen;
            
            base.Initialize();
        }

  
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            extra.NormalFont = Content.Load<SpriteFont>("normalfont");
            extra.TitleFont = Content.Load<SpriteFont>("titlefont");
            introScreen.BG = Content.Load<Texture2D>("IntroBG");
            extra.Pixel = Content.Load<Texture2D>("Pixel");
            extra.MalePic = Content.Load<Texture2D>("Male");
            extra.FemalePic = Content.Load<Texture2D>("Female");
            extra.GenderSelectionScreen = Content.Load<Texture2D>("MaleOrFemale");
            extra.AttributesScreen = Content.Load<Texture2D>("Attributes Screen");
            extra.AttributesInt = Content.Load<Texture2D>("Attributes Intellect");
            extra.AttributesAg = Content.Load<Texture2D>("Attributes Agility");
            extra.AttributesStr = Content.Load<Texture2D>("Attributes Strength");
            extra.DefenseHpPic = Content.Load<Texture2D>("DefenseHpPic");
            extra.MoveTypesScreen = Content.Load<Texture2D>("MoveSelection");
            extra.ToonSelectionScreen = Content.Load<Texture2D>("ToonSelection");
            extra.FightScreen = Content.Load<Texture2D>("FightScreen");
            extra.SOPBuffPic = Content.Load<Texture2D>("SOPBuff");
            extra.AbyssPic = Content.Load<Texture2D>("Abyss");
            extra.CurseofWhitchesPic = Content.Load<Texture2D>("witch");
            extra.ConcussionPic = Content.Load<Texture2D>("ConcussionPic");
            extra.RainOfArrowsPic = Content.Load<Texture2D>("RainOfArrowsPic");
            extra.StealthPic = Content.Load<Texture2D>("Stealth");
            extra.ChinkPic = Content.Load<Texture2D>("Broken Chain");
        }
 
        protected override void Update(GameTime gameTime)
        {
            IsMouseVisible = true;
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();
            mousepoint = new Point(mouse.X, mouse.Y);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch (extra.State)
            {
                case Extras.GameState.Initialize:
                    Initialize();
                    break;
                case Extras.GameState.IntroScreen:
                    introScreen.Code(mouse, oldmouse, mousepoint, extra);
                    Character = new Toon[extra.NumOfToons];
                    break;
                case Extras.GameState.CreationScreen:
                    creationScreen.Code(extra, keyboard, oldkeyboard, mouse ,oldmouse,mousepoint, Character);
                    break;
                case Extras.GameState.LoadScreen:
                    loadScreen.Code(Character, extra, keyboard,oldkeyboard, ChosenOne,OpponentToon, mouse,mousepoint);
                    break;
                case Extras.GameState.FIGHT:
                    fightScreen.Code(ChosenOne, OpponentToon, ManaMoves, RageMoves, EnergyMoves, luckMoves, mouse, oldmouse, mousepoint, keyboard, oldkeyboard, extra);
                    break;
                case Extras.GameState.EndScreen:
                    endScreen.EndScreenCode(ChosenOne, OpponentToon, extra, keyboard, oldkeyboard);
                    break;
            }
            oldmouse = mouse;
            oldkeyboard = keyboard;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (extra.State)
            {   
                case Extras.GameState.IntroScreen:
                    introScreen.Draw(spriteBatch, extra);
                     break;
                case Extras.GameState.CreationScreen:
                     creationScreen.Draw(spriteBatch, extra);
                     break;
                case Extras.GameState.LoadScreen:
                     loadScreen.Draw(Character, spriteBatch, extra);
                     break;
                case Extras.GameState.FIGHT:
                     fightScreen.Draw(spriteBatch, extra, ChosenOne, OpponentToon);
                     break;
                case Extras.GameState.EndScreen:
                     endScreen.EndScreenDraw(spriteBatch, extra);
                     break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
