using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    
    [RequireComponent(typeof(Text))]
    public class FullscreenScoreDisplayBehaviour : MonoBehaviour {
        [SerializeField]
        private ScoreBehaviour _scoreBehaviour;
        
        private Text _text;

        public void Awake() {
            _text = GetComponent<Text>();
        }
        
        public void Update() {
            _text.text = _scoreBehaviour.Score + " fish!";
        }
    }
}