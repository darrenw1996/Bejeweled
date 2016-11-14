using System;
using SwinGameSDK;
using System.Collections;
using System.Collections.Generic;
namespace MyGame
{
	public static class UIController
	{
		private	static Dictionary<string, Sprite> _Sprites = new Dictionary<string, Sprite> ();
		private static Bitmap _Background;
		private static uint timeTicks;
		private static Timer gameTime = SwinGame.CreateTimer("timer");
		public static int endTime = 60;

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
			_Background = SwinGame.LoadBitmap ("background.jpg");

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

			//SwinGame.SpriteStartAnimation (_Sprites["blueDiamond"], "spinningdiamond");
			//SwinGame.SpriteStartAnimation (_Sprites["redDiamond"], "spinningdiamond");
			//SwinGame.SpriteStartAnimation (_Sprites["yellowDiamond"], "spinningdiamond");
			//SwinGame.SpriteStartAnimation (_Sprites["greenDiamond"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites["timerBlock"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites ["rainbowDiamond"], "spinningdiamond");

		}

		public static void LoadResources()
		{
			LoadBundles ();
			LoadSprites ();
			LoadImages ();
		}

		public static void DrawGameBoard(Board myBoard,ColorBlock[,] _blocks)
		{
			timeTicks = SwinGame.TimerTicks (gameTime);
			SwinGame.DrawBitmapOnScreen (_Background, 0 ,0);
			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 100), 12, 12, 525, 615);
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					_blocks [x, y].Draw ();
				}
			}
				
			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 12, 639, 524, 50);

			SwinGame.DrawRectangle (Color.White, 12, 639, 524, 50);
			SwinGame.DrawText ("EndTime: " + endTime, Color.White, 350, 645);
			SwinGame.DrawText ("Score: " + myBoard.Score, Color.White, 100, 660); 
			SwinGame.DrawText ("Time: " + (timeTicks/=1000).ToString() + " seconds", Color.White, 350, 660);
		}

		public static void DrawMenuPage()
		{
			//Bitmap background = SwinGame.LoadBitmap("blocks.png");  //background image for start page 
			SwinGame.DrawBitmapOnScreen (_Background, 0 ,0);
			//SwinGame.DrawBitmapOnScreen (background, 0 ,42);
			//SwinGame.DrawBitmapOnScreen (background, 483, 42);
			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 549, 30);
			SwinGame.DrawTextLines ("Swinburne University of Technology Sarawak", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 12, 300, 100);
			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 100), 0, 110, 549, 120);
			SwinGame.DrawTextLines ("Welcome to", Color.CornflowerBlue, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 20, FontAlignment.AlignCenter, 125, 130, 300, 100);
			SwinGame.DrawTextLines ("Bejeweled!", Color.CornflowerBlue, SwinGame.RGBAColor (0, 0, 0, 0), "cour", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);

			//Play button
			SwinGame.FillRectangle (Color.White, 220, 295, 108, 48);
			SwinGame.FillRectangle (Color.CornflowerBlue, 224, 299, 100, 40);
			SwinGame.DrawText ("Play", Color.Black, 258, 315);

			//Instruction button
			SwinGame.FillRectangle (Color.White, 220, 356, 108, 48);
			SwinGame.FillRectangle (Color.CornflowerBlue, 224, 360, 100, 40);
			SwinGame.DrawText ("Instruction", Color.Black, 229, 376);

			//Quit button
			SwinGame.FillRectangle (Color.White, 220, 416, 108, 48);
			SwinGame.FillRectangle (Color.CornflowerBlue, 224, 420, 100, 40);
			SwinGame.DrawText ("Quit", Color.Black, 258, 436);

			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 685, 549, 30);
			SwinGame.DrawTextLines ("COS20007 Object Oriented Programming", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 685, 400, 100);
			SwinGame.DrawTextLines ("Created by: Sharon Lo", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 400, 685, 300, 100);
		}

		public static void DrawInstructionPage()
		{
			SwinGame.DrawBitmapOnScreen (_Background, 0 ,0);
			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 0, 549, 30);
			SwinGame.DrawTextLines ("Swinburne University of Technology Sarawak", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 12, 300, 100);

			SwinGame.DrawTextLines ("Instructions", Color.White, Color.Black, "cour", 20, FontAlignment.AlignLeft, 10, 70, 550, 100);
			SwinGame.DrawText ("1. Left click on your mouse to select the first block, then,", Color.White, 10, 115);
			SwinGame.DrawText ("   right click on the second block to swap.", Color.White, 10, 130);
			SwinGame.DrawText ("2. Blocks can only be swapped if their are located next to each ", Color.White, 10, 155);
			SwinGame.DrawText ("   other, above or below of each other.", Color.White, 10, 170);
			SwinGame.DrawText ("3. Blocks will not be swapped if there is no match.", Color.White, 10, 190);
			SwinGame.DrawText ("4. When there are 3 or more matching blocks, they will be deleted", Color.White, 10, 215);
			SwinGame.DrawText ("   and new blocks will be generated.", Color.White, 10, 230);
			SwinGame.DrawText ("5. Score as many as you can in 1 minute before the time runs out!", Color.White, 10, 250);

			//Back button
			SwinGame.FillRectangle (Color.White, 220, 295, 108, 48);
			SwinGame.FillRectangle (Color.CornflowerBlue, 224, 299, 100, 40);
			SwinGame.DrawText ("Back", Color.Black, 258, 315);

			SwinGame.FillRectangle(SwinGame.RGBAColor (0, 0, 0, 200), 0, 685, 549, 30);
			SwinGame.DrawTextLines ("COS20007 Object Oriented Programming", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 685, 400, 100);
			SwinGame.DrawTextLines ("Created by: Sharon Lo", Color.White, SwinGame.RGBAColor (0, 0, 0, 0), "maven_pro_regular", 12, FontAlignment.AlignLeft, 400, 685, 300, 100);
		}

		public static void DrawEndGame(Board myBoard)
		{
			if (myBoard.Score <= 300)
			{
				SwinGame.DrawTextLines ("Good!", Color.White, Color.Black, "maven_pro_regular", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);
			}
			else if (myBoard.Score <= 500)
			{
				SwinGame.DrawTextLines ("Very Good!", Color.White, Color.Black, "maven_pro_regular", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);
			}
			else if (myBoard.Score <= 750)
			{
				SwinGame.DrawTextLines ("Excellent!", Color.White, Color.Black, "maven_pro_regular", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);
			}
			else
			{
				SwinGame.DrawTextLines ("Outstanding!", Color.White, Color.Black, "maven_pro_regular", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);
			}

			SwinGame.DrawTextLines ("You scored:" + myBoard.Score + "!", Color.White, Color.Black, "maven_pro_regular", 15, FontAlignment.AlignCenter, 125, 230, 300, 100);


			SwinGame.FillRectangle (Color.White, 220, 295, 108, 48);
			SwinGame.FillRectangle (Color.CornflowerBlue, 224, 299, 100, 40);
			SwinGame.DrawText ("Play again", Color.Black, 235, 315);

			SwinGame.FillRectangle (Color.White, 220, 416, 108, 48);
			SwinGame.FillRectangle (Color.CornflowerBlue, 224, 420, 100, 40);
			SwinGame.DrawText ("Quit", Color.Black, 258, 436);
		}

		public static void FreeResources()
		{
			foreach (Sprite s in _Sprites.Values)
			{
				SwinGame.FreeSprite (s);
			}
			SwinGame.ReleaseResourceBundle ("destroyedbundle.txt");
			SwinGame.ReleaseResourceBundle ("diamondbundle.txt");
			SwinGame.FreeBitmap (_Background);
			SwinGame.ReleaseAllResources ();
		}

	}
}


