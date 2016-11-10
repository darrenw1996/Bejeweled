using System;
using SwinGameSDK;

namespace MyGame
{
	public class BlockFactory
	{
		private Random _randColor;
		string sprite = "";
		public BlockFactory ()
		{
			_randColor = new Random ();	
		}

		public ColorBlock CreateRandBlock(float x, float y)
		{
			if (_randColor.NextDouble() <= 0.2)
			{
				return new RedBlock (x, y);
				/*
				sprite = "diamondred";
				SwinGame.SpriteSetX (UIController.getSprite (sprite), X + 10);
				SwinGame.SpriteSetY (UIController.getSprite (sprite), Y + 15);
				Sprite = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite), SwinGame.AnimationScriptNamed ("diamondanimation"));
				//SwinGame.SpriteSetX (Sprite, X + 10);
				//SwinGame.SpriteSetY (Sprite, Y + 15);
				//SwinGame.DrawSprite (Sprite);
				Sprite = UIController.getSprite (sprite);
				SwinGame.DrawSprite (UIController.getSprite (sprite));*/
			}
			else if (_randColor.NextDouble() <= 0.4)
			{
				return new BlueBlock (x, y);
				/*
				SwinGame.SpriteSetX (UIController.getSprite (sprite), X + 10);
				SwinGame.SpriteSetY (UIController.getSprite (sprite), Y + 15);
				Sprite = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite), SwinGame.AnimationScriptNamed ("diamondanimation"));
				//SwinGame.SpriteSetX (Sprite, X + 10);
				//SwinGame.SpriteSetY (Sprite, Y + 15);
				//SwinGame.DrawSprite (Sprite);
				Sprite = UIController.getSprite (sprite);
				SwinGame.DrawSprite (UIController.getSprite (sprite));*/
			}
			else if (_randColor.NextDouble() <= 0.6)
			{
				return new GreenBlock (x, y);
				/*
				SwinGame.SpriteSetX (UIController.getSprite (sprite), X + 10);
				SwinGame.SpriteSetY (UIController.getSprite (sprite), Y + 15);
				Sprite = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite), SwinGame.AnimationScriptNamed ("diamondanimation"));
				//SwinGame.SpriteSetX (Sprite, X + 10);
				//SwinGame.SpriteSetY (Sprite, Y + 15);
				//SwinGame.DrawSprite (Sprite);
				Sprite = UIController.getSprite (sprite);
				SwinGame.DrawSprite (UIController.getSprite (sprite));*/
			}
			else
			{
				return new YellowBlock (x, y);
				/*
				SwinGame.SpriteSetX (UIController.getSprite (sprite), X + 10);
				SwinGame.SpriteSetY (UIController.getSprite (sprite), Y + 15);
				Sprite = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite), SwinGame.AnimationScriptNamed ("diamondanimation"));
				//SwinGame.SpriteSetX (Sprite, X + 10);
				//SwinGame.SpriteSetY (Sprite, Y + 15);
				//SwinGame.DrawSprite (Sprite);
				Sprite = UIController.getSprite (sprite);
				SwinGame.DrawSprite (UIController.getSprite (sprite));*/
			}
		}
			
	}
}

