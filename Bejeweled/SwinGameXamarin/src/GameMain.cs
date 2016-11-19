using System;
using SwinGameSDK;

namespace MyGame
{
	public class GameMain
    {
		
        public static void Main()
        {
            //Open the game window
			SwinGame.OpenGraphicsWindow("Bejeweled", 750, 630); //549, 700
			UIController.LoadResources ();

			SwinGame.PlayMusic (UIController.GameMusic ("Background"));

			//generate blocks for the board
			GameController.Board.GenerateBlock ();

			//check for matching, delete matching blocks and generate new blocks until there is no matching blocks in it
			while (GameController.Board.CheckMatching ())
			{
				GameController.Board.CheckMatching ();
				GameController.Board.GenerateBlock ();
			}

			//initialize the score to 0
			GameController.Board.Score = 0;

            //Run the game loop
			while(false == SwinGame.WindowCloseRequested() && GameController.GameState != GameState.Quitting)
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                
                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.Black);

				if (GameController.GameState == GameState.ViewingGameMenu)
				{
					UIController.DrawMenuPage ();
					GameController.HandleUserInput ();
				}
					
				//show the instruction page
				if (GameController.GameState == GameState.ViewingGameInstruction)
				{
					UIController.DrawInstructionPage ();
					GameController.HandleUserInput ();
				}

				//if player chose to start the game, display the board and start the game
				if (GameController.GameState == GameState.PlayingGame)
				{
					//Draw game board
					GameController.Board.DrawBoard ();

					SwinGame.StopMusic ();

					//when time reaches 1 minute, show the final score page
					if (UIController.TimeTicks >= UIController.EndTime)
					{
						GameController.GameState = GameState.EndingGame;
						SwinGame.PlaySoundEffect (UIController.GameSound("endgame"));
					}
						
					GameController.HandleUserInput ();
					GameController.Board.GenerateBlock2 ();
				}

				if (GameController.GameState == GameState.ViewingHighScore)
				{
					UIController.DrawRanking ();
					HighScore.DrawHighScores ();
					GameController.HandleRankinginput ();
				}


				//if the game ends, show the final score page
				if (GameController.GameState == GameState.EndingGame)
				{
					UIController.DrawEndGame();
					HighScore.ReadUserScore (GameController.Board.Score);


					//GameController.HandleUserInput ();
					//GameController.HandleEndOfGameInput ();

					//HighScore.ReadUserScore (GameController.Board.Score);

				}

				if (GameController.GameState == GameState.EndingRanking)
				{
					UIController.DrawEndRanking();
					HighScore.DrawHighScores ();
					GameController.HandleEndOfGameInput ();
				}

				SwinGame.RefreshScreen(60);
				SwinGame.UpdateAllSprites ();
			}

			UIController.FreeResources ();
        }
    }
}