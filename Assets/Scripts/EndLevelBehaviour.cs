using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (Collider2D))]
    public class EndLevelBehaviour : MonoBehaviour {
        public MainCameraBehaviour MainCamera;

        public LevelRestarter LevelRestarter { private get; set; }

        public void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Player") {
                MainCamera.LockCamera();
                LevelSetupBehaviour.ActiveLevel += 1;
                LevelRestarter.RestartLevel();
            }
        }
    }
}