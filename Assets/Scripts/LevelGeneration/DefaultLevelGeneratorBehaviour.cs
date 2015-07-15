using System.Collections.Generic;
using Assets.Scripts.Score;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    [RequireComponent(typeof (FishFactoryBehaviour), typeof (FloatingBlockFactoryBehaviour))]
    public class DefaultLevelGeneratorBehaviour : MonoBehaviour {
        private static readonly IDictionary<Section, float> SectionWeights = new Dictionary<Section, float> {
            {Section.Ground, 3.5f},
            {Section.SmallGap, 0.25f},
            {Section.NormalGap, 2f},
            {Section.LargeGap, 0.25f},
            {Section.FloatingBlock, 8f}
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

        // TODO: This doesn't end up being the actual number of generated segments
        [SerializeField]
        private int _sectionCount = 100;

        private ILevelGenerator _levelGenerator;

        public void Awake() {
            _levelGenerator = new DefaultLevelGenerator(_platformLeftPrefab,
                                                 _platformMiddlePrefab,
                                                 _platformRightPrefab,
                                                 GetComponent<FloatingBlockFactoryBehaviour>(),
                                                 GetComponent<FishFactoryBehaviour>(),
                                                 transform.position);
        }

        public void Start() {
            var sections = new List<Section>() {
                Section.Ground,
                Section.Ground,
                Section.Ground,
                Section.Ground,
                Section.NormalGap,
                Section.Ground,
                Section.Ground,
                Section.FloatingBlock,
                Section.FloatingBlock,
                Section.FloatingBlock,
                Section.Ground,
                Section.SmallGap,
                Section.Ground
            };

            sections.AddRange(_sectionGenerator.GenerateSections(_sectionCount));
            _levelGenerator.GenerateLevel(sections);
        }
    }
}