using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (AudioSource))]
    public class JukeboxBehaviour : MonoBehaviour {
        public static JukeboxBehaviour Instance;

        public void Awake() {
            if (Instance == null) {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        public void StartMusic() {
            var audioSource = GetComponent<AudioSource>();
            if (!audioSource.isPlaying) {
                audioSource.Play();                
            }
        }

        public void StopMusic() {
            GetComponent<AudioSource>().Stop();
        }
    }
}