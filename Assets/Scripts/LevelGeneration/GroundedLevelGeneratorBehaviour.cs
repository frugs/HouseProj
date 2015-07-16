using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    [RequireComponent(typeof (ObstacleFactoryBehaviour), typeof (FloatingBlockFactoryBehaviour))]
    public class GroundedLevelGeneratorBehaviour : MonoBehaviour {
        [SerializeField]
        private GameObject _groundPrefab;

        private ILevelGenerator _levelGenerator;

        public void Awake() {
//            _levelGenerator = new LevelGenerator(transform.position,
//                                                 GetComponent<ObstacleFactoryBehaviour>(),
//                                                 GetComponent<FloatingBlockFactoryBehaviour>(),
//                                                 null,
//                                                 _groundPrefab,
//                                                 null,
//                                                 2f,
//                                                 4f,
//                                                 7f,
//                                                 true);
        }

        public void Start() {
            var sectionGenerator = new SectionGenerator(new Dictionary<Section, float> {
                {Section.SmallObstacle, 1f},
                {Section.LargeObstacle, 1f},
                {Section.SmallGap, 2f},
                {Section.NormalGap, 3f},
                {Section.LargeGap, 1f},
                {Section.FloatingBlockMid, 10f}
            });

            var sections = new AlleyLevelSectionSanitizer().SanitiseSections(sectionGenerator.GenerateSections(150));
            _levelGenerator.GenerateLevel(sections);
        }
    }
}