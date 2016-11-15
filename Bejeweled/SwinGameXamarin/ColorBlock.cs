using System;
using SwinGameSDK;

namespace MyGame
{
	public abstract class ColorBlock
	{
		private Color _color;
		private Sprite _sprite;
		private float _x, _y;
		private int _width, _height;
		private bool _selected;

		public ColorBlock (float x, float y, Color color)
		{
			_x = x;
			_y = y;
			_color = color;
			_height = 60;
			_width = 50;
		}

		public bool Selected
		{
			get{ return _selected; }
			set{ _selected = value; }
		}

		public float X
		{
			get { return _x; }
			set { _x = value; }
		}

		public float Y
		{
			get { return _y; }
			set { _y = value; }
		}

		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}

		public int Height
		{
			get { return _height; }
			set { _height = value; }
		}
			
		public bool IsAt (Point2D pt)
		{
			if (SwinGame.PointInRect (pt, _x, _y, _width, _height))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
			
		public void DrawOutline ()
		{
			SwinGame.DrawRectangle (Color.White, X-2, Y-2, _width+4, _height+4);
		}

		public abstract void Draw();

		public Sprite Sprite {
			get { return _sprite; }
			set { _sprite = value; }
		}
	}
}

