using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ScreenMate.Model
{
	public class Mate
	{
		public Point Position { get; set; } = new Point(500, 300);
		public bool IsRam { get; set; } = false;
		public bool IsProcessor { get; set; } = false;
		public bool IsIdle { get; set; } = false;
		public int SpriteWidth { get; set; } = 32;
		public int SpriteHeight { get; set; } = 35;
		public int NumOfSprites { get; set; } = 4;
		public int NumOfRows { get; set; } = 5;
		public List<List<Image>> Sprites { get; set; } = new List<List<Image>>();
		public int CurrentSpriteCol { get; set; } = 0;
		public int CurrentSpriteRow { get; set; } = 0;

	}
}
