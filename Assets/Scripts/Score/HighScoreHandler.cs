using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

namespace Assets.Scripts.Score
{
	public class HighScoreHandler
	{
		private static String SCORE_FILE_NAME = "/highScores.dat";

		private BinaryFormatter formatter = new BinaryFormatter();

		private List<Score> scores = new List<Score>();

		public void Load ()
		{
			if (File.Exists (Application.persistentDataPath + SCORE_FILE_NAME)) {
				FileStream file = File.Open (Application.persistentDataPath + SCORE_FILE_NAME, FileMode.Open);
				scores = (List<Score>)formatter.Deserialize (file);
				file.Close ();
			}
		}

		public void submitScore (string name, int score, float survivedTime)
		{
			scores.Add(new Score(name, score, survivedTime));
			Save ();
		}

		private void Save ()
		{
			FileStream file = File.Create (Application.persistentDataPath + SCORE_FILE_NAME);
			List<Score> toSave = new List<Score> (5);
			scores.Sort();
			if (scores.Count > 5) {
				toSave.AddRange (scores.GetRange (0, 5));
			} else {
				toSave.AddRange(scores);
			}

			formatter.Serialize (file, toSave);
			file.Close ();
		}

		public List<Score> getScores() {
			Load ();
			return scores;
		}
	}
}
