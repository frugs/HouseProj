using UnityEngine;
using UnityEngine.UI;
using System;

namespace Assets.Scripts {
    
    [RequireComponent(typeof(Text))]
    public class FullscreenScoreDisplayBehaviour : MonoBehaviour {
        [SerializeField]
        private ScoreBehaviour _scoreBehaviour;
        
        private Text _text;

        public void Awake() {
            _text = GetComponent<Text>();
        }

        public void OnEnable() {
            _text.text = _scoreBehaviour.Score + " fish!\nYou Survived " + String.Format("{0:#,###}", Time.timeSinceLevelLoad) + " Seconds";            
        }
    }
}