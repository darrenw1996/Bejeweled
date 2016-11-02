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
			SwinGame.FillRectangle (Color, X, Y, Width, Height);
		}
	}
}

