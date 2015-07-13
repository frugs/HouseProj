using UnityEngine;

namespace Assets.Scripts {
    public class FishBehaviour : PickUpBehaviour {
        [SerializeField]
        private ScoreBehaviour _scoreBehaviour;

        protected override void PickedUp() {
            _scoreBehaviour.Score++;
        }
    }
}