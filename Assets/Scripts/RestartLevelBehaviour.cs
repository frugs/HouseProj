using System.Collections;
using Assets.Scripts.Character;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (Collider2D))]
    public class
            RestartLevelBehaviour : MonoBehaviour {
        public ScoreDisplayBehaviour ScoreDisplayBehaviour;
        public FullscreenScoreDisplayBehaviour FullscreenScoreDisplayBehaviour;

        public void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                other.GetComponent<EndlessRunCharacter>().Kill();
                StartCoroutine(ShowHighScoreCoroutine());
            }
        }

        private static void RestartLevel() {
            Application.LoadLevel(Application.loadedLevelName);
        }

        private IEnumerator ShowHighScoreCoroutine() {
            yield return new WaitForSeconds(0.5f);
            ScoreDisplayBehaviour.gameObject.SetActive(false);
            FullscreenScoreDisplayBehaviour.gameObject.SetActive(true);
            while (!FullscreenScoreDisplayBehaviour.IsDone()) {
                yield return null;
            }

            StartCoroutine(RestartLevelOnTimerCoroutine());
            StartCoroutine(RestartLevelOnUserActionCoroutine());
        }

        private static IEnumerator RestartLevelOnTimerCoroutine() {
            yield return new WaitForSeconds(4);
            RestartLevel();
        }

        private IEnumerator RestartLevelOnUserActionCoroutine() {
            while (!Input.GetButtonDown("Jump") && Input.touchCount == 0) {
                yield return null;
            }
            RestartLevel();
        }
    }
}