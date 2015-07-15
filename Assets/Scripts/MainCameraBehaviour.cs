using UnityEngine;

namespace Assets.Scripts {
    public class MainCameraBehaviour : MonoBehaviour {
        [SerializeField]
        private Transform _followTransform;

        [SerializeField]
        private float _lookAheadFactor = 0.5f;

        public void Update() {
            float lookAhead = Camera.main.orthographicSize * Camera.main.aspect * _lookAheadFactor;
            transform.position = lookAhead > 1.3f
                    ? new Vector3(_followTransform.position.x + lookAhead,
                                  transform.position.y,
                                  transform.position.z)
                    : new Vector3(_followTransform.position.x, _followTransform.position.y, transform.position.z);
        }
    }
}