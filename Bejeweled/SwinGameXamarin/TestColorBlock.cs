using System;
using NUnit.Framework;
using SwinGameSDK;
namespace MyGame
{
	[TestFixture()]
	public class TestColorBlock
	{
		[Test()]
		public void TestBlockIsAt ()
		{
			RedBlock red = new RedBlock (0, 0); //default width 60, height 50

			Assert.IsTrue (red.IsAt (SwinGame.PointAt (50, 50)));
			Assert.IsTrue (red.IsAt (SwinGame.PointAt (25, 25)));
			Assert.IsFalse (red.IsAt (SwinGame.PointAt (70, 50)));
			Assert.IsFalse (red.IsAt (SwinGame.PointAt (50, 70)));
		}

		[Test()]
		public void TestBlockIsAtWhenMoved()
		{
			GreenBlock g = new GreenBlock (0, 0);

			Assert.IsTrue (g.IsAt (SwinGame.PointAt (50, 50)));
			Assert.IsTrue (g.IsAt (SwinGame.PointAt (25, 25)));
			Assert.IsFalse (g.IsAt (SwinGame.PointAt (70, 50)));
			Assert.IsFalse (g.IsAt (SwinGame.PointAt (50, 70)));

			g.X = 100;
			g.Y = 100;

			Assert.IsTrue (g.IsAt (SwinGame.PointAt (110, 110)));
			Assert.IsTrue (g.IsAt (SwinGame.PointAt (120, 120)));
			Assert.IsFalse (g.IsAt (SwinGame.PointAt (25, 50)));
			Assert.IsFalse (g.IsAt (SwinGame.PointAt (50, 25)));
		}
			
		[Test()]
		public void TestSelectedValue()
		{
			BlueBlock blue = new BlueBlock (0, 0);
			Assert.IsFalse (blue.Selected);

			blue.Selected = true;
			Assert.IsTrue (blue.Selected);
		}

		[Test()]
		public void TestConstructor()
		{
			YellowBlock yellow = new YellowBlock (0, 20);
			Assert.AreEqual (Color.Yellow, yellow.Color);
			Assert.AreEqual (0, yellow.X);
			Assert.AreEqual (20, yellow.Y);
			Assert.AreEqual (50, yellow.Width);
			Assert.AreEqual (60, yellow.Height);
		}

		[Test()]
		public void TestBlockColor()
		{
			RedBlock red = new RedBlock (0, 0);
			GreenBlock green = new GreenBlock (0, 0);
			BlueBlock blue = new BlueBlock (0, 0);
			YellowBlock yellow = new YellowBlock (0, 20);

			Assert.AreEqual (Color.Yellow, yellow.Color);
			Assert.AreEqual (Color.Red, red.Color);
			Assert.AreEqual (Color.Blue, blue.Color);
			Assert.AreEqual (Color.Green, green.Color);
		}
	}
}

