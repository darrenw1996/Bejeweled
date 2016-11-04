using System;
using SwinGameSDK;

namespace MyGame
{
	public class RedBlock:ColorBlock
	{
		public RedBlock (float x, float y):base (x, y, Color.Red)
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
			}

			SwinGame.SpriteSetX (UIController.getSprite (sprite), X+10);
			SwinGame.SpriteSetY (UIController.getSprite (sprite), Y+15);
			SwinGame.DrawSprite (UIController.getSprite (sprite));
		}
	}
}

