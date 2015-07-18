using System.Collections;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts {
    public class LevelRestarter {
        private readonly MonoBehaviour _coroutineRunner;
        private readonly ScoreDisplayBehaviour _scoreDisplayBehaviour;
        private readonly FullscreenScoreDisplayBehaviour _fullscreenScoreDisplayBehaviour;

        public LevelRestarter(MonoBehaviour coroutineRunner,
                              ScoreDisplayBehaviour scoreDisplayBehaviour,
                              FullscreenScoreDisplayBehaviour fullscreenScoreDisplayBehaviour) {
            _coroutineRunner = coroutineRunner;
            _scoreDisplayBehaviour = scoreDisplayBehaviour;
            _fullscreenScoreDisplayBehaviour = fullscreenScoreDisplayBehaviour;
        }

        public void RestartLevel() {
            StartCoroutine(ShowHighScoreCoroutine());
        }

        private void StartCoroutine(IEnumerator coroutine) {
            _coroutineRunner.StartCoroutine(coroutine);
        }

        private IEnumerator ShowHighScoreCoroutine() {
            yield return new WaitForSeconds(0.5f);
            _scoreDisplayBehaviour.gameObject.SetActive(false);
            _fullscreenScoreDisplayBehaviour.gameObject.SetActive(true);
            while (!_fullscreenScoreDisplayBehaviour.IsDone()) {
                yield return null;
            }

            StartCoroutine(RestartLevelOnTimerCoroutine());
            StartCoroutine(RestartLevelOnUserActionCoroutine());
        }

        private static IEnumerator RestartLevelOnTimerCoroutine() {
            yield return new WaitForSeconds(4);
            Application.LoadLevel(Application.loadedLevelName);
        }

        private IEnumerator RestartLevelOnUserActionCoroutine() {
            while (!Input.GetButtonDown("Jump") && Input.touchCount == 0) {
                yield return null;
            }
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}