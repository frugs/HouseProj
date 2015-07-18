using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts {
    public class EndLevelFactoryBehaviour : MonoBehaviour {
        [SerializeField]
        private MainCameraBehaviour _mainCamera;

        [SerializeField]
        private ScoreDisplayBehaviour _scoreDisplayBehaviour;

        [SerializeField]
        private FullscreenScoreDisplayBehaviour _fullscreenScoreDisplayBehaviour;

        [SerializeField]
        private GameObject _endLevelPrefab;

        private LevelRestarter _levelRestarter;

        public void Awake() {
            _levelRestarter = new LevelRestarter(this, _scoreDisplayBehaviour, _fullscreenScoreDisplayBehaviour);
        }

        public GameObject CreateEndLevel(Vector2 position) {
            var obj = (GameObject) Instantiate(_endLevelPrefab, position, Quaternion.identity);
            obj.GetComponent<EndLevelBehaviour>().LevelRestarter = _levelRestarter;
            obj.GetComponent<EndLevelBehaviour>().MainCamera = _mainCamera;
            return obj;
        }
    }
}