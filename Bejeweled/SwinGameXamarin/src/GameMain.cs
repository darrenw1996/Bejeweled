using System;
using SwinGameSDK;

namespace MyGame
{
	public class GameMain
    {
		
        public static void Main()
        {
            //Open the game window
			SwinGame.OpenGraphicsWindow("Bejeweled", 549, 700); 
			UIController.LoadResources ();

			bool startGame = false;   
			bool endGame = false;    //display final score page
			bool instruction = false;  //instruction page
			Timer gameTime = SwinGame.CreateTimer("timer");	//game time
			uint ticks;   //timer ticks


			Board myBoard = new Board ();
			ColorBlock firstSelected = null, secondSelected = null;

			//generate blocks for the board
			myBoard.GenerateBlock ();

			//check for matching, delete matching blocks and generate new blocks until there is no matching blocks in it
			while (myBoard.CheckMatching ())
			{
				myBoard.CheckMatching ();
				myBoard.GenerateBlock ();
			}

			//initialize the score to 0
			myBoard.Score = 0;

            //Run the game loop
			while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();
                
                //Clear the screen and draw the framerate
                SwinGame.ClearScreen(Color.Black);
                
				//show the game menu page
				if (startGame == false && endGame == false)
				{
					if (instruction == false)
					{
						UIController.DrawMenuPage ();
					}

					//check which button did the user clicked on
					if (SwinGame.MouseClicked (MouseButton.LeftButton))
					{
						if (SwinGame.PointInRect (SwinGame.MousePosition (), 220, 295, 108, 48))
						{
							//start the game and timer
							startGame = true;
							SwinGame.StartTimer (gameTime);

						}
						else if (SwinGame.PointInRect (SwinGame.MousePosition (), 220, 356, 108, 48))
						{
							//show the instructions
							instruction = true;
						}
						else if (SwinGame.PointInRect (SwinGame.MousePosition (), 220, 416, 108, 48))
						{
							//quit the game
							break;
						}
					}
				}

				//show the instruction page
				if (instruction)
				{
					UIController.DrawInstructionPage ();

					if (SwinGame.MouseClicked (MouseButton.LeftButton))
					{
						if (SwinGame.PointInRect (SwinGame.MousePosition (), 220, 295, 108, 48))
						{
							//Go back to the game menu page
							startGame = false;
							instruction = false;
						}
					}
				}

				//if player chose to start the game, display the board and start the game
				if (startGame)
				{
					
					ticks = SwinGame.TimerTicks (gameTime);

					//Draw game board
					myBoard.DrawBoard ();

					SwinGame.DrawRectangle (Color.White, 12, 639, 524, 50);
					SwinGame.DrawText ("Score: " + myBoard.Score, Color.White, 100, 660); 
					SwinGame.DrawText ("Time: " + (ticks/=1000).ToString() + " seconds", Color.White, 350, 660); 

					//when time reaches 1 minute, show the final score page
					if (ticks >= 60)
					{
						startGame = false;
						instruction = false;
						endGame = true;
					}


					//Check for user's action
					if (SwinGame.MouseClicked (MouseButton.LeftButton))
					{
						myBoard.SelectBlockAt (SwinGame.MousePosition ());
						firstSelected = myBoard.SelectedBlock;
					}

					if (SwinGame.MouseClicked (MouseButton.RightButton))
					{
						myBoard.SelectBlockAt (SwinGame.MousePosition ());
						secondSelected = myBoard.SelectedBlock;
					}

					//Swap the selected blocks
					if (firstSelected != null && secondSelected != null)
					{
						myBoard.Swap (firstSelected, secondSelected);
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

					/* JOSEPH - check if match horizontally or vertically 
					if (myBoard.CheckMatchingHorizontal ()) {
						Sprite firstSprite = firstSelected.Sprite;
						Sprite secondSprite = secondSelected.Sprite;

						SwinGame.SpriteSetDX (firstSprite, 0);
						SwinGame.SpriteSetDY (firstSprite, 0);
						SwinGame.SpriteSetDX (secondSprite, 0);
						SwinGame.SpriteSetDY (secondSprite, 0);

						myBoard.SwapAnimation(firstSprite, 0, -20);
						myBoard.SwapAnimation(secondSprite, 0, +20);
					} 
					if (myBoard.CheckMatchingVertical ()) { 
						Sprite firstSprite = firstSelected.Sprite;
						Sprite secondSprite = secondSelected.Sprite;

						SwinGame.SpriteSetDX (firstSprite, 0);
						SwinGame.SpriteSetDY (firstSprite, 0);
						SwinGame.SpriteSetDX (secondSprite, 0);
						SwinGame.SpriteSetDY (secondSprite, 0);

						myBoard.SwapAnimation (firstSprite, -20, 0);
						myBoard.SwapAnimation (secondSprite, +20, 0);
					}
					*/
					//if (SwinGame.MouseClicked (MouseButton.RightButton) || SwinGame.MouseClicked (MouseButton.LeftButton)) {
						//myBoard.MoveSprite (firstSelected, secondSelected);
					//}
					myBoard.GenerateBlock2 ();

				}

				//if the game ends, show the final score page
				if (endGame)
				{
					UIController.DrawEndGame (myBoard);

					if (SwinGame.MouseClicked (MouseButton.LeftButton))
					{
						if (SwinGame.PointInRect (SwinGame.MousePosition (), 220, 295, 108, 48))
						{
							endGame = false;
							startGame = true;
							SwinGame.ResetTimer (gameTime);
							myBoard.Score = 0;

						}
						else if (SwinGame.PointInRect (SwinGame.MousePosition (), 220, 416, 108, 48))
						{
							break;
						}
					}
				}
                
				SwinGame.RefreshScreen(60);
				SwinGame.UpdateAllSprites ();
            }

			UIController.FreeResources ();
        }
    }
}