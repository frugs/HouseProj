using System.Linq;
using UnityEngine;

namespace Assets.Scripts.AlternativeInput {
    public static class TouchInput {
        public static bool GetJump() {
            return Input.touches.Where(touch => touch.phase == TouchPhase.Began && touch.position.x < Camera.main.pixelRect.center.x).Any();
        }

        public static bool GetCrouch() {
            return Input.touches.Where(touch => touch.position.x > Camera.main.pixelRect.center.x).Any();
        }
    }
}