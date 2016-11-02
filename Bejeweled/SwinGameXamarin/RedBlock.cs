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
			SwinGame.FillRectangle (Color, X, Y, Width, Height);
		}
	}
}

