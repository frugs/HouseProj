﻿using UnityEngine;
using Assets.Scripts.Score;
using System.Collections.Generic;
using Assets.Scripts.Character;

namespace Assets.Scripts.Score {
    public class ScoreBehaviour : MonoBehaviour {
		private HighScoreHandler highScoreHandler = new HighScoreHandler ();
		[SerializeField]
		private EndlessRunCharacter player;

		private bool wasDead = false;

        public int Score { get; set; }
		public float SurvivedTime { get; set; }

		public void Start() {
			highScoreHandler.Load ();
			wasDead = false;
		}

		public void Update() {
			this.SurvivedTime = Time.timeSinceLevelLoad;
			if (player.isDead () && !wasDead) {
				wasDead = true;
			}
		}

		public List<Score> getHighScores ()
		{
			return highScoreHandler.getScores ();
		}

		public void submitScore(string name) {
			highScoreHandler.submitScore (name, Score, SurvivedTime);
		}

		public bool HasNewHighScore() {
			List<Score> currentScores = getHighScores ();
			return currentScores.Count < 5 || currentScores [4].score < Score;
		}
    }
}