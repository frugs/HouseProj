using UnityEngine;

namespace Assets.Scripts.LevelSelect {
    public class SelectLevelButtonBehaviour : MonoBehaviour {
        [SerializeField]
        private Level _level;

        // Called from button press
        public void SelectLevel() {
            LevelSetupBehaviour.ActiveLevel = _level;
            Application.LoadLevel(Scenes.Gameplay);
        }
    }
}