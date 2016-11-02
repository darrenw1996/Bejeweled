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
			SwinGame.FillRectangle (Color, X, Y, Width, Height);
		}
	}
}

