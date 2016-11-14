using System;
using SwinGameSDK;

namespace MyGame
{
	public class GreenBlock:ColorBlock
	{
		public GreenBlock (float x, float y):base (x, y, Color.Green)
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
				sprite = "yellowDiamond";
			}
			else if (Color == Color.Red)
			{
				sprite = "redDiamond";
			} else if (Color == Color.Blue)
			{
				sprite = "blueDiamond";
			} else if (Color == Color.Green)
			{
				sprite = "greenDiamond";
			} else if (Color == Color.White)
			{
				sprite = "timerBlock";
			} else if (Color == Color.MistyRose)
			{
				sprite = "rainbowDiamond";
			}
			//SwinGame.FillRectangle (Color.Maroon, X, Y, Width, Height);
			//SwinGame.FillRectangle (Color.SaddleBrown, X+5, Y+5, Width-10, Height-10);

			SwinGame.SpriteSetX (UIController.getSprite (sprite), X+10);
			SwinGame.SpriteSetY (UIController.getSprite (sprite), Y+15);
			SwinGame.DrawSprite (UIController.getSprite (sprite));
		}
	}
}

