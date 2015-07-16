using System.Linq;
using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts {
    public class LevelSetupBehaviour : MonoBehaviour {
        public static Level ActiveLevel { get; set; }

        [SerializeField]
        private JukeboxBehaviour _jukebox;

        [SerializeField]
        private SpriteRenderer _backgroundRenderer;

        public void Awake() {
            ActiveLevel = Level.Alley;
        }

        public void Start() {
            ILevelInfo levelInfo = ActiveLevel == Level.Indoor
                    ? (ILevelInfo) GetComponentsInChildren<IndoorLevelInfoBehaviour>(true).First()
                    : GetComponentsInChildren<AlleyLevelInfoBehaviour>(true).First();

            if (_jukebox != null) {
                _jukebox.GetComponent<AudioSource>().clip = levelInfo.Bgm;
            }

            _backgroundRenderer.sprite = levelInfo.Background;

            var levelGenerator = new LevelGenerator(Vector2.zero, levelInfo);
            var sectionGenerator = new SectionGenerator(levelInfo.SectionWeights);
            var sections = levelInfo.SectionSanitiser.SanitiseSections(sectionGenerator.GenerateSections(120));
            levelGenerator.GenerateLevel(sections);
        }
    }
}