using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (AudioSource))]
    public class JukeboxBehaviour : MonoBehaviour {
        private static bool _musicStarted;

        public void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        public void Start() {
            if (!_musicStarted) {
                GetComponent<AudioSource>().Play();
                _musicStarted = true;
            } else {
                Destroy(gameObject);
            }
        }
    }
}