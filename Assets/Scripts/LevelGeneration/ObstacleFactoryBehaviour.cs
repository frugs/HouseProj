using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public class ObstacleFactoryBehaviour : MonoBehaviour {
        
        [SerializeField]
        private GameObject _smallObstaclePrefab;

        [SerializeField]
        private GameObject _largeObstaclePrefab;

        [SerializeField]
        private FullscreenScoreDisplayBehaviour _fullscreenScoreDisplayBehaviour;

        [SerializeField]
        private ScoreDisplayBehaviour _scoreDisplayBehaviour;

        private LevelRestarter _levelRestarter;

        public void Awake() {
            _levelRestarter = new LevelRestarter(this, _scoreDisplayBehaviour, _fullscreenScoreDisplayBehaviour);
        }

        public GameObject CreateSmallObstacle(Vector2 position) {
            var obstacle = (GameObject) Instantiate(_smallObstaclePrefab,
                                                    position,
                                                    Quaternion.identity);

            var restartLevelBehaviour = obstacle.GetComponent<KillZoneBehaviour>();
            restartLevelBehaviour.LevelRestarter = _levelRestarter;
            return obstacle;
        }

        public GameObject CreateLargeObstacle(Vector2 position) {
            var obstacle = (GameObject) Instantiate(_largeObstaclePrefab,
                                                    position,
                                                    Quaternion.identity);

            var restartLevelBehaviour = obstacle.GetComponent<KillZoneBehaviour>();
            restartLevelBehaviour.LevelRestarter = _levelRestarter;
            return obstacle;
        }
    }
}