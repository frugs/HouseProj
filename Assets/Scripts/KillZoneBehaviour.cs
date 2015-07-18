using Assets.Scripts.Character;
using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (Collider2D))]
    public class KillZoneBehaviour : MonoBehaviour {
        public LevelRestarter LevelRestarter { private get; set; }

        public void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player") {
                other.GetComponent<EndlessRunCharacter>().Kill();
                LevelRestarter.RestartLevel();
            }
        }
    }
}