using System;
using SwinGameSDK;
namespace MyGame
{
	public static class GameController
	{
		private static GameState gameState;
		private static Board myBoard;
		private static ColorBlock firstSelected = null, secondSelected = null;

		public static Board Board
		{
			get{ return myBoard; }
			set{ myBoard = value; }
		}

		public static ColorBlock FirstSelected
		{
			get{ return firstSelected; }
			set{ firstSelected = value; }
		}

		public static ColorBlock SecondSelected
		{
			get{ return secondSelected; }
			set{ secondSelected = value; }
		}

		public static GameState GameState
		{
			get{ return gameState; }
			set { gameState = value; }
		}

		static GameController ()
		{
			gameState = GameState.ViewingGameMenu;
			myBoard = new Board ();
			firstSelected = null;
			secondSelected = null;
		}

		public static void HandleUserInput()
		{
			HandleShortcutKey ();

			switch (gameState)
			{
			case GameState.ViewingGameMenu:	
				HandleGameMenuInput ();
				break;
			case GameState.PlayingGame:
				HandleGamePlayInput ();
				break;
			case GameState.ViewingGameInstruction:
				HandleInstructionInput ();
				break;
			case GameState.EndingGame:
				HandleEndOfGameInput ();
				break;
			}
		}

		public static void HandleGameMenuInput()
		{
			//SwinGame.ProcessEvents();
			if (SwinGame.MouseClicked (MouseButton.LeftButton))
			{
				if (SwinGame.PointInRect (SwinGame.MousePosition (), 253, 226, 261, 59))
				{
					//start the game and timer
					gameState = GameState.PlayingGame;
					SwinGame.StartTimer (UIController.gameTimer);
					SwinGame.PlaySoundEffect (UIController.GameSound ("startgame"));
				}
				else if (SwinGame.PointInRect (SwinGame.MousePosition (), 253, 302, 261, 59))
				{
					//show the instructions
					gameState = GameState.ViewingGameInstruction;
				} 
				else if (SwinGame.PointInRect (SwinGame.MousePosition (), 253, 375, 261, 59))
				{
					//quit the game
					gameState = GameState.ViewingHighScore;
					//break;
				}
				else if (SwinGame.PointInRect (SwinGame.MousePosition (), 253, 449, 261, 59))
				{
					//quit the game
					gameState = GameState.Quitting;
					//break;
				}
			}
		}

		public static void HandleGamePlayInput()
		{
			//SwinGame.ProcessEvents ();
			//pressing P to pause game
			if (SwinGame.KeyTyped (KeyCode.vk_p))
			{
				if (!UIController.TimerPaused)
				{
					SwinGame.PauseTimer ("timer");
					UIController.TimerPaused = true;
				}
				else
				{
					SwinGame.ResumeTimer ("timer");
					UIController.TimerPaused = false;
				}
			}

			if (SwinGame.MouseClicked (MouseButton.LeftButton))
			{
				myBoard.SelectBlockAt (SwinGame.MousePosition ());
				firstSelected = myBoard.SelectedBlock;

				if (firstSelected != null) {
					SwinGame.PlaySoundEffect (UIController.GameSound ("leftclick"));
				}

				if (SwinGame.PointInRect (SwinGame.MousePosition (), 565, 400, 160, 71))	//menu button
				{
					//Go back to the game menu page
					gameState = GameState.ViewingGameMenu;
				}
				else if (SwinGame.PointInRect (SwinGame.MousePosition (), 565, 480, 160, 71))	//exit button
				{
					gameState = GameState.Quitting;
				} else if(SwinGame.PointInRect (SwinGame.MousePosition (), 565, 320, 160, 71))
				{
					if (!UIController.TimerPaused)
					{
						SwinGame.PauseTimer ("timer");
						UIController.TimerPaused = true;
					}
					else
					{
						SwinGame.ResumeTimer ("timer");
						UIController.TimerPaused = false;
					}
				}
			}

			if (SwinGame.MouseClicked (MouseButton.RightButton))
			{
				myBoard.SelectBlockAt (SwinGame.MousePosition ());
				secondSelected = myBoard.SelectedBlock;

				if (secondSelected != null) {
					SwinGame.PlaySoundEffect (UIController.GameSound ("rightclick"));
				}
			}

			//Swap the selected blocks
			if (firstSelected != null && secondSelected != null)
			{
				myBoard.Swap (firstSelected, secondSelected);

				if (myBoard.CheckMatching() == true)
					SwinGame.PlaySoundEffect (UIController.GameSound ("match"));
				if (myBoard.CheckMatching () == false) {
					myBoard.Swap (firstSelected, secondSelected);
					SwinGame.PlaySoundEffect (UIController.GameSound ("errormatch"));
					firstSelected = null;
					secondSelected = null;
				}

			}

			//if there is matching of blocks in the board, check the matching, else swap the blocks back to original places
			if (myBoard.CheckMatching ())
			{
				myBoard.CheckMatching ();
				firstSelected = null;
				secondSelected = null;
			}
			else
			{
				myBoard.Swap (firstSelected, secondSelected);
			}

		}

		public static void HandleInstructionInput()
		{
			if (SwinGame.MouseClicked (MouseButton.LeftButton))
			{
				if (SwinGame.PointInRect (SwinGame.MousePosition (), 280, 500, 200, 99))
				{
					//Go back to the game menu page
					gameState = GameState.ViewingGameMenu;
				}
			}
		}

		public static void HandleRankinginput(){
			if (SwinGame.MouseClicked (MouseButton.LeftButton))
			{
				if (SwinGame.PointInRect (SwinGame.MousePosition (), 280, 500, 200, 99))
				{
					//Go back to the game menu page
					gameState = GameState.ViewingGameMenu;
				}
			}

		}

		public static void HandleEndOfGameInput()
		{
			if (SwinGame.MouseClicked (MouseButton.LeftButton))
			{
				if (SwinGame.PointInRect (SwinGame.MousePosition (), 500, 500, 200, 99))
				{
					//gameState = GameState.PlayingGame;

					//myBoard = new Board ();
					//myBoard.GenerateBlock ();

					//check for matching, delete matching blocks and generate new blocks until there is no matching blocks in it
					//while (myBoard.CheckMatching ())
					//{
					//	myBoard.CheckMatching ();
					//	myBoard.GenerateBlock ();
					//}
					SwinGame.ResetTimer (UIController.gameTimer);
					myBoard.Score = 0;
					UIController.EndTime = 60;
					GameState = GameState.PlayingGame;
				}
				else if (SwinGame.PointInRect (SwinGame.MousePosition (), 60, 500, 200, 99))
				{
					gameState = GameState.ViewingGameMenu;
				}
			}
		}

		public static void HandleShortcutKey()
		{
			if (SwinGame.KeyTyped (KeyCode.vk_q))
			{
				gameState = GameState.Quitting;
			}
			else if (SwinGame.KeyTyped (KeyCode.vk_m))
			{
				gameState = GameState.ViewingGameMenu;
				myBoard.Score = 0; 
				UIController.EndTime = 60;
			}
			else if (SwinGame.KeyTyped (KeyCode.vk_s))
			{
				gameState = GameState.ViewingHighScore;
			} 

		}
	}
}

