using System;
using Assets.Scripts.Score;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public class FishFactoryBehaviour : MonoBehaviour {
        private const float FishYOffset = 1f;
        private const float FishSpacing = 1.5f;

        [SerializeField]
        private GameObject _fishPrefab;

        [SerializeField]
        private ScoreBehaviour _scoreBehaviour;

        private bool _rotate = true;

        public void CreateFish(Vector2 position) {
            var fish = (GameObject) Instantiate(_fishPrefab,
                                                position,
                                                _rotate ? Quaternion.identity : Quaternion.AngleAxis(90, Vector3.forward));
            fish.GetComponent<FishBehaviour>().ScoreBehaviour = _scoreBehaviour;
            _rotate = !_rotate;
        }

        public void CreateFishParabola(Vector2 origin, float parabolaWidth, float parabolaHeight, int fishCount) {
            var halfWidthSquared = (parabolaWidth / 2) * (parabolaWidth / 2);
            Func<float, float> parabola = x => x * (parabolaWidth - x) * (parabolaHeight / halfWidthSquared);

            for (var i = 1; i <= fishCount; i++) {
                var fishXOffset = parabolaWidth * i / (fishCount + 1);
                var fishPosition = new Vector2(origin.x + fishXOffset, origin.y + parabola(fishXOffset));
                CreateFish(fishPosition);
            }
        }

        public void CreateFishStretch(Vector2 origin, float stretchWidth) {
            var fishCount = Mathf.FloorToInt(stretchWidth / FishSpacing);
            for (var i = 1; i <= fishCount; i++) {
                var fishXOffset = stretchWidth * i / (fishCount + 1);
                var fishPosition = new Vector2(origin.x + fishXOffset, origin.y + FishYOffset);
                CreateFish(fishPosition);
            }
        }
    }
}