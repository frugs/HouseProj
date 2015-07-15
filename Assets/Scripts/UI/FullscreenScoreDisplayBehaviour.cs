using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Score;
using System.Collections;

namespace Assets.Scripts.UI {
    public class FullscreenScoreDisplayBehaviour : MonoBehaviour {
        [SerializeField]
        private ScoreBehaviour _scoreBehaviour;

        private Text _text;

		[SerializeField]
		private GameObject _nameEntryMenu;

		private string playerName = "";

		private bool _done = false;

        public void Awake() {
            _text = GetComponentsInChildren<Text>(true).First();
        }

        public void OnEnable() {
			_done = false;
			StartCoroutine (ShowScores());
        }

		public void SetPlayerName(string name) {
			playerName = name;
		}

		private IEnumerator ShowScores() {
			if (_scoreBehaviour.HasNewHighScore ()) {
				_nameEntryMenu.SetActive(true);
				while (playerName == "") {
					yield return null;
				}
				_nameEntryMenu.SetActive(false);
				_scoreBehaviour.submitScore(playerName);
			}
			_done = true;
			_text.text = _scoreBehaviour.Score + " fish!\nYou Survived " + string.Format("{0:#,###}", _scoreBehaviour.SurvivedTime) + " Seconds\n\n";
			_text.text += "High Scores\nName\t\tScore\t\tTime\n";
			foreach (var score in _scoreBehaviour.getHighScores()) {
				_text.text += score.name + "\t\t" + score.score + "\t\t" + score.time + "\n";
			}
		}

		public bool IsDone() {
			return _done;
		}
    }
}