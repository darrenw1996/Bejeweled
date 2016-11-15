using System;
using SwinGameSDK;
using System.Collections.Generic;
using System.Threading;

namespace MyGame
{
	public class Board
	{
		private ColorBlock[,] _blocks; 
		private int _score, _scoreRed=4, _scoreBlue=3, _scoreGreen=2, _scoreYellow=1, _matchCount=1;
		private bool _matchRed=false, _matchBlue=false, _matchGreen=false, _matchYellow=false;
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
						if (_blocks[x, y] == null || _blocks [x, y].Color == Color.Black || _blocks[x, y].Color == Color.Wheat || _blocks[x, y].Color == Color.Grey)
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
						if (_blocks [x, y] == null || _blocks [x, y].Color == Color.Black)
						{
							colorBlocks.Add (_blocks [x, y]);
							AddBlock (x, y, myFactory.CreateRandBlock (a, b));
						}
						else if (_blocks [x, y].Color == Color.Wheat)
						{
							colorBlocks.Add (_blocks [x, y]);
							AddBlock (x, y, myFactory.CreateTimerBlock (a, b));
						}
						else if (_blocks [x, y].Color == Color.Grey)
						{
							colorBlocks.Add (_blocks [x, y]);
							AddBlock (x, y, myFactory.CreateRainbowBlock (a, b));
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
				//SwinGame.DrawAllSprites();
				foreach(Sprite _sprite in _destroyedSprites)
				{
					SwinGame.DrawSprite(_sprite);
				}
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

		public void RemoveBlock(List<ColorBlock> blocks, List<ColorBlock> timerBlocks, List<ColorBlock> rainbowBlocks)
		{
			//change the color of the blocks to be removed to black color 
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					foreach (ColorBlock block in blocks)
					{
						if (_blocks [x, y] == block)
						{
							/* JOSEPH - different score for different colored diamonds */
							if (_blocks [x, y].Color == Color.Red) {
								_matchRed = true;
							}
							if (_blocks [x, y].Color == Color.Blue) {
								_matchBlue = true;
							}
							if (_blocks [x, y].Color == Color.Green) {
								_matchGreen = true;
							}
							if (_blocks [x, y].Color == Color.Yellow) {
								_matchYellow = true;
							}
							_blocks [x, y].Color = Color.Black;
						}
					}
				}
			}

			//change block color to wheat so that timer block will be generated for wheat color block
			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					foreach (ColorBlock timerBlock in timerBlocks)
					{
						if (_blocks [x, y] == timerBlock)
						{
							_blocks [x, y].Color = Color.Wheat;
						}
					}
				}
			}

			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					foreach (ColorBlock block in rainbowBlocks)
					{
						if (_blocks [x, y] == block)
						{
							_blocks [x, y].Color = Color.Grey;
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
			
			UIController.DrawGameBoard (this, _blocks);
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

							if (((y >= 1 && _blocks [x, y - 1] == secondSelected) || (y <= 7 && _blocks [x, y + 1] == secondSelected)) && secondSelected.Selected) {

								string sprt1 = "", sprite_first = "";
								string sprt2 = "", sprite_second = "";

								if (firstSelected.Color == Color.Red) {
									sprt1 = "redDiamond";
									sprite_first = "diamondred";
								} else if (firstSelected.Color == Color.Blue) {
									sprt1 = "blueDiamond";
									sprite_first = "diamondblue";
								} else if (firstSelected.Color == Color.Green) {
									sprt1 = "greenDiamond";
									sprite_first = "diamondgreen";
								} else if (firstSelected.Color == Color.Yellow) {
									sprt1 = "yellowDiamond";
									sprite_first = "diamondyellow";
								}

								if (secondSelected.Color == Color.Red) {
									sprt2 = "redDiamond";
									sprite_second = "diamondred";
								} else if (secondSelected.Color == Color.Blue) {
									sprt2 = "blueDiamond";
									sprite_second = "diamondblue";
								} else if (secondSelected.Color == Color.Green) {
									sprt2 = "greenDiamond";
									sprite_second = "diamondgreen";
								} else if (secondSelected.Color == Color.Yellow) {
									sprt2 = "yellowDiamond";
									sprite_second = "diamondyellow";
								}

								/*
								SwinGame.SpriteSetX (UIController.getSprite (sprt1), secondSelected.X + 10);
								SwinGame.SpriteSetY (UIController.getSprite (sprt1), secondSelected.Y + 15);
								SwinGame.SpriteSetX (UIController.getSprite (sprt2), firstSelected.X + 10);
								SwinGame.SpriteSetY (UIController.getSprite (sprt2), firstSelected.Y + 15);

								SwinGame.DrawSprite (UIController.getSprite (sprt1));
								SwinGame.DrawSprite (UIController.getSprite (sprt2));
*/
								Sprite sprite1 = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite_first), SwinGame.AnimationScriptNamed ("diamondanimation"));
								Sprite sprite2 = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite_second), SwinGame.AnimationScriptNamed ("diamondanimation"));

								SwinGame.SpriteSetX (sprite1, firstSelected.X + 10);
								SwinGame.SpriteSetY (sprite1, firstSelected.Y + 15);
								SwinGame.SpriteSetX (sprite2, secondSelected.X + 10);
								SwinGame.SpriteSetY (sprite2, secondSelected.Y + 15);

								SwinGame.DrawSprite (sprite1);
								SwinGame.DrawSprite (sprite2);

								SwinGame.UpdateSprite (sprite1);
								SwinGame.UpdateSprite (sprite2);
								SwinGame.RefreshScreen (30);

								SwinGame.SpriteSetDX (secondSelected.Sprite, 10);
								SwinGame.SpriteSetDY (secondSelected.Sprite, 10);
								SwinGame.SpriteSetDX (secondSelected.Sprite, 10);
								SwinGame.SpriteSetDY (secondSelected.Sprite, 10);

								SwapAnimation (sprite1, -10, 0);
								SwapAnimation (sprite2, +10, 0);

								SwinGame.SpriteSetDX (sprite1, -10);
								SwinGame.SpriteSetDY (sprite1, +10);


								//SwinGame.UpdateSprite (firstSelected.Sprite);
								//SwinGame.UpdateSprite (secondSelected.Sprite);
							}
							if (((x >= 1 && _blocks [x - 1, y] == secondSelected) || (x <= 7 && _blocks [x + 1, y] == secondSelected)) && secondSelected.Selected) {
								
								string sprt1 = "", sprite_first = "";
								string sprt2 = "", sprite_second = "";

								if (firstSelected.Color == Color.Red) {
									sprt1 = "redDiamond";
									sprite_first = "diamondred";
								} else if (firstSelected.Color == Color.Blue) {
									sprt1 = "blueDiamond";
									sprite_first = "diamondblue";
								} else if (firstSelected.Color == Color.Green) {
									sprt1 = "greenDiamond";
									sprite_first = "diamondgreen";
								} else if (firstSelected.Color == Color.Yellow) {
									sprt1 = "yellowDiamond";
									sprite_first = "diamondyellow";
								}

								if (secondSelected.Color == Color.Red) {
									sprt2 = "redDiamond";
									sprite_second = "diamondred";
								} else if (secondSelected.Color == Color.Blue) {
									sprt2 = "blueDiamond";
									sprite_second = "diamondblue";
								} else if (secondSelected.Color == Color.Green) {
									sprt2 = "greenDiamond";
									sprite_second = "diamondgreen";
								} else if (secondSelected.Color == Color.Yellow) {
									sprt2 = "yellowDiamond";
									sprite_second = "diamondyellow";
								}

								/*
								SwinGame.SpriteSetX (UIController.getSprite (sprt1), secondSelected.X + 10);
								SwinGame.SpriteSetY (UIController.getSprite (sprt1), secondSelected.Y + 15);
								SwinGame.SpriteSetX (UIController.getSprite (sprt2), firstSelected.X + 10);
								SwinGame.SpriteSetY (UIController.getSprite (sprt2), firstSelected.Y + 15);

								SwinGame.DrawSprite (UIController.getSprite (sprt1));
								SwinGame.DrawSprite (UIController.getSprite (sprt2));
*/
								Sprite sprite1 = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite_first), SwinGame.AnimationScriptNamed ("diamondanimation"));
								Sprite sprite2 = SwinGame.CreateSprite (SwinGame.BitmapNamed (sprite_second), SwinGame.AnimationScriptNamed ("diamondanimation"));

								SwinGame.SpriteSetX (sprite1, firstSelected.X + 10);
								SwinGame.SpriteSetY (sprite1, firstSelected.Y + 15);
								SwinGame.SpriteSetX (sprite2, secondSelected.X + 10);
								SwinGame.SpriteSetY (sprite2, secondSelected.Y + 15);

								SwinGame.DrawSprite (sprite1);
								SwinGame.DrawSprite (sprite2);

								SwinGame.UpdateSprite (sprite1);
								SwinGame.UpdateSprite (sprite2);
								SwinGame.RefreshScreen (30);

								SwinGame.SpriteSetDX (sprite1, 10);
								SwinGame.SpriteSetDY (sprite1, 10);
								SwinGame.SpriteSetDX (sprite2, 10);
								SwinGame.SpriteSetDY (sprite2, 10);

								SwapAnimation (sprite1, 0, -10);
								SwapAnimation (sprite2, 0, +10);

								//SwinGame.UpdateSprite (firstSelected.Sprite);
								//SwinGame.UpdateSprite (secondSelected.Sprite);
							}
						}
					}
				}
			}

		}

		public void SwapAnimation (Sprite sprt, float dx, float dy)
		{
			SwinGame.SpriteSetDX (sprt, dx);
			SwinGame.SpriteSetDY (sprt, dy);
		}

		public bool CheckMatching()
		{
			List<ColorBlock> clusters = new List<ColorBlock> ();
			List<ColorBlock> timerClusters = new List<ColorBlock> ();	//timer blocks that should be added into the board
			List<ColorBlock> rainbowClusters = new List<ColorBlock>();	//rainbow blocks to be added in
			int match = 0;
			int hMatch = 0, vMatch = 0;
			int timerMatch = 0;
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
						
						if (_blocks [x, y].Color == _blocks [x, y + 1].Color || (y < 7 && (_blocks[x, y].Color == Color.White || _blocks[x, y].Color == Color.MistyRose) && _blocks[x, y+1].Color == _blocks[x, y+2].Color) || (y > 0 && (_blocks[x, y +1].Color == Color.White || _blocks[x, y+1].Color == Color.MistyRose)&& _blocks[x, y -1].Color == _blocks[x, y].Color) || (y > 1 && (_blocks[x, y].Color == Color.White || _blocks[x, y].Color == Color.MistyRose) && _blocks[x, y-1].Color == _blocks[x, y-2].Color && _blocks[x, y-1].Color == _blocks[x, y+1].Color) )
						{
							match++;
						} else
						{
							startCheck = true;
						}
					}
						
					if (startCheck)
					{
						//if matching is greater than or equal to 3, add it into the clusters list 
						if (match == 4)
						{
							if (!rainbowClusters.Contains (_blocks [x, y]))
							{
								rainbowClusters.Add (_blocks [x, y]);
							}

						} else if (match == 5)
						{
							if (!timerClusters.Contains (_blocks [x, y]))
							{
								timerClusters.Add (_blocks [x, y]);
							}
						}

						if (match >= 3)
						{
							hMatch = match;
							_matchCount = match + vMatch;
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
						if (_blocks [y, x].Color == _blocks [y + 1, x].Color || (y < 7 && (_blocks[y, x].Color == Color.White || _blocks[y, x].Color == Color.MistyRose) && _blocks[y+1, x].Color == _blocks[y+2, x].Color) || (y > 0 && (_blocks[y+1, x ].Color == Color.White || _blocks[y+1, x].Color == Color.MistyRose) && _blocks[y-1, x].Color == _blocks[y, x].Color) || (y > 1 && (_blocks[y, x].Color == Color.White || _blocks[y, x].Color == Color.MistyRose) && _blocks[y-1, x].Color == _blocks[y-2, x].Color && _blocks[y-1, x].Color == _blocks[y+1, x].Color))
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
						if (match == 4)
						{
							if (!rainbowClusters.Contains (_blocks [y, x]))
							{
								rainbowClusters.Add (_blocks [y, x]);
							}

						} else if (match == 5)
						{
							if (!timerClusters.Contains (_blocks [y, x]))
							{
								timerClusters.Add(_blocks[y, x]);
							}
						}

						//if matching is greater than or equal to 3, add it into the clusters list
						if (match >= 3)
						{
							vMatch = match;
							_matchCount = match + hMatch;
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
				foreach (ColorBlock block in clusters)
				{
					if (block.Color == Color.White)
					{
						timerMatch++;
					}
				}
				RemoveBlock (clusters, timerClusters, rainbowClusters);
				IncreaseTime (timerMatch);
				CalScore ();
				return true;
			}

		}

		public void IncreaseTime(int timerMatch)
		{
			UIController.endTime += timerMatch;
			if (timerMatch > 0)
			{
				SwinGame.DrawTextLines ("Time Increase!", Color.White, SwinGame.RGBAColor (0, 0, 0, 200), "maven_pro_regular", 30, FontAlignment.AlignCenter, 125, 160, 300, 100);
				SwinGame.Delay (200);
			}

			/*if (timerMatch > 0 && UIController.isTimerFreeze != true)
			{
				SwinGame.PauseTimer ("timer");
				UIController.isTimerFreeze = true;

				if (UIController.isCountStarted == false)
				{
					SwinGame.StartTimer ("countTimer");	//start timer to count
					UIController.isCountStarted = true;
				}

			}

			if (UIController.isTimerFreeze == true && (SwinGame.TimerTicks(UIController.countTimer)/1000) > 3)
			{
				SwinGame.ResumeTimer ("timer");
				UIController.isTimerFreeze = false;

				SwinGame.StopTimer ("countTimer");
				UIController.isCountStarted = false;
			}*/

		}

		public void CalScore()
		{

			for (int x = 0; x < 9; x++)
			{
				for (int y = 0; y < 9; y++)
				{
					if (_blocks [x, y].Color == Color.Black)
					{
						//_score++;
						/* JOSEPH - different score for different colored diamonds */
						if (_matchRed == true) {
							_score = _score + _scoreRed * _matchCount;
							_matchRed = false;
							_matchCount = 1;
						}
						if (_matchBlue == true) {
							_score = _score + _scoreBlue * _matchCount;
							_matchBlue = false;
							_matchCount = 1;
						}
						if (_matchGreen == true) {
							_score = _score + _scoreGreen * _matchCount;
							_matchGreen = false;
							_matchCount = 1;
						}
						if (_matchYellow == true)
						{
							_score = _score + _scoreYellow * _matchCount;
							_matchYellow = false;
							_matchCount = 1;
						}
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

