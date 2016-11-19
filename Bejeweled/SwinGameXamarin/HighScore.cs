using System;
using System.Collections;
using System.Collections.Generic;
using SwinGameSDK;
using System.IO;
using System.Diagnostics;

namespace MyGame
{
	static class HighScore
	{
		private const int namesize = 3;
		private static Dictionary<string, Font> _Fonts = new Dictionary<string, Font>();





		private struct Score:IComparable{
			public string Name;
			public int score;


			public int CompareTo(object obj){

				if (obj is Score)
				{
					Score other = (Score)obj;

					return other.score - this.score;
				}
				else
				{
					return 0;
				}

			}
		}

		private static List<Score> _Scores = new List<Score>();

		private static void LoadScores()
		{

			StreamReader input = default(StreamReader);
			input = new StreamReader (SwinGame.PathToResource ("highscores.txt"));

			int numScores = 0;
			numScores = Convert.ToInt32 (input.ReadLine ());

			_Scores.Clear ();

			int i = 0;

			for (i = 1; i <= numScores; i++)
			{
				Score s = default(Score);
				string line = null;

				line = input.ReadLine ();

				s.Name = line.Substring (0, namesize);
				s.score = Convert.ToInt32 (line.Substring (namesize));
				_Scores.Add (s);

			}
			input.Close ();

		}

		private static void SaveScores()
		{
			StreamWriter output = default (StreamWriter);
			output = new StreamWriter (SwinGame.PathToResource ("highscores.txt"));

			output.WriteLine (_Scores.Count);

			foreach (Score s in _Scores)
			{
				output.WriteLine (s.Name + s.score);

			}

			output.Close ();


		}

		public static void DrawHighScores()
		{	

			const int fromleft =325;
			const int top = 180;
			const int gap = 30;

			//UIController.DrawRanking ();


			if (_Scores.Count == 0)
			{
				LoadScores ();
			}




			for (int i = 0; i <= _Scores.Count - 1; i++)
			{
				Score s = default(Score);

				s = _Scores [i];

				if (i < 9)
				{
					SwinGame.DrawText (" " + (i + 1) + ":  " + s.Name + "  " + s.score, Color.White, fromleft, top + i * gap);
				}
				else
				{
					SwinGame.DrawText (i + 1 + ":  " + s.Name + "  " + s.score, Color.White, fromleft, top + i * gap);
				}

			}


		}

		private static void NewFont(string fontName, string filename, int size)
		{
			if (!_Fonts.ContainsKey (fontName))
			{
				_Fonts.Add(fontName, SwinGame.LoadFont(SwinGame.PathToResource(filename, ResourceKind.FontResource), size));
			}
		}

		public static void ReadUserScore (int value)
		{

			NewFont ("maven_pro_regular", "maven_pro_regular.ttf", 14);

			const int fromleft = 325;
			const int inputtop = 500;

			if (_Scores.Count == 0)
			{
				LoadScores ();
			}

			//GameController.GameState = GameState.ViewingHighScore;

			if (value > _Scores [_Scores.Count - 1].score)
			{
				Score s = new Score ();
				s.score = value;



				int x = 0;
				x = fromleft + SwinGame.TextWidth (_Fonts ["maven_pro_regular"], "Name: ");

				SwinGame.StartReadingText (Color.White, namesize, _Fonts ["maven_pro_regular"], x, inputtop);

				while (SwinGame.ReadingText ())
				{
					SwinGame.ProcessEvents ();

					UIController.DrawEndGame ();
					DrawHighScores ();
					SwinGame.DrawText ("Name: ", Color.White, fromleft, inputtop); 

					SwinGame.RefreshScreen (60);

				}

				s.Name = SwinGame.TextReadAsASCII (); 

				if (s.Name.Length == 3)
				{
					s.Name = s.Name + new string (Convert.ToChar (" "), 3 - s.Name.Length);

					_Scores.RemoveAt (_Scores.Count - 1);
					_Scores.Add (s);
					_Scores.Sort ();
					SaveScores ();
					GameController.GameState = GameState.EndingRanking;



				}
				else
				{
					while (s.Name.Length != 3)
					{
						if (value > _Scores [_Scores.Count - 1].score)
						{
							s.score = value; 

							x = fromleft + SwinGame.TextWidth (_Fonts ["maven_pro_regular"], "Name: ");

							SwinGame.StartReadingText (Color.White, namesize, _Fonts ["maven_pro_regular"], x, inputtop);

							while (SwinGame.ReadingText ())
							{
								SwinGame.ProcessEvents ();

								DrawHighScores ();
								SwinGame.DrawText ("Name: ", Color.White, fromleft - 5, inputtop);
								SwinGame.DrawText ("Your name must be at least 3 characters!", Color.White, fromleft - 5, inputtop + 15);

								SwinGame.RefreshScreen (60);


							}

							s.Name = SwinGame.TextReadAsASCII ();

							if (s.Name.Length == 3)
							{
								s.Name = s.Name + new string (Convert.ToChar (" "), 3 - s.Name.Length);
								_Scores.RemoveAt (_Scores.Count - 1);
								_Scores.Add (s);
								_Scores.Sort ();

								SaveScores ();


							}

						}
					}


				}

			}
			else
			{
				DrawHighScores ();
				GameController.GameState = GameState.EndingRanking;
			}

		}



	}
}
