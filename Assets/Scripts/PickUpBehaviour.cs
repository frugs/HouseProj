using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (Collider2D))]
    public abstract class PickUpBehaviour : MonoBehaviour {

        public void OnTriggerEnter2D(Collider2D other) {
            if (other.GetComponent<PickUpperBehaviour>() != null) {
                PickedUp();
                Destroy(gameObject);
            }
        }

        protected abstract void PickedUp();
    }
}