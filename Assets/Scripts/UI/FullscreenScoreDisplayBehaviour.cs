using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Score;

namespace Assets.Scripts.UI {
    [RequireComponent(typeof (Text))]
    public class FullscreenScoreDisplayBehaviour : MonoBehaviour {
        [SerializeField]
        private ScoreBehaviour _scoreBehaviour;

        private Text _text;

        public void Awake() {
            _text = GetComponent<Text>();
        }

        public void OnEnable() {
			//_text.text = _scoreBehaviour.Score + " fish!\nYou Survived " + string.Format("{0:#,###}", _scoreBehaviour.SurvivedTime) + " Seconds";
			_text.text = _scoreBehaviour.Score + " fish!\nYou Survived " + string.Format("{0:#,###}", _scoreBehaviour.SurvivedTime) + " Seconds\n\n";
			_text.text += "High Scores\nName\t\tScore\t\tTime\n";
			Debug.Log (_scoreBehaviour.getHighScores ().Count);
			foreach (var score in _scoreBehaviour.getHighScores()) {
				_text.text += score.name + "\t\t" + score.score + "\t\t" + score.time + "\n";
			}
        }
    }
}