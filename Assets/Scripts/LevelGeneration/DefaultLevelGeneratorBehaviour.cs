using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    [RequireComponent(typeof (FishFactoryBehaviour), typeof (FloatingBlockFactoryBehaviour))]
    public class DefaultLevelGeneratorBehaviour : MonoBehaviour {
        private static readonly IDictionary<Section, float> SectionWeights = new Dictionary<Section, float> {
            {Section.PlatformMid, 3.5f},
            {Section.SmallGap, 0.25f},
            {Section.NormalGap, 2f},
            {Section.LargeGap, 0.25f},
            {Section.FloatingBlockMid, 8f}
        };

        private readonly SectionGenerator _sectionGenerator = new SectionGenerator(SectionWeights);

        [SerializeField]
        private GameObject _platformLeftPrefab;

        [SerializeField]
        private GameObject _platformMiddlePrefab;

        [SerializeField]
        private GameObject _platformRightPrefab;

        // TODO: This doesn't end up being the actual number of generated segments
        [SerializeField]
        private readonly int _sectionCount = 100;

        private ILevelGenerator _levelGenerator;

        public void Awake() {
//            _levelGenerator = new LevelGenerator(transform.position,
//                                                 null,
//                                                 GetComponent<FloatingBlockFactoryBehaviour>(),
//                                                 _platformLeftPrefab,
//                                                 _platformMiddlePrefab,
//                                                 _platformRightPrefab,
//                                                 3.2f,
//                                                 4.3f,
//                                                 5.7f,
//                                                 false);
        }

        public void Start() {
            var sections = new IndoorLevelSectionSanitizer().SanitiseSections(_sectionGenerator.GenerateSections(_sectionCount));
            _levelGenerator.GenerateLevel(sections);
        }
    }
}