using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (FishFactoryBehaviour), typeof (FloatingBlockFactoryBehaviour))]
    public class LevelGeneratorBehaviour : MonoBehaviour {
        private static readonly IDictionary<Section, float> SectionWeights = new Dictionary<Section, float> {
            {Section.Ground, 4f},
            {Section.SmallGap, 0.25f},
            {Section.NormalGap, 2f},
            {Section.LargeGap, 0.25f},
            {Section.FloatingBlock, 6f}
        };

        private readonly SectionGenerator _sectionGenerator = new SectionGenerator(SectionWeights);

        [SerializeField]
        private ScoreBehaviour _scoreBehaviour;

        [SerializeField]
        private GameObject _platformLeftPrefab;

        [SerializeField]
        private GameObject _platformMiddlePrefab;

        [SerializeField]
        private GameObject _platformRightPrefab;

        [SerializeField]
        private GameObject _floatingBlockLeftPrefab;

        [SerializeField]
        private GameObject _floatingBlockMidPrefab;

        [SerializeField]
        private GameObject _floatingBlockRightPrefab;

        // TODO: This doesn't end up being the actual number of generated segments
        [SerializeField]
        private readonly int _sectionCount = 100;

        private LevelGenerator _levelGenerator;

        public void Awake() {
            _levelGenerator = new LevelGenerator(_platformLeftPrefab,
                                                 _platformMiddlePrefab,
                                                 _platformRightPrefab,
                                                 GetComponent<FloatingBlockFactoryBehaviour>(),
                                                 GetComponent<FishFactoryBehaviour>(),
                                                 transform.position);
        }

        public void Start() {
            var sections = _sectionGenerator.GenerateSections(_sectionCount);
            _levelGenerator.GenerateLevel(sections);
        }
    }
}