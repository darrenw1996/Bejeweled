using System;
using SwinGameSDK;
using System.Collections;
using System.Collections.Generic;
namespace MyGame
{
	public static class UIController
	{
		private	static Dictionary<string, Sprite> _Sprites = new Dictionary<string, Sprite> ();

		public static Sprite getSprite(string sprite)
		{
			return _Sprites [sprite];
		}

		private static void LoadBundles()
		{
			SwinGame.LoadResourceBundle("diamondbundle.txt");
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

			SwinGame.SpriteStartAnimation (_Sprites["blueDiamond"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites["redDiamond"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites["yellowDiamond"], "spinningdiamond");
			SwinGame.SpriteStartAnimation (_Sprites["greenDiamond"], "spinningdiamond");
		}

		public static void LoadResources()
		{
			LoadBundles ();
			LoadSprites ();
		}

		public static void DrawGameBoard(ColorBlock[,] _blocks)
		{
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					_blocks [x, y].Draw ();
				}
			}

		}


		public static void FreeResources()
		{
			SwinGame.ReleaseResourceBundle ("diamondbundle.txt");
			SwinGame.ReleaseAllResources ();
		}


	}
}


