using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
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

        public GameObject CreateLeft(Vector2 position) {
            return CreateFloatingBlock(Section.FloatingBlockLeft, position);
        }

        public GameObject CreateMiddle(Vector2 position) {
            return CreateFloatingBlock(Section.FloatingBlockMid, position);
        }

        public GameObject CreateRight(Vector2 position) {
            return CreateFloatingBlock(Section.FloatingBlockRight, position);
        }

        private GameObject CreateFloatingBlock(Section section, Vector2 position) {
            var prefab = section == Section.FloatingBlockLeft
                    ? _floatingBlockLeftPrefab
                    : section == Section.FloatingBlockRight
                            ? _floatingBlockRightPrefab
                            : _floatingBlockMiddlePrefab;

            var obj = (GameObject) Instantiate(prefab,
                                               position,
                                               Quaternion.identity);
            var restartLevelBehaviour = obj.GetComponent<RestartLevelBehaviour>();
            restartLevelBehaviour.FullscreenScoreDisplayBehaviour = _fullscreenScoreDisplayBehaviour;
            restartLevelBehaviour.ScoreDisplayBehaviour = _scoreDisplayBehaviour;
            return obj;
        }
    }
}