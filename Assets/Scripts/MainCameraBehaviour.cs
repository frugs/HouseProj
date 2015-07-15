using UnityEngine;

namespace Assets.Scripts {
    public class MainCameraBehaviour : MonoBehaviour {
        [SerializeField]
        private Transform _followTransform;

        [SerializeField]
        private readonly float _lookAheadFactor = 0.25f;

        public void Update() {
            float lookAhead = Camera.main.orthographicSize * Camera.main.aspect * 0.25f;
            transform.position = lookAhead > 0.25f
                    ? new Vector3(_followTransform.position.x + lookAhead,
                                  _followTransform.position.y,
                                  transform.position.z)
                    : new Vector3(_followTransform.position.x, _followTransform.position.y, transform.position.z);
        }
    }
}