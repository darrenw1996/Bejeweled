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
			Timer gameTime = SwinGame.CreateTimer();	//game time
			uint ticks;   //timer ticks
			Bitmap background = SwinGame.LoadBitmap("blocks.png");  //background image for start page 

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
						SwinGame.DrawBitmapOnScreen (background, 0 ,42);
						SwinGame.DrawBitmapOnScreen (background, 483, 42);
						SwinGame.DrawTextLines ("Swinburne University of Technology Sarawak", Color.White, Color.Black, "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 12, 300, 100);
						SwinGame.DrawTextLines ("Welcome to", Color.CornflowerBlue, Color.Black, "maven_pro_regular", 20, FontAlignment.AlignCenter, 125, 130, 300, 100);
						SwinGame.DrawTextLines ("Bejeweled!", Color.CornflowerBlue, Color.Black, "cour", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);

						//Play button
						SwinGame.FillRectangle (Color.White, 220, 295, 108, 48);
						SwinGame.FillRectangle (Color.CornflowerBlue, 224, 299, 100, 40);
						SwinGame.DrawText ("Play", Color.Black, 258, 315);

						//Instruction button
						SwinGame.FillRectangle (Color.White, 220, 356, 108, 48);
						SwinGame.FillRectangle (Color.CornflowerBlue, 224, 360, 100, 40);
						SwinGame.DrawText ("Instruction", Color.Black, 229, 376);

						//Quit button
						SwinGame.FillRectangle (Color.White, 220, 416, 108, 48);
						SwinGame.FillRectangle (Color.CornflowerBlue, 224, 420, 100, 40);
						SwinGame.DrawText ("Quit", Color.Black, 258, 436);

						SwinGame.DrawTextLines ("COS20007 Object Oriented Programming", Color.White, Color.Black, "maven_pro_regular", 12, FontAlignment.AlignLeft, 12, 680, 300, 100);
						SwinGame.DrawTextLines ("Created by: Sharon Lo", Color.White, Color.Black, "maven_pro_regular", 12, FontAlignment.AlignLeft, 400, 680, 300, 100);

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
					SwinGame.DrawTextLines ("Instructions", Color.White, Color.Black, "cour", 20, FontAlignment.AlignLeft, 10, 70, 300, 100);
					SwinGame.DrawText ("1. Left click on your mouse to select the first block, then,", Color.White, 10, 115);
					SwinGame.DrawText ("   right click on the second block to swap.", Color.White, 10, 130);
					SwinGame.DrawText ("2. Blocks can only be swapped if their are located next to each ", Color.White, 10, 155);
					SwinGame.DrawText ("   other, above or below of each other.", Color.White, 10, 170);
					SwinGame.DrawText ("3. Blocks will not be swapped if there is no match.", Color.White, 10, 190);
					SwinGame.DrawText ("4. When there are 3 or more matching blocks, they will be deleted", Color.White, 10, 215);
					SwinGame.DrawText ("   and new blocks will be generated.", Color.White, 10, 230);
					SwinGame.DrawText ("5. Score as many as you can in 1 minute before the time runs out!", Color.White, 10, 250);

					//Back button
					SwinGame.FillRectangle (Color.White, 220, 295, 108, 48);
					SwinGame.FillRectangle (Color.CornflowerBlue, 224, 299, 100, 40);
					SwinGame.DrawText ("Back", Color.Black, 258, 315);

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

					//Draw the box for score and timer
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


					myBoard.GenerateBlock ();

				}

				//if the game ends, show the final score page
				if (endGame)
				{
					if (myBoard.Score <= 300)
					{
						SwinGame.DrawTextLines ("Good!", Color.White, Color.Black, "maven_pro_regular", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);
					}
					else if (myBoard.Score <= 500)
					{
						SwinGame.DrawTextLines ("Very Good!", Color.White, Color.Black, "maven_pro_regular", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);
					}
					else if (myBoard.Score <= 750)
					{
						SwinGame.DrawTextLines ("Excellent!", Color.White, Color.Black, "maven_pro_regular", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);
					}
					else
					{
						SwinGame.DrawTextLines ("Outstanding!", Color.White, Color.Black, "maven_pro_regular", 50, FontAlignment.AlignCenter, 125, 160, 300, 100);
					}
						
					SwinGame.DrawTextLines ("You scored:" + myBoard.Score + "!", Color.White, Color.Black, "maven_pro_regular", 15, FontAlignment.AlignCenter, 125, 230, 300, 100);


					SwinGame.FillRectangle (Color.White, 220, 295, 108, 48);
					SwinGame.FillRectangle (Color.CornflowerBlue, 224, 299, 100, 40);
					SwinGame.DrawText ("Play again", Color.Black, 235, 315);

					SwinGame.FillRectangle (Color.White, 220, 416, 108, 48);
					SwinGame.FillRectangle (Color.CornflowerBlue, 224, 420, 100, 40);
					SwinGame.DrawText ("Quit", Color.Black, 258, 436);

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


        }
    }
}