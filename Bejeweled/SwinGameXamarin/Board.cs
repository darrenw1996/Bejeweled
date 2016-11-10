using System;
using SwinGameSDK;
using System.Collections.Generic;
using System.Threading;

namespace MyGame
{
	public class Board
	{
		private ColorBlock[,] _blocks; 
		private int _score;

		public Board ()
		{
			_blocks = new ColorBlock[9, 9];
			_score = 0;
		}

		public void GenerateBlock()
		{
			BlockFactory myFactory = new BlockFactory ();
			int a = 12, b = 12; //a for x position, b for y position of blocks

			while(b < 500){

				for (int x = 0; x < 9; x++)
				{
					for (int y = 0; y < 9; y++)
					{
						if (_blocks[x, y] == null || _blocks [x, y].Color == Color.Black)
						{
							
							AddBlock (x, y, myFactory.CreateRandBlock (a, b));
								
						}
							
						//increment x position
						a += 59;
					}

					//reset x position to 12 and increment y position
					a = 12;
					b += 69;
				}
			}
				
		}

		public void GenerateBlock2()
		{
			List<ColorBlock> colorBlocks = new List<ColorBlock> ();
			BlockFactory myFactory = new BlockFactory ();
			int a = 12, b = 12; //a for x position, b for y position of blocks

			while(b < 500){

				for (int x = 0; x < 9; x++)
				{
					for (int y = 0; y < 9; y++)
					{
						if (_blocks[x, y] == null || _blocks [x, y].Color == Color.Black)
						{




							AddBlock (x, y, myFactory.CreateRandBlock (a, b));

							colorBlocks.Add (_blocks [x, y]);



						}

						//increment x position
						a += 59;
					}

					//reset x position to 12 and increment y position
					a = 12;
					b += 69;
				}
			}
			List<Sprite> _destroyedSprites = new List<Sprite> ();
			Sprite sprite =SwinGame.CreateSprite (SwinGame.BitmapNamed ("destroyedani"), SwinGame.AnimationScriptNamed ("destroyedanimation"));
			foreach (ColorBlock block in colorBlocks)
			{
				sprite = SwinGame.CreateSprite (SwinGame.BitmapNamed ("destroyedani"), SwinGame.AnimationScriptNamed ("destroyedanimation"));
				SwinGame.SpriteStartAnimation (sprite, "dematerialize");
				SwinGame.SpriteSetX (sprite, block.X-20);
				SwinGame.SpriteSetY (sprite, block.Y-15);
				_destroyedSprites.Add (sprite);


			}

			if (!sprite.AnimationHasEnded)
			{
				SwinGame.PauseTimer("timer");
			}

			do
			{
				SwinGame.DrawAllSprites();

				SwinGame.RefreshScreen(60);
				SwinGame.UpdateAllSprites();
			} while(!sprite.AnimationHasEnded);

			if (sprite.AnimationHasEnded)
			{
				SwinGame.ResumeTimer("timer");
			}


		}


		public void AddBlock(int x, int y, ColorBlock block)
		{
			_blocks [x, y] = block;
		}

