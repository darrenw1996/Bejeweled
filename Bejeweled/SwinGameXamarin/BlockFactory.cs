using System;

namespace MyGame
{
	public class BlockFactory
	{
		private Random _randColor;

		public BlockFactory ()
		{
			_randColor = new Random ();	
		}

		public ColorBlock CreateRandBlock(float x, float y)
		{
			if (_randColor.NextDouble() <= 0.2)
			{
				return new RedBlock (x, y);
			}
			else if (_randColor.NextDouble() <= 0.4)
			{
				return new BlueBlock (x, y);
			}
			else if (_randColor.NextDouble() <= 0.6)
			{
				return new GreenBlock (x, y);
			}
			else
			{
				return new YellowBlock (x, y);
			}
		}

		public ColorBlock CreateTimerBlock(float x, float y)
		{
			return new TimerBlock (x, y);
		}

		public ColorBlock CreateRainbowBlock(float x, float y)
		{
			return new RainbowBlock (x, y);
		}
			
	}
}

