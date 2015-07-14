using System.Collections;
using Assets.Scripts.Character;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (Collider2D))]
    public class RestartLevelBehaviour : MonoBehaviour {
        public ScoreDisplayBehaviour ScoreDisplayBehaviour;
        public FullscreenScoreDisplayBehaviour FullscreenScoreDisplayBehaviour;

        public void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                other.GetComponent<EndlessRunCharacter>().Kill();
                StartCoroutine(RestartLevel());
            }
        }

        private IEnumerator RestartLevel() {
            ScoreDisplayBehaviour.gameObject.SetActive(false);
            FullscreenScoreDisplayBehaviour.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}