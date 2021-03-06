﻿using System;
using SwinGameSDK;
using System.Collections;
using System.Collections.Generic;
namespace MyGame
{
	public static class UIController
	{
		private	static Dictionary<string, Sprite> _Sprites = new Dictionary<string, Sprite> ();
		private static Bitmap _Background;
		private static Bitmap _gameBackground;
		private static Bitmap _gamePanel;
		private static Bitmap _boxPanel;
		private static Dictionary<string, Bitmap> _Images = new Dictionary<string, Bitmap> (); 
		private static SoundEffect _matchSound;
		private static SoundEffect _errorMatchSound;
		private static SoundEffect _leftClick;
		private static SoundEffect _rightClick;
		private static SoundEffect _explosion;
		private static SoundEffect _startGame;
		private static SoundEffect _endGame;
		private static SoundEffect _button;
		private static Dictionary<string, SoundEffect> _Sounds = new Dictionary<string, SoundEffect> ();
		private static Dictionary<string, Music> _Music = new Dictionary<string, Music> ();
		private static uint timeTicks;
		private static Timer gameTime = SwinGame.CreateTimer("timer");
		private static int endTime = 60;
		private static bool isTimerPaused = false;

		public static bool TimerPaused
		{
			get{ return isTimerPaused; }
			set{ isTimerPaused = value; }
		}

		public static int EndTime
		{
			get{ return endTime; }
			set { endTime = value; }
		}

		public static uint TimeTicks
		{
			get{ return timeTicks; }
			set { timeTicks = value; }
		}

		public static Timer gameTimer
		{
			get{ return gameTime; }
			set { gameTime = value; }
		}

		public static Sprite getSprite(string sprite)
		{
			return _Sprites [sprite];
		}

		public static void LoadImages()
		{
			_Background = SwinGame.LoadBitmap ("menubackground.png");
			_gameBackground = SwinGame.LoadBitmap ("background.png");
			_gamePanel = SwinGame.LoadBitmap ("panel.png");
			_boxPanel = SwinGame.LoadBitmap("panelbox.png");

			_Images.Add ("pause", SwinGame.LoadBitmap ("pausebtn.png"));
			_Images.Add ("resume", SwinGame.LoadBitmap ("resumebtn.png"));
			_Images.Add ("menu", SwinGame.LoadBitmap ("menubtn.png"));
			_Images.Add ("exit", SwinGame.LoadBitmap ("exitbtn.png"));
			_Images.Add ("button", SwinGame.LoadBitmap ("button.png"));
			_Images.Add ("instruction", SwinGame.LoadBitmap ("instruction.png"));
			_Images.Add ("ranking", SwinGame.LoadBitmap ("ranking.png"));
		}

		private static void LoadBundles()
		{
			SwinGame.LoadResourceBundle("diamondbundle.txt");
			SwinGame.LoadResourceBundle ("destroyedbundle.txt");
		}

		private static void LoadSprites()
		{
			Sprite blueDiamond = SwinGame.CreateSprite(SwinGame.BitmapNamed("diamondblue"), SwinGame.AnimationScriptNamed("diamondanimation"));
			_Sprites.Add ("blueDiamond", blueDiamond);

			Sprite redDiamond = SwinGame.CreateSprite(SwinGame.BitmapNamed("diamondred"), SwinGame.AnimationScriptNamed("diamondanimation"));
			_Sprites.Add ("redDiamond", redDiamond);

			Sprite yellowDiamond = SwinGame.CreateSprite(SwinGame.BitmapNamed("diamondyellow"), SwinGame.AnimationScriptNamed("diamondanimation"));
			_Sprites.Add ("yellowDiamond", yellowDiamond);

			Sprite greenDiamond = SwinGame.CreateSprite(SwinGame.BitmapNamed("diamondgreen"), SwinGame.AnimationScriptNamed("diamondanimation"));
			_Sprites.Add ("greenDiamond", greenDiamond);

			Sprite timerBlock = SwinGame.CreateSprite(SwinGame.BitmapNamed("diamondtimer"), SwinGame.AnimationScriptNamed("diamondanimation"));
			_Sprites.Add ("timerBlock", timerBlock);

			Sprite rainbowDiamond = SwinGame.CreateSprite (SwinGame.BitmapNamed ("diamondrainbow"), SwinGame.AnimationScriptNamed ("diamondanimation"));
			_Sprites.Add ("rainbowDiamond", rainbowDiamond);

			SwinGame.SpriteStartAnimation (_Sprites["blueDiamond"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites["redDiamond"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites["yellowDiamond"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites["greenDiamond"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites["timerBlock"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites ["rainbowDiamond"], "spinningdiamond");

		}

		/* JOSEPH - load sound files */
		private static void LoadSounds () 
		{
			_matchSound = SwinGame.LoadSoundEffect ("match_sound.wav");
			_errorMatchSound = SwinGame.LoadSoundEffect ("error_match_sound.wav");
			_leftClick = SwinGame.LoadSoundEffect ("left_click.wav");
			_rightClick = SwinGame.LoadSoundEffect ("right_click.wav");
			_explosion = SwinGame.LoadSoundEffect ("explosion.wav");
			_startGame = SwinGame.LoadSoundEffect ("start_game.wav");
			_endGame = SwinGame.LoadSoundEffect ("end_game.ogg");
			_button = SwinGame.LoadSoundEffect ("button.wav");

			_Sounds.Add ("match", SwinGame.LoadSoundEffect ("match_sound.wav"));
			_Sounds.Add ("errormatch", SwinGame.LoadSoundEffect ("error_match_sound.wav"));
			_Sounds.Add ("leftclick", SwinGame.LoadSoundEffect ("left_click.wav"));
			_Sounds.Add ("rightclick", SwinGame.LoadSoundEffect ("right_click.wav"));
			_Sounds.Add ("explosion", SwinGame.LoadSoundEffect ("explosion.wav"));
			_Sounds.Add ("startgame", SwinGame.LoadSoundEffect ("start_game.wav"));
			_Sounds.Add ("endgame", SwinGame.LoadSoundEffect ("end_game.ogg"));
			_Sounds.Add ("button", SwinGame.LoadSoundEffect ("button.wav"));
		}

		private static void LoadMusic ()
		{
			_Music.Add ("Background", Audio.LoadMusic ("background3.mp3"));
		}

		public static void LoadResources()
		{
			LoadBundles ();
			LoadSprites ();
			LoadImages ();
			LoadSounds ();
			LoadMusic ();
		}

		public static SoundEffect GameSound (string sound)
		{
			return _Sounds [sound];
		}

		public static Music GameMusic (string music)
		{
			return _Music [music];
		}

		public static void DrawGameBoard(Board myBoard,ColorBlock[,] _blocks)
		{

			timeTicks = SwinGame.TimerTicks (gameTime);
			SwinGame.DrawBitmapOnScreen (_gameBackground, 0 ,0);
			SwinGame.DrawBitmapOnScreen (_gamePanel, 540, 12);
			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 12, 12, 525, 615);
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					_blocks [x, y].Draw ();
				}
			}
				
			//SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 12, 639, 524, 50);

			//SwinGame.DrawRectangle (Color.White, 12, 639, 524, 50);
			SwinGame.DrawBitmapOnScreen(_boxPanel, 548, 48);
			SwinGame.DrawTextLines ("End Time:", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 20, FontAlignment.AlignCenter, 492, 60, 300, 100);
			SwinGame.DrawTextLines (endTime.ToString(), Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 25, FontAlignment.AlignCenter, 496, 80, 300, 100);

			SwinGame.DrawBitmapOnScreen(_boxPanel, 548, 130);
			SwinGame.DrawTextLines ("Time:", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 20, FontAlignment.AlignCenter, 496, 140, 300, 100);
			SwinGame.DrawTextLines ((timeTicks/=1000).ToString(), Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 25, FontAlignment.AlignCenter, 496, 160, 300, 100);

			SwinGame.DrawBitmapOnScreen(_boxPanel, 548, 210);
			SwinGame.DrawTextLines ("Score:", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 20, FontAlignment.AlignCenter, 495, 220, 300, 100);
			SwinGame.DrawTextLines (myBoard.Score.ToString(), Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 25, FontAlignment.AlignCenter, 496, 240, 300, 100);

			if (UIController.TimerPaused)
			{
				//Draw a layer to cover the game board
				SwinGame.FillRectangle (SwinGame.RGBAColor (0, 0, 0, 200), 12, 12, 525, 615);
				SwinGame.DrawTextLines ("Game Paused", Color.White, SwinGame.RGBAColor (0, 0, 0, 200), "maven_pro_regular", 40, FontAlignment.AlignCenter, 125, 260, 300, 100);

				//Draw the resume button on the right game panel
				SwinGame.DrawBitmapOnScreen (_Images ["resume"], 565, 320);
			}
			else
			{
				//Draw the pause button on the right game panel
				SwinGame.DrawBitmapOnScreen (_Images ["pause"], 565, 320);
			}
			//Draw the buttons on the game panel
			SwinGame.DrawBitmapOnScreen (_Images["menu"], 565, 400);
			SwinGame.DrawBitmapOnScreen (_Images["exit"], 565, 480);
			//SwinGame.DrawText ("EndTime: " + endTime, Color.White, 550, 100);
			//SwinGame.DrawText ("Score: " + myBoard.Score, Color.White, 100, 660); 
			//SwinGame.DrawText ("Time: " + (timeTicks/=1000).ToString() + " seconds", Color.White, 350, 660);
		}

		public static void DrawMenuPage()
		{
			SwinGame.DrawBitmapOnScreen (_Background, 0 ,0);

			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 30);
			SwinGame.DrawTextLines ("Swinburne University of Technology Sarawak", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 12, 300, 100);
			SwinGame.DrawTextLines ("SWE20001 Development Project 1 - Tools and Practices", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 420, 12, 400, 100);

			SwinGame.DrawTextLines ("Play", SwinGame.RGBColor (51, 102, 0), SwinGame.RGBAColor (0, 0, 0, 0), "mavenbold", 20, FontAlignment.AlignCenter, 282, 242, 200, 100);
			SwinGame.DrawTextLines ("Instruction", SwinGame.RGBColor (51, 102, 0), SwinGame.RGBAColor (0, 0, 0, 0), "mavenbold", 20, FontAlignment.AlignCenter, 285, 318, 200, 100);
			SwinGame.DrawTextLines ("Ranking", SwinGame.RGBColor (51, 102, 0), SwinGame.RGBAColor (0, 0, 0, 0), "mavenbold", 20, FontAlignment.AlignCenter, 285, 390, 200, 100);
			SwinGame.DrawTextLines ("Quit", SwinGame.RGBColor (51, 102, 0), SwinGame.RGBAColor (0, 0, 0, 0), "mavenbold", 20, FontAlignment.AlignCenter, 285, 465, 200, 100);

			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 610, 750, 30);

			SwinGame.DrawTextLines ("Developed by: Agile Crocodile (Sharon Lo Ying Ying, Darren Wong Siew Ding, Joseph Sim Wei Min)", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 612,600, 100);
		}

		public static void DrawInstructionPage()
		{
			SwinGame.DrawBitmapOnScreen (_gameBackground, 0 ,0);
			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 630);


			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 30);
			SwinGame.DrawTextLines ("Swinburne University of Technology Sarawak", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 12, 300, 100);
			SwinGame.DrawTextLines ("SWE20001 Development Project 1 - Tools and Practices", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 420, 12, 400, 100);

			SwinGame.DrawBitmapOnScreen (_Images["instruction"], 180 ,55);

			SwinGame.DrawText ("1. Left click on your mouse to select the first block, then, right click on the second", Color.White, 10, 155);
			SwinGame.DrawText ("   block to swap.", Color.White, 10, 170);
			SwinGame.DrawText ("2. Blocks can only be swapped if their are located next to each other, above or below", Color.White, 10, 195);
			SwinGame.DrawText ("   of each other.", Color.White, 10, 210);
			SwinGame.DrawText ("3. Blocks will not be swapped if there is no match.", Color.White, 10, 235);
			SwinGame.DrawText ("4. When there are 3 or more matching blocks, they will be deleted and new blocks will", Color.White, 10, 260);
			SwinGame.DrawText ("   be generated.", Color.White, 10, 275);
			SwinGame.DrawText ("5. Score as many as you can in 1 minute before the time runs out!", Color.White, 10, 300);

