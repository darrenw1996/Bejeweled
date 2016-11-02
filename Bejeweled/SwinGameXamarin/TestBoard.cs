using System;
using NUnit.Framework;
using SwinGameSDK;
using System.Collections.Generic;

namespace MyGame
{
	[TestFixture()]
	public class TestBoard
	{
		[Test()]
		public void TestAddBlock ()
		{
			RedBlock red = new RedBlock (20, 20);
			BlueBlock blue = new BlueBlock (0, 0);
			Board board = new Board ();
			board.AddBlock (0, 0, red);
			board.AddBlock (0, 1, blue);

			Assert.AreSame (blue, board.Block [0, 1]);
		}

		[Test()]
		public void TestRemoveBlock()
		{
			Board board = new Board ();
			List<ColorBlock> clusters = new List<ColorBlock> ();
			board.GenerateBlock ();

			for (int x = 0; x < 1; x++)
			{
				for (int y = 0; y < 4; y++)
				{
					clusters.Add (board.Block [x, y]);
				}
			}
			//remove 4 blocks 
			board.RemoveBlock (clusters);
			board.CalScore ();
			//check that there are 4 blocks in black color
			Assert.AreEqual (4, board.Score);

		}

		[Test()]
		public void TestSwapLeftRight()
		{
			RedBlock red = new RedBlock (20, 20);
			BlueBlock blue = new BlueBlock (0, 0);
			Board board = new Board ();
			//add blocks into board
			//red and blue blocks are next to each other
			board.AddBlock (0, 0, red);
			board.AddBlock (0, 1, blue);

			board.Swap (red, blue);
			Assert.AreEqual (Color.Blue, red.Color);
			Assert.AreEqual (Color.Red, blue.Color);

			board.Swap (red, blue);
			Assert.AreEqual (Color.Red, red.Color);
			Assert.AreEqual (Color.Blue, blue.Color);
		}

		[Test()]
		public void TestSwapUpDown()
		{
			RedBlock red = new RedBlock (20, 20);
			BlueBlock blue = new BlueBlock (0, 0);
			Board board = new Board ();
			//add blocks into board
			//red block is above blue block
			board.AddBlock (0, 0, red);
			board.AddBlock (1, 0, blue);

			board.Swap (red, blue);
			Assert.AreEqual (Color.Blue, red.Color);
			Assert.AreEqual (Color.Red, blue.Color);

			board.Swap (red, blue);
			Assert.AreEqual (Color.Red, red.Color);
			Assert.AreEqual (Color.Blue, blue.Color);
		}

		[Test()]
		public void TestSwapNotNeighbour()
		{
			RedBlock red = new RedBlock (20, 20);
			BlueBlock blue = new BlueBlock (0, 0);
			Board board = new Board ();
			//add blocks into board
			board.AddBlock (0, 0, red);
			board.AddBlock (1, 1, blue);

			//the color of both blocks should remain unchanged as they are not neighbour block
			board.Swap (red, blue);
			Assert.AreEqual (Color.Red, red.Color);
			Assert.AreEqual (Color.Blue, blue.Color);
		}

		[Test()]
		public void TestCalScore()
		{
			Board board = new Board ();
			board.GenerateBlock ();
			//set to blocks to block color
			board.Block [0, 1].Color = Color.Black;
			board.Block [0, 4].Color = Color.Black;

			//check that the score is 2 as there are 2 blocks in black
			board.CalScore ();
			Assert.AreEqual (2, board.Score);
		}
	}
}

