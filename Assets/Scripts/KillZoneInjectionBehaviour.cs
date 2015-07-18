using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (KillZoneBehaviour))]
    public class KillZoneInjectionBehaviour : MonoBehaviour {
        [SerializeField]
        private ScoreDisplayBehaviour _scoreDisplayBehaviour;

        [SerializeField]
        private FullscreenScoreDisplayBehaviour _fullscreenScoreDisplayBehaviour;

        public void Awake() {
            GetComponent<KillZoneBehaviour>().LevelRestarter = new LevelRestarter(this,
                                                                                  _scoreDisplayBehaviour,
                                                                                  _fullscreenScoreDisplayBehaviour);
        }
    }
}