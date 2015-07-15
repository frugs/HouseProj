using UnityEngine;

namespace Assets.Scripts {
    public class PauseMenuBehaviour : MonoBehaviour {
        [SerializeField]
        private GameObject _menu;

        private bool _paused;

        public void Update() {
            if (Input.GetButtonDown("Cancel")) {
                _paused = !_paused;
                UpdatePaused();
            }
        }

        //Called from menu button
        public void Resume() {
            _paused = false;
            UpdatePaused();
        }

        // Called from menu button
        public void Quit() {
            Application.Quit();
        }

        private void UpdatePaused() {
            Time.timeScale = _paused ? 0f : 1f;
            _menu.SetActive(_paused);
        }
    }
}