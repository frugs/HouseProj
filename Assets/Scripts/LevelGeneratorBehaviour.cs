using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class LevelGeneratorBehaviour : MonoBehaviour {
        private static readonly IDictionary<Section, float> SectionWeights = new Dictionary<Section, float> {
            {Section.Ground, 4f},
            {Section.SmallGap, 1f},
            {Section.LargeGap, 2f},
            {Section.FloatingBlock, 7f}
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

        [SerializeField]
        private FishFactoryBehaviour _fishFactory;

        // TODO: This doesn't end up being the actual section count
        [SerializeField]
        private int _sectionCount = 100;

        [SerializeField]
        private float _gapSize = 2.5f;

        private LevelGenerator _levelGenerator;
        
        public void Awake() {
            _levelGenerator = new LevelGenerator(_platformLeftPrefab,
                                                 _platformMiddlePrefab,
                                                 _platformRightPrefab,
                                                 _floatingBlockLeftPrefab,
                                                 _floatingBlockMidPrefab,
                                                 _floatingBlockRightPrefab,
                                                 _fishFactory,
                                                 _gapSize,
                                                 transform.position);
        }

        public void Start() {
            var sections = _sectionGenerator.GenerateSections(_sectionCount);
            _levelGenerator.GenerateLevel(sections);
        }
    }
}