using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    [RequireComponent(typeof (ObstacleFactoryBehaviour), typeof (FloatingBlockFactoryBehaviour))]
    public class GroundedLevelGeneratorBehaviour : MonoBehaviour {
        [SerializeField]
        private GameObject _groundPrefab;

        private GroundedLevelGenerator _levelGenerator;

        public void Awake() {
            _levelGenerator = new GroundedLevelGenerator(transform.position,
                                                         _groundPrefab,
                                                         GetComponent<ObstacleFactoryBehaviour>(),
                                                         GetComponent<FloatingBlockFactoryBehaviour>());
        }

        public void Start() {
            var sectionGenerator = new SectionGenerator(new Dictionary<Section, float> {
                {Section.SmallObstacle, 1f},
                {Section.LargeObstacle, 1f},
                {Section.SmallGap, 2f},
                {Section.NormalGap, 3f},
                {Section.LargeGap, 1f},
                {Section.FloatingBlock, 10f}
            });

            var sections = new List<Section> {
                Section.LargeGap,
                Section.LargeGap,
                Section.LargeGap,
                Section.LargeGap
            };
            sections.AddRange(sectionGenerator.GenerateSections(90));
            _levelGenerator.GenerateLevel(sections);
        }
    }
}