using System;

namespace Assets.Scripts.Score
{
	[System.Serializable]
	public class Score : IComparable
	{
		public String name { get; set;}
		public int score { get; set;}
		public float time { get; set;}

		public Score () {}

		public Score (string name, int score, float time)
		{
			this.name = name;
			this.score = score;
			this.time = time;
		}

		public int CompareTo (object obj)
		{
			Score score = (Score) obj;

			int diff = score.score.CompareTo (this.score);
			if (diff == 0) diff = score.time.CompareTo (this.time);
			return diff;
		}
	}
}

