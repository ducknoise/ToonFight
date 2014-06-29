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
    class FightScreen
    {
       
        Move SelectedMove = new Move();
        enum FightState
        {
            InitiliazeMoves, ChosenOnesTurn, OpponentsTurn,Attack, OAttack, NoResource, Special
        }
        FightState State = FightState.InitiliazeMoves;
        Random rand = new Random();
        enum InfoState
        {
            Move0, Move1, Move2,Move3,Stats
        }
        int OnToon; //0 = Chosen, 1 = Opponent
        int[] MoveDamage = new int[4];
        InfoState info = InfoState.Stats;
        double Damage;
        bool possibleMove = true;
        bool firstTimeThrough = true; //for initial CD-- and Resource Regen
        bool HaveResource = false; //check to make sure player has enough resource for move
        Rectangle SkipTurnRect = new Rectangle(945, 415, 130, 35);
        public void Code(Toon ChosenOne, Toon OpponentToon, IntMoves ManaMoves, StrMoves RageMoves, AgMoves EnergyMoves, LuckMoves luckMoves, MouseState mouse, MouseState oldmouse, Point mousepoint, KeyboardState keyboard, KeyboardState oldkeyboard, Extras extra)
        {
            switch (State)
            {
                case FightState.InitiliazeMoves:
                    InitializeMoves(ChosenOne, OpponentToon, ManaMoves, RageMoves, EnergyMoves, luckMoves,extra);
                    break;
                case FightState.ChosenOnesTurn:
                    OnToon = 0;
                    TurnCode(ChosenOne, mouse, oldmouse, mousepoint, ChosenOne, OpponentToon, extra);
                    break;
                case FightState.OpponentsTurn:
                    OnToon = 1;
                    TurnCode(OpponentToon, mouse, oldmouse, mousepoint, ChosenOne, OpponentToon, extra);
                    break;
                case FightState.Attack:
                    AttackCode(keyboard, oldkeyboard);
                    break;
                case FightState.NoResource:
                  NoResourceCode(keyboard, oldkeyboard);
                    break;
                case FightState.Special:
                    SpecialCode(keyboard, oldkeyboard, ChosenOne,OpponentToon);
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Extras extra, Toon ChosenOne, Toon OpponentToon)
        {
            switch (State)
            {
                case FightState.ChosenOnesTurn:
                    TurnDraw(spriteBatch, extra, ChosenOne, OpponentToon);
                    break;
                case FightState.Attack:
                    AttackDraw(spriteBatch, extra, ChosenOne, OpponentToon);
                    break;
                case FightState.OpponentsTurn:
                    TurnDraw(spriteBatch, extra, ChosenOne, OpponentToon);
                    break;
 
                case FightState.NoResource:
                    NoResourceDraw(spriteBatch, extra, ChosenOne, OpponentToon);
                    break;
                case FightState.Special:
                    SpecialDraw(spriteBatch, extra, ChosenOne, OpponentToon);
                    break;
            }
        }

        private void InitializeMoves(Toon ChosenOne, Toon OpponentToon, IntMoves ManaMoves, StrMoves RageMoves, AgMoves EnergyMoves, LuckMoves luckMoves, Extras extra)
        {
            MoveChoice(OpponentToon, ChosenOne, ManaMoves, RageMoves, EnergyMoves, luckMoves,extra);
            MoveChoice(ChosenOne, OpponentToon, ManaMoves, RageMoves, EnergyMoves, luckMoves,extra);
            OnToon = rand.Next(0, 2);
            if (OnToon == 0)
                State = FightState.ChosenOnesTurn;
            if (OnToon == 1)
                State = FightState.OpponentsTurn;
        } 
        private void MoveChoice(Toon Main, Toon Other, IntMoves ManaMoves, StrMoves RageMoves, AgMoves EnergyMoves, LuckMoves luckMoves, Extras extra)
        {
            ManaMoves.InitializeMoves(Other, Main,extra);
            RageMoves.InitializeMoves(Other, Main,extra);
            EnergyMoves.InitializeMoves(Other,extra, Main);
            luckMoves.InitializeMoves(Other);
            
            Move ability = new Move();
            for (int i = 0; i < 4; i++)
            {
                Main.Ability[i] = new Move();
                bool Assign = true;
                if (Main.moveClass[i] == "Mana")
                {
                     ability = ManaMoves.Ability[rand.Next(0, ManaMoves.NumOfMoves)];
                     for (int k = 0; k < i; k++)
                     {
                         Assign = true;
                         if (Main.Ability[k].Name == ability.Name)
                         {
                             i--;
                             Assign = false;
                             break;
                         }
                     }
                    if (Assign == true)
                     AssignAbility(Main.Ability[i], ability);
                }
                if (Main.moveClass[i] == "Energy")
                {
                    ability = EnergyMoves.Ability[rand.Next(0, EnergyMoves.NumOfMoves)];
                    for (int k = 0; k < i; k++)
                    {
                        Assign = true;
                        if (Main.Ability[k].Name == ability.Name)
                        {
                            i--;
                            Assign = false;
                            break;
                        }
                    }
                    if (Assign == true)
                        AssignAbility(Main.Ability[i], ability);
                }
                if (Main.moveClass[i] == "Rage")
                {
                     ability = RageMoves.Ability[rand.Next(0, RageMoves.NumOfMoves)];
                     for (int k = 0; k < i; k++)
                     {
                         Assign = true;
                         if (Main.Ability[k].Name == ability.Name)
                         {
                             i--;
                             Assign = false;
                             break;
                         }
                     }
                     if (Assign == true)
                     AssignAbility(Main.Ability[i], ability);
                }
                if (Main.moveClass[i] == "Luck")
                {
                     ability = luckMoves.Ability[rand.Next(0, luckMoves.NumOfMoves)];
                     for (int k = 0; k < i; k++)
                     {
                         Assign = true;
                         if (Main.Ability[k].Name == ability.Name)
                         {
                             i--;
                             Assign = false;
                             break;
                         }
                     }
                     if (Assign == true)
                     AssignAbility(Main.Ability[i], ability);
                }
            }
        } // randomly assigns moves based on the MoveClasses chosen in the character creation screen, calls upon AssignAbility()
    
        private void AssignAbility(Move toonMove, Move ability)
        {
            toonMove.BeenActive = ability.BeenActive;
            toonMove.CoolDown = ability.CoolDown;
            toonMove.Cost = ability.Cost;
            toonMove.Damage = ability.Damage;
            toonMove.Description = ability.Description;
            toonMove.Duration = ability.Duration;
            toonMove.Effect = ability.Effect;
            toonMove.Name = ability.Name;
            toonMove.Resource = ability.Resource;
            toonMove.BuffIcon = ability.BuffIcon;

        } 

        private void TurnCode(Toon Activetoon, MouseState mouse, MouseState oldmouse, Point mousepoint, Toon ChosenOne, Toon OpponentToon, Extras extra)
        {

            ChosenOne.KeepGoing = true;
            OpponentToon.KeepGoing = true;
            FirstTimeThrough(Activetoon);
            if (possibleMove == false)
            {
                State = FightState.NoResource;
                firstTimeThrough = false;
            }
            else
            MoveSelection(mouse, oldmouse, mousepoint, ChosenOne, OpponentToon, extra);
        }
       
        private void FirstTimeThrough(Toon ActiveToon) //Regen for Energy and Mana...decreases the cooldown remaining on all moves by one...calls upon PossibleMoveCheck()
        {
            if (firstTimeThrough == true)
            {
                if (ActiveToon.Agility > 0)
                    ActiveToon.EnergyRegen();
                if (ActiveToon.Intellect > 0)
                    ActiveToon.ManageRegen();
                if (ActiveToon.CD[0] > 0)
                    ActiveToon.CD[0]--;
                if (ActiveToon.CD[1] > 0)
                    ActiveToon.CD[1]--;
                if (ActiveToon.CD[2] > 0)
                    ActiveToon.CD[2]--;
                if (ActiveToon.CD[3] > 0)
                    ActiveToon.CD[3]--;
                firstTimeThrough = false;
                
                
                PossibleMoveCheck(ActiveToon);
            }
        }
        private void PossibleMoveCheck(Toon ActiveToon) //Checks to see if their is enough resource to preform any of the available abilities. If not, the turn will be skipped.
        {
            possibleMove = false;
            for (int i = 0; i < 4; i++)
            {
                switch (ActiveToon.Ability[i].Resource)
                {
                    case "Mana":
                        if (ActiveToon.Ability[i].Cost < ActiveToon.Mana)
                            if (ActiveToon.CD[i] == 0)
                                possibleMove = true;
                        break;
                    case "Rage":
                        if (ActiveToon.Ability[i].Cost < ActiveToon.Rage)
                            if (ActiveToon.CD[i] == 0)
                                possibleMove = true;
                        break;
                    case "Energy":
                        if (ActiveToon.Ability[i].Cost < ActiveToon.Energy)
                            if (ActiveToon.CD[i] == 0)
                                possibleMove = true;
                        break;
                    case "None":
                        possibleMove = true;
                        break;

                }
                if (possibleMove == true)
                    break;
            }
        }
        private void MoveSelection( MouseState mouse,  MouseState oldmouse,  Point mousepoint, Toon ChosenOne, Toon OpponentToon, Extras extra) //Controls the InfoState which controls what is displayed on the RightSide of the screen...also calls upon MoveSelected()
        {
            switch (OnToon)
            {
                case 0:
                    ChosenOne.KeepGoingCheck(OpponentToon);
                    if (ChosenOne.KeepGoing == true)
                    {
                        HumanInterfaceMoveSelection(mouse, oldmouse, mousepoint, ChosenOne, OpponentToon, extra);
                    }
                    else
                    {
                        State = FightState.Special;
                        ChosenOne.KeepGoing = true;
                    }
                    break;
                case 1:
                    OpponentToon.KeepGoingCheck( ChosenOne);
                    if (OpponentToon.KeepGoing == true)
                    {
                        int OnMove = rand.Next(0, 4);
                        SelectedMove = OpponentToon.Ability[OnMove];
                        MoveSelected(OpponentToon, ChosenOne, OnMove, extra);
                    }
                    else
                    {
                        State = FightState.Special;
                        OpponentToon.KeepGoing = false;
                    }
                    break;
            }
            
        }
        private void HumanInterfaceMoveSelection( MouseState mouse,  MouseState oldmouse,  Point mousepoint, Toon ChosenOne, Toon OpponentToon, Extras extra)
        {
            Rectangle[] MoveRect = new Rectangle[4];
            MoveRect[0] = new Rectangle(5, 465, 288, 123);
            MoveRect[1] = new Rectangle(293, 465, 288, 123);
            MoveRect[2] = new Rectangle(5, 588, 288, 123);
            MoveRect[3] = new Rectangle(293, 588, 288, 123);
            for (int i = 0; i < 4; i++)
            {
                if (MoveRect[i].Contains(mousepoint))
                {
                    switch (i)
                    {
                        case 0:
                            info = InfoState.Move0;
                            break;
                        case 1:
                            info = InfoState.Move1;
                            break;
                        case 2:
                            info = InfoState.Move2;
                            break;
                        case 3:
                            info = InfoState.Move3;
                            break;
                    }
                    if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
                    MoveSelected(ChosenOne, OpponentToon, i, extra);

                }
            }
            if (SkipTurnRect.Contains(mousepoint) && mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                State = FightState.OpponentsTurn;
                DamageCalcCode(ChosenOne, OpponentToon);
                firstTimeThrough = true;
                HaveResource = false;
            }
            if (MoveRect[0].Contains(mousepoint) == false && MoveRect[1].Contains(mousepoint) == false && MoveRect[2].Contains(mousepoint) == false && MoveRect[3].Contains(mousepoint) == false)
                info = InfoState.Stats;
        }
        private void MoveSelected(Toon ActiveToon, Toon OtherToon, int OnMove, Extras extra)//Deals with what happens after a move has been selected... calls upon ResourceCheck, DamageCalcCode(),Toon.MoveCost(),
        {
            
                if (ActiveToon.CD[OnMove] == 0)
                {
                    ResourceCheck(ActiveToon, OnMove);
                    if (HaveResource == true)
                    {
                        SelectedMove = ActiveToon.Ability[OnMove];
                            ActiveToon.Ability[OnMove].BeenActive = 0;
                            DamageCalcCode(ActiveToon, OtherToon);
                            ActiveToon.MoveCost(SelectedMove, OnMove);
                            firstTimeThrough = true;
                            HaveResource = false;
                            State = FightState.Attack;
                            if (OtherToon.HP <= 0) // Game Ended
                            {
                                State = FightState.InitiliazeMoves;
                                extra.State = Extras.GameState.EndScreen;
                            }
                        
                    }
                }
            
        }
        private void ResourceCheck(Toon Attacker, int OnMove) //Check to make sure that the player has enough resource to preform Ability he or she wants to
        {
            switch (Attacker.Ability[OnMove].Resource)
            {
                case "Mana":
                    if (Attacker.Ability[OnMove].Cost <= Attacker.Mana)
                        HaveResource = true;
                    break;
                case "Rage":
                    if (Attacker.Ability[OnMove].Cost <= Attacker.Rage)
                        HaveResource = true;
                    break;
                case "Energy":
                    if (Attacker.Ability[OnMove].Cost <= Attacker.Energy)
                        HaveResource = true;
                    break;
                case "None":
                    HaveResource = true;
                    break;
            }
        }
        private void DamageCalcCode(Toon Attacker, Toon Defender) //calls upon Toon.DamageSent and Toon.DamageTaken
        {
            Attacker.DamageSent(SelectedMove, out Damage, MoveDamage, Defender);
            Attacker.Buffs(Defender,out Damage, ref Damage, SelectedMove);
            Defender.DamageTaken(Damage, out Damage);
            if (Defender.HitChance == 0)
                SelectedMove.Effect = Attacker.Name + " has missed and no \ndamage was dealt.";


        }

        private void TurnDraw(SpriteBatch spriteBatch, Extras extra, Toon ChosenOne, Toon OpponentToon)
        {
            GenericDraw(spriteBatch, extra, ChosenOne, OpponentToon);
            if (OnToon == 0)
                HumansTurnDraw(spriteBatch,extra,OpponentToon,ChosenOne);
                
        }

        private void HumansTurnDraw(SpriteBatch spriteBatch, Extras extra, Toon OpponentToon, Toon ChosenOne)
        {
            switch (info)
            {
                case InfoState.Stats:
                    int healthWidth = (int)((483 / ChosenOne.MaxHP) * ChosenOne.HP);
                    spriteBatch.Draw(extra.Pixel, new Rectangle(590, 465, healthWidth, 30), Color.Red);
                    spriteBatch.DrawString(extra.NormalFont, "HP: " + ChosenOne.HP, new Vector2(590, 465), Color.White);
                    int manaWidth = (int)((483 / ChosenOne.MaxMana) * ChosenOne.Mana);
                    spriteBatch.Draw(extra.Pixel, new Rectangle(590, 495, manaWidth, 30), Color.Blue);
                    spriteBatch.DrawString(extra.NormalFont, "Mana: " + ChosenOne.Mana, new Vector2(590, 495), Color.White);
                    int rageWidth = (int)((483 / ChosenOne.MaxRage) * ChosenOne.Rage);
                    spriteBatch.Draw(extra.Pixel, new Rectangle(590, 525, rageWidth, 30), Color.DarkRed);
                    spriteBatch.DrawString(extra.NormalFont, "Rage: " + ChosenOne.Rage, new Vector2(590, 525), Color.White);
                    int EnergyWidth = (int)((483 / ChosenOne.MaxEnergy) * ChosenOne.Energy);
                    spriteBatch.Draw(extra.Pixel, new Rectangle(590, 555, EnergyWidth, 30), Color.Brown);
                    spriteBatch.DrawString(extra.NormalFont, "Energy: " + ChosenOne.Energy, new Vector2(590, 555), Color.White);
                    break;
                case InfoState.Move0:
                    spriteBatch.DrawString(extra.NormalFont, ChosenOne.Ability[0].Description, new Vector2(590, 465), Color.White);
                    break;
                case InfoState.Move1:
                    spriteBatch.DrawString(extra.NormalFont, ChosenOne.Ability[1].Description, new Vector2(590, 465), Color.White);
                    break;
                case InfoState.Move2:
                    spriteBatch.DrawString(extra.NormalFont, ChosenOne.Ability[2].Description, new Vector2(590, 465), Color.White);
                    break;
                case InfoState.Move3:
                    spriteBatch.DrawString(extra.NormalFont, ChosenOne.Ability[3].Description, new Vector2(590, 465), Color.White);
                    break;
            }

        }
        private static void GenericDraw(SpriteBatch spriteBatch, Extras extra, Toon ChosenOne, Toon OpponentToon)
        {
            spriteBatch.Draw(extra.FightScreen, new Rectangle(0, 0, 1080, 720), Color.White);
            spriteBatch.Draw(ChosenOne.sprite, new Rectangle(125, 45, 180, 300), Color.White);
            spriteBatch.Draw(OpponentToon.sprite, new Rectangle(735, 45, 180, 300), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ChosenOne.Name, new Vector2(170, 350), Color.White);
            spriteBatch.DrawString(extra.NormalFont, OpponentToon.Name, new Vector2(780, 350), Color.White);
            int OhealthWidth = (int)((483 / OpponentToon.MaxHP) * OpponentToon.HP);
            spriteBatch.Draw(extra.Pixel, new Rectangle(610, 0, OhealthWidth, 30), Color.Red);
            spriteBatch.DrawString(extra.NormalFont, "HP: " + OpponentToon.HP, new Vector2(610, 0), Color.White);
            AblityDraw(spriteBatch, extra, ChosenOne);
            int healthWidth = (int)((483 / ChosenOne.MaxHP) * ChosenOne.HP);
            spriteBatch.Draw(extra.Pixel, new Rectangle(0, 0, healthWidth, 30), Color.Red);
            spriteBatch.DrawString(extra.NormalFont, "HP: " + ChosenOne.HP, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "[Skip Turn]", new Vector2(950, 420), Color.White);
            ChosenOne.BuffDraw(spriteBatch, extra, 0);
            OpponentToon.BuffDraw(spriteBatch, extra, 1050);
            
        }
        private static void AblityDraw(SpriteBatch spriteBatch, Extras extra, Toon ChosenOne)
        {
            spriteBatch.DrawString(extra.NormalFont, ChosenOne.Ability[0].Name, new Vector2(30, 475), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Cost: " + ChosenOne.Ability[0].Cost.ToString() + " " + ChosenOne.Ability[0].Resource, new Vector2(20, 505), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Raw Damage: " + ChosenOne.Ability[0].Damage.ToString(), new Vector2(20, 535), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "CoolDown Remaining: " + ChosenOne.CD[0].ToString(), new Vector2(20, 565), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ChosenOne.Ability[1].Name, new Vector2(318, 475), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Cost: " + ChosenOne.Ability[1].Cost.ToString() + " " + ChosenOne.Ability[1].Resource, new Vector2(308, 505), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Raw Damage: " + ChosenOne.Ability[1].Damage.ToString(), new Vector2(308, 535), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "CoolDown Remaining: " + ChosenOne.CD[1].ToString(), new Vector2(308, 565), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ChosenOne.Ability[2].Name, new Vector2(30, 598), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Cost: " + ChosenOne.Ability[2].Cost.ToString() + " " + ChosenOne.Ability[2].Resource, new Vector2(20, 628), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Raw Damage: " + ChosenOne.Ability[2].Damage.ToString(), new Vector2(20, 658), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "CoolDown Remaining: " + ChosenOne.CD[2].ToString(), new Vector2(20, 688), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ChosenOne.Ability[3].Name, new Vector2(318, 598), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Cost: " + ChosenOne.Ability[3].Cost.ToString() + " " + ChosenOne.Ability[3].Resource, new Vector2(308, 628), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Raw Damage: " + ChosenOne.Ability[3].Damage.ToString(), new Vector2(308, 658), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "CoolDown Remaining: " + ChosenOne.CD[3].ToString(), new Vector2(308, 688), Color.White);
        }

        private void AttackCode(KeyboardState keyboard, KeyboardState oldkeyboard)
        {
            if (keyboard.IsKeyDown(Keys.Enter) && oldkeyboard.IsKeyUp(Keys.Enter))
            {
                if (OnToon == 0)
                    State = FightState.OpponentsTurn;
                if (OnToon == 1)
                    State = FightState.ChosenOnesTurn;
                MoveDamage[0] = 0;
                MoveDamage[1] = 0;
                MoveDamage[2] = 0;
                MoveDamage[3] = 0;

            }
        }
        private void AttackDraw(SpriteBatch spriteBatch, Extras extra, Toon ChosenOne, Toon OpponentToon)
        {
            if (OnToon == 0)
                DrawingAttack(spriteBatch, extra, ChosenOne, OpponentToon, ChosenOne, OpponentToon);
            if (OnToon ==1)
                DrawingAttack(spriteBatch, extra, OpponentToon, ChosenOne, ChosenOne, OpponentToon);
        }
        private void DrawingAttack(SpriteBatch spriteBatch, Extras extra, Toon ActiveToon, Toon OtherToon, Toon ChosenOne, Toon OpponentToon)
        {
            GenericDraw(spriteBatch, extra, ChosenOne, OpponentToon);
            spriteBatch.DrawString(extra.NormalFont, ActiveToon.Name + " has Used " + SelectedMove.Name, new Vector2(590, 465), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ActiveToon.Name + " has inflicted " + (int)Damage + " damage!", new Vector2(590, 495), Color.White);
            spriteBatch.DrawString(extra.NormalFont, SelectedMove.Effect, new Vector2(590, 555), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Press [ENTER] to continue.", new Vector2(590, 670), Color.White);
            spriteBatch.DrawString(extra.NormalFont, "Raw Damage:", new Vector2(345, 70), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ActiveToon.Ability[0].Name + ": " + MoveDamage[0], new Vector2(345, 100), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ActiveToon.Ability[1].Name + ": " + MoveDamage[1], new Vector2(345, 130), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ActiveToon.Ability[2].Name + ": " + MoveDamage[2], new Vector2(345, 160), Color.White);
            spriteBatch.DrawString(extra.NormalFont, ActiveToon.Ability[3].Name + ": " + MoveDamage[3], new Vector2(345, 190), Color.White);
        }

        private void NoResourceCode(KeyboardState keyboard, KeyboardState oldkeyboard)
        {
            if (keyboard.IsKeyDown(Keys.Enter) && oldkeyboard.IsKeyUp(Keys.Enter))
            {
                if (OnToon == 0)
                    State = FightState.OpponentsTurn;
                if (OnToon == 1)
                    State = FightState.ChosenOnesTurn;

                firstTimeThrough = true;
                HaveResource = false;
            }
        }
        private void NoResourceDraw(SpriteBatch spriteBatch, Extras extra, Toon ChosenOne, Toon OpponentToon)
        {
            GenericDraw(spriteBatch, extra, ChosenOne, OpponentToon);
            spriteBatch.DrawString(extra.NormalFont, "The Active Toon does not have enough \nresource to do anything and so his or her \nturn will be skipped", new Vector2(590, 465), Color.White);
        }

        private void SpecialCode(KeyboardState keyboard, KeyboardState oldkeyboard, Toon ChosenOne, Toon OpponentToon )
        {
            if (keyboard.IsKeyDown(Keys.Enter) && oldkeyboard.IsKeyUp(Keys.Enter))
            {
                if (OnToon == 0)
                {
                
                    DamageCalcCode(ChosenOne, OpponentToon);
                    State = FightState.Attack;
                }
                if (OnToon == 1)
                {
             
                    DamageCalcCode(OpponentToon, ChosenOne);
                    State = FightState.Attack;
                }
                
                firstTimeThrough = true;
                HaveResource = false;
            }
        }
        private void SpecialDraw(SpriteBatch spriteBatch, Extras extra, Toon ChosenOne, Toon OpponentToon)
        {
            GenericDraw(spriteBatch, extra, ChosenOne, OpponentToon);
            switch (OnToon)
            {
                case 0:
                    ChosenOne.SpecialDraw(spriteBatch, extra, OpponentToon);
                    break;
                case 1:
                    OpponentToon.SpecialDraw(spriteBatch, extra, ChosenOne);
                    break;
            }
        }
    }
}
