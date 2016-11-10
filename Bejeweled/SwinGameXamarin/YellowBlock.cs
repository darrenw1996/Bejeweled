using System;
using SwinGameSDK;

namespace MyGame
{
	public class YellowBlock:ColorBlock
	{
		public YellowBlock (float x, float y):base (x, y, Color.Yellow)
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
			//Color myColor = Color.Red;
			if (Color == Color.Yellow)
			{
				//sprite = "diamondyellow";
				sprite = "yellowDiamond";
				//myColor = Color.LightYellow;
			}
			else if (Color == Color.Red)
			{
				//sprite = "diamondred";
				sprite = "redDiamond";
				//myColor = Color.Bisque;
			} else if (Color == Color.Blue)
			{
				//sprite = "diamondblue";
				sprite = "yellowDiamond";
				//myColor = Color.Lavender;
			} else if (Color == Color.Green)
			{
				//sprite = "diamondgreen";
				sprite = "greenDiamond";
				//myColor = Color.Azure;
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