		public void RemoveBlock(List<ColorBlock> blocks)
		{
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					foreach (ColorBlock block in blocks)
					{
						if (_blocks [x, y] == block)
						{
							_blocks [x, y].Color = Color.Black;
						}
					}
				}
			}

		}

		public ColorBlock[,] Block
		{
			get { return _blocks; }
			set { _blocks = value; }
		}

		public void DrawBoard()
		{
			/*for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					_blocks [x, y].Draw ();
				}
			}*/
			UIController.DrawGameBoard (_blocks);
		}

		public void Swap(ColorBlock firstSelected, ColorBlock secondSelected)
		{
			RedBlock temp = new RedBlock(0, 0);

			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					if (_blocks [x, y] == firstSelected)
					{
						//if the first selected and second selected are neighbour blocks, swap them
						if ((y >= 1 && _blocks[x, y-1] == secondSelected) || (y <= 7 && _blocks[x, y+1] == secondSelected) || ( x >= 1 && _blocks[x-1, y] == secondSelected)|| (x <= 7 && _blocks[x+1, y] == secondSelected))
						{					
							temp.Color = firstSelected.Color;
							firstSelected.Color = secondSelected.Color;
							secondSelected.Color = temp.Color;

							/* JOSEPH - create temporary sprite */
							/*
							Sprite firstSprite;
							Sprite secondSprite;

							if (firstSelected.Color == Color.Yellow) { 
								SwinGame.SpriteSetX (UIController.getSprite (sprite), X + 10);
								SwinGame.SpriteSetY (UIController.getSprite (sprite), Y + 15);
								SwinGame.DrawSprite (UIController.getSprite (sprite));
							}
							*/

								//Sprite firstSprite = firstSelected.Sprite;
								//Sprite secondSprite = secondSelected.Sprite;

							if (((y >= 1 && _blocks [x, y - 1] == secondSelected) || (y <= 7 && _blocks [x, y + 1] == secondSelected)) && secondSelected.Selected) {

								SwinGame.SpriteSetX (firstSelected.Sprite, firstSelected.X+10);
								SwinGame.SpriteSetY (firstSelected.Sprite, firstSelected.Y+15);
								SwinGame.SpriteSetX (secondSelected.Sprite, secondSelected.X+10);
								SwinGame.SpriteSetY (secondSelected.Sprite, secondSelected.Y+15);

								SwinGame.ProcessEvents ();
								SwinGame.DrawFramerate (0,0);

								SwinGame.DrawSprite (firstSelected.Sprite);
								SwinGame.UpdateAllSprites ();
								SwinGame.DrawSprite (secondSelected.Sprite);
								SwinGame.UpdateAllSprites ();

								SwinGame.RefreshScreen (60);

								SwinGame.SpriteSetDX (firstSelected.Sprite, 10);
								SwinGame.SpriteSetDY (firstSelected.Sprite, 10);
								SwinGame.SpriteSetDX (secondSelected.Sprite, 10);
								SwinGame.SpriteSetDY (secondSelected.Sprite, 10);

								SwapAnimation (firstSelected.Sprite, -1, 0);
								SwapAnimation (secondSelected.Sprite, +1, 0);
								break;
								//SwinGame.UpdateSprite (firstSelected.Sprite);
								//SwinGame.UpdateSprite (secondSelected.Sprite);
							} 
							if (((x >= 1 && _blocks [x - 1, y] == secondSelected) || (x <= 7 && _blocks [x + 1, y] == secondSelected)) && secondSelected.Selected) {
								SwinGame.SpriteSetX (firstSelected.Sprite, firstSelected.X+10);
								SwinGame.SpriteSetY (firstSelected.Sprite, firstSelected.Y+15);
								SwinGame.SpriteSetX (secondSelected.Sprite, secondSelected.X+10);
								SwinGame.SpriteSetY (secondSelected.Sprite, secondSelected.Y+15);

								SwinGame.SpriteSetDX (firstSelected.Sprite, 10);
								SwinGame.SpriteSetDY (firstSelected.Sprite, 10);
								SwinGame.SpriteSetDX (secondSelected.Sprite, 10);
								SwinGame.SpriteSetDY (secondSelected.Sprite, 10);

								SwapAnimation (firstSelected.Sprite, 0, -1);
								SwapAnimation (secondSelected.Sprite, 0, +1);
								break;
								//SwinGame.UpdateSprite (firstSelected.Sprite);
								//SwinGame.UpdateSprite (secondSelected.Sprite);
							}

						}
					}
				}
			}

		}

		public void MoveSprite ()
		{
			
		}

		public void SwapAnimation (Sprite sprt, float dx, float dy)
		{
				SwinGame.SpriteSetDX (sprt, dx);
				SwinGame.SpriteSetDY (sprt, dy);
		}

		public bool CheckMatching()
		{
			List<ColorBlock> clusters = new List<ColorBlock> ();
			int match = 0;
			bool startCheck = false;

			//check horizontal match
			for (int x = 0; x < 9; x++)
			{
				match = 1;
				for (int y = 0; y < 9; y++)
				{
					startCheck = false;
					//if it is the last column in the row, start to check the matching
					if (y == 8)
					{
						startCheck = true;
					}
					else
					{
						if (_blocks [x, y].Color == _blocks [x, y + 1].Color)
						{
							match++;
						}
						else
						{
							startCheck = true;
						}
					}
						
					if (startCheck)
					{
						//if matching is greater than or equal to 3, add it into the clusters list 
						if (match >= 3)
						{
							for (int i = 0; i < match; i++)
							{
								clusters.Add(_blocks[x, y-i]);

							}

						}
						//reset the match for next checking
						match = 1;
					}
				}
			}

			//check vertical match
			for (int x = 0; x < 9; x++)
			{
				match = 1;
				for (int y = 0; y < 9; y++)
				{
					startCheck = false;

					//if it is the last row in the column, start to check the matching
					if (y == 8)
					{
						startCheck = true;
					}
					else
					{
						if (_blocks [y, x].Color == _blocks [y + 1, x].Color)
						{
							match++;
						}
						else
						{
							startCheck = true;
						}
					}


					if (startCheck)
					{
						//if matching is greater than or equal to 3, add it into the clusters list
						if (match >= 3)
						{
							for (int i = 0; i < match; i++)
							{
								clusters.Add (_blocks [y - i, x]);

							}
						}

						//reset the match for next checking
						match = 1;
					}
				}
			}

			//if no match, return false, else, remove the matching blocks and calculate score
			if (clusters.Count == 0)
			{
				return false;
			}
			else
			{
				RemoveBlock (clusters);
				CalScore ();
				return true;
			}

		}

		public bool CheckMatchingHorizontal ()
		{
			List<ColorBlock> clusters = new List<ColorBlock> ();
			int match = 0;
			bool startCheck = false;

			//check horizontal match
			for (int x = 0; x < 9; x++) {
				match = 1;
				for (int y = 0; y < 9; y++) {
					startCheck = false;
					//if it is the last column in the row, start to check the matching
					if (y == 8) {
						startCheck = true;
					} else {
						if (_blocks [x, y].Color == _blocks [x, y + 1].Color) {
							match++;
						} else {
							startCheck = true;
						}
					}

					if (startCheck) {
						//if matching is greater than or equal to 3, add it into the clusters list 
						if (match >= 3) {
							for (int i = 0; i < match; i++) {
								clusters.Add (_blocks [x, y - i]);
							}
						}
						//reset the match for next checking
						match = 1;
					}
				}
			}

			//if no match, return false, else, remove the matching blocks and calculate score
			if (clusters.Count == 0) {
				return false;
			} else {
				return true;
			}

		}

		public bool CheckMatchingVertical ()
		{
			List<ColorBlock> clusters = new List<ColorBlock> ();
			int match = 0;
			bool startCheck = false;

			//check vertical match
			for (int x = 0; x < 9; x++) {
				match = 1;
				for (int y = 0; y < 9; y++) {
					startCheck = false;

					//if it is the last row in the column, start to check the matching
					if (y == 8) {
						startCheck = true;
					} else {
						if (_blocks [y, x].Color == _blocks [y + 1, x].Color) {
							match++;
						} else {
							startCheck = true;
						}
					}


					if (startCheck) {
						//if matching is greater than or equal to 3, add it into the clusters list
						if (match >= 3) {
							for (int i = 0; i < match; i++) {
								clusters.Add (_blocks [y - i, x]);
							}
						}
						//reset the match for next checking
						match = 1;
					}
				}
			}

			//if no match, return false, else, remove the matching blocks and calculate score
			if (clusters.Count == 0) {
				return false;
			} else {

				return true;
			}

		}

		public void CalScore()
		{

			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					if (_blocks [x, y].Color == Color.Black)
					{
						_score++;
					}
				}
			}
		}

		public void SelectBlockAt(Point2D pt)
		{
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					if (_blocks [x, y].IsAt (pt))
					{
						_blocks [x, y].Selected = true;
					}
					else
					{
						_blocks [x, y].Selected = false;
					}
				}
			}
		}

		public ColorBlock SelectedBlock
		{
			get {
				ColorBlock selected = null;
				for (int x = 0; x < 9; x++)
				{
					for (int y = 0; y < 9; y++)
					{
						if (_blocks [x, y].Selected)
						{
							selected = _blocks [x, y];
						}
					}
				}
				return selected;
			}
		}

		public int Score
		{
			get { return _score; }
			set { _score = value; }
		}
			
	}

}