			//Back button
			SwinGame.DrawBitmapOnScreen (_Images ["button"], 280, 500);
			SwinGame.DrawTextLines ("Back", SwinGame.RGBColor (51, 102, 0), SwinGame.RGBAColor (0, 0, 0, 0), "mavenbold", 20, FontAlignment.AlignCenter, 280, 535, 200, 100);

			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 610, 750, 30);
			//SwinGame.DrawTextLines ("SWE20001 Development Project 1 - Tools and Practices", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 612, 400, 100);
			SwinGame.DrawTextLines ("Developed by: Agile Crocodile (Sharon Lo Ying Ying, Darren Wong Siew Ding, Joseph Sim Wei Min)", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 612,600, 100);
		}

		public static void DrawRanking(){
			SwinGame.DrawBitmapOnScreen (_gameBackground, 0, 0);
			SwinGame.FillRectangle (SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 630);

			SwinGame.FillRectangle (SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 30);
			SwinGame.DrawTextLines ("Swinburne University of Technology Sarawak", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 12, 300, 100);
			SwinGame.DrawTextLines ("SWE20001 Development Project 1 - Tools and Practices", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 420, 12, 400, 100);

			SwinGame.DrawBitmapOnScreen (_Images["ranking"], 245 ,55);

			SwinGame.DrawBitmapOnScreen (_Images ["button"], 280, 500);
			SwinGame.DrawTextLines ("Back", SwinGame.RGBColor (51, 102, 0), SwinGame.RGBAColor (0, 0, 0, 0), "mavenbold", 20, FontAlignment.AlignCenter, 280, 535, 200, 100);
			SwinGame.DrawTextLines ("Developed by: Agile Crocodile (Sharon Lo Ying Ying, Darren Wong Siew Ding, Joseph Sim Wei Min)", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 612,600, 100);
		}

		public static void DrawEndGame(){
			SwinGame.DrawBitmapOnScreen (_gameBackground, 0, 0);
			SwinGame.FillRectangle (SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 630);

			SwinGame.FillRectangle (SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 30);
			SwinGame.DrawTextLines ("Swinburne University of Technology Sarawak", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 12, 300, 100);

			SwinGame.DrawBitmapOnScreen (_Images["ranking"], 245 ,55);

		}

		public static void DrawEndRanking(){

			SwinGame.DrawBitmapOnScreen (_gameBackground, 0, 0);
			SwinGame.FillRectangle (SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 630);

			SwinGame.FillRectangle (SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 750, 30);
			SwinGame.DrawTextLines ("Swinburne University of Technology Sarawak", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 12, 300, 100);

			SwinGame.DrawBitmapOnScreen (_Images["ranking"], 245 ,55);


			SwinGame.DrawBitmapOnScreen(_Images["button"], 60, 500);
			SwinGame.DrawTextLines ("Menu", SwinGame.RGBColor (51, 102, 0), SwinGame.RGBAColor (0, 0, 0, 0), "mavenbold", 20,FontAlignment.AlignCenter, 115, 535, 100, 100);


			SwinGame.DrawBitmapOnScreen(_Images["button"], 500, 500);
			SwinGame.DrawTextLines ("Play Again",SwinGame.RGBColor (51, 102, 0), SwinGame.RGBAColor (0, 0, 0, 0), "mavenbold", 20, FontAlignment.AlignCenter, 550, 535, 100, 100);

		}

		public static void FreeResources()
		{
			foreach (Sprite s in _Sprites.Values)
			{
				SwinGame.FreeSprite (s);
			}
			foreach (SoundEffect sd in _Sounds.Values) {
				SwinGame.FreeSoundEffect (sd);
			}
			foreach (Music m in _Music.Values) {
				SwinGame.FreeMusic (m);
			}
			SwinGame.ReleaseResourceBundle ("destroyedbundle.txt");
			SwinGame.ReleaseResourceBundle ("diamondbundle.txt");
			SwinGame.FreeBitmap (_Background);
			//SwinGame.ReleaseAllResources ();
		}

	}
}


