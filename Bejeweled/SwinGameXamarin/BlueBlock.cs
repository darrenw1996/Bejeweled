using System;
using SwinGameSDK;

namespace MyGame
{
	public class BlueBlock:ColorBlock
	{
		public BlueBlock (float x, float y):base (x, y, Color.Blue)
		{
		}

		public override void Draw ()
		{
			if (Selected)
			{
				DrawOutline ();	
			}

			//SwinGame.FillRectangle (Color, X, Y, Width, Height);
			string sprite = "";
			if (Color == Color.Yellow) 
			{
				sprite = "diamondyellow";
				sprite = "yellowDiamond";
			} else if (Color == Color.Red)
			{
				sprite = "diamondred";
				sprite = "redDiamond";
			} else if (Color == Color.Blue) 
			{
				sprite = "diamondblue";
				sprite = "blueDiamond";
			} else if (Color == Color.Green) 
			{
				sprite = "diamondgreen";
				sprite = "greenDiamond";
			}
			//SwinGame.FillRectangle (Color.Maroon, X, Y, Width, Height);
			//SwinGame.FillRectangle (Color.SaddleBrown, X+5, Y+5, Width-10, Height-10);

			SwinGame.SpriteSetX (UIController.getSprite (sprite), X+10);
			SwinGame.SpriteSetY (UIController.getSprite (sprite), Y+15);
			//Sprite = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite), SwinGame.AnimationScriptNamed ("diamondanimation"));
			//SwinGame.SpriteSetX (Sprite, X + 10);
			//SwinGame.SpriteSetY (Sprite, Y + 15);
			//SwinGame.DrawSprite (Sprite);
			Sprite = UIController.getSprite (sprite);
			SwinGame.DrawSprite (UIController.getSprite (sprite));
		}
	}
}

