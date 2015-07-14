using UnityEngine;
using UnityEngine.UI;
using System;

namespace Assets.Scripts.UI {
    [RequireComponent(typeof(Text))]
    public class ScoreDisplayBehaviour : MonoBehaviour {
        [SerializeField]
        private ScoreBehaviour _score;

        private Text _text;

        public void Awake() {
            _text = GetComponent<Text>();
        }

        public void LateUpdate() {
			_text.text = "Fish: " + _score.Score.ToString() + "\nTime: " + String.Format("{0:F}", Time.timeSinceLevelLoad);
        }
    }
}