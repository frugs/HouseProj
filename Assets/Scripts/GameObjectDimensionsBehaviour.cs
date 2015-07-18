using UnityEngine;

namespace Assets.Scripts {
    public class GameObjectDimensionsBehaviour : MonoBehaviour {
        [SerializeField]
        private Vector2 _overrideSize = Vector2.zero;

        private Sprite _sprite;

        public void Awake() {
            _sprite = GetComponent<SpriteRenderer>().sprite;
        }

        public Vector2 Size {
            get {
                if (_overrideSize != Vector2.zero) {
                    return _overrideSize;
                } else {
                    Vector2 size = _sprite.bounds.max - _sprite.bounds.min;
                    return new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
                }
            }
        }
    }
}