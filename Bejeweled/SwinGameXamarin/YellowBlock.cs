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
			SwinGame.FillRectangle (Color, X, Y, Width, Height);
			 
		}
	}
}

