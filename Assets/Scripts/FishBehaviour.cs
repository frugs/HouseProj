namespace Assets.Scripts {
    public class FishBehaviour : PickUpBehaviour {
        public ScoreBehaviour ScoreBehaviour;

        protected override void PickedUp() {
            ScoreBehaviour.Score++;
        }
    }
}