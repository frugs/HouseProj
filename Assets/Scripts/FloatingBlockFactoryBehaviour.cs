using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts {
    public class FloatingBlockFactoryBehaviour : MonoBehaviour {
        [SerializeField]
        private GameObject _floatingBlockLeftPrefab;

        [SerializeField]
        private GameObject _floatingBlockMiddlePrefab;

        [SerializeField]
        private GameObject _floatingBlockRightPrefab;

        [SerializeField]
        private FullscreenScoreDisplayBehaviour _fullscreenScoreDisplayBehaviour;

        [SerializeField]
        private ScoreDisplayBehaviour _scoreDisplayBehaviour;
        
        public void CreateLeft(Vector2 position) {
            var obj = (GameObject) Instantiate(_floatingBlockLeftPrefab,
                                                      position,
                                                      Quaternion.identity);
            var restartLevelBehaviour = obj.GetComponent<RestartLevelBehaviour>();
            restartLevelBehaviour.FullscreenScoreDisplayBehaviour = _fullscreenScoreDisplayBehaviour;
            restartLevelBehaviour.ScoreDisplayBehaviour = _scoreDisplayBehaviour;
        }

        public void CreateMiddle(Vector2 position) {
            var obj = (GameObject) Instantiate(_floatingBlockMiddlePrefab,
                                                      position,
                                                      Quaternion.identity);
            var restartLevelBehaviour = obj.GetComponent<RestartLevelBehaviour>();
            restartLevelBehaviour.FullscreenScoreDisplayBehaviour = _fullscreenScoreDisplayBehaviour;
            restartLevelBehaviour.ScoreDisplayBehaviour = _scoreDisplayBehaviour;
        }

        public void CreateRight(Vector2 position) {
            var obj = (GameObject) Instantiate(_floatingBlockRightPrefab,
                                                      position,
                                                      Quaternion.identity);
            var restartLevelBehaviour = obj.GetComponent<RestartLevelBehaviour>();
            restartLevelBehaviour.FullscreenScoreDisplayBehaviour = _fullscreenScoreDisplayBehaviour;
            restartLevelBehaviour.ScoreDisplayBehaviour = _scoreDisplayBehaviour;
        }
    }
}