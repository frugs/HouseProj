using UnityEngine;

namespace Assets.Scripts {
    public class FishFactoryBehaviour : MonoBehaviour {
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
    }
}