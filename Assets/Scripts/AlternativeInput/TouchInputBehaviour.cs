using UnityEngine;

namespace Assets.Scripts.AlternativeInput {
    public static class TouchInputBehaviour {
        public static bool GetJump() {
            return Input.touchCount > 0 && Input.GetTouch(0).position.x < Camera.main.pixelRect.center.x;
        }

        public static bool GetCrouch() {
            return Input.touchCount > 0 && Input.GetTouch(0).position.x > Camera.main.pixelRect.center.x;
        }
    }
}