using UnityEngine;

namespace Assets.Scripts {
    public class GameObjectDimensionsBehaviour : MonoBehaviour {
        private Sprite _sprite;

        public void Awake() {
            _sprite = GetComponent<SpriteRenderer>().sprite;
        }

        public Vector2 Size {
            get {
                Vector2 size = _sprite.bounds.max - _sprite.bounds.min;
                return new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
            }
        }
    }
}