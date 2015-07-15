using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public class GroundedLevelGenerator : ILevelGenerator {
        // TODO: Again, probably don't do this here...
        private const int NumGrounds = 100;
        private const float SmallGapSize = 2f;
        private const float NormalGapSize = 4f;
        private const float LargeGapSize = 7f;

        private readonly Vector2 _origin;
        private readonly GameObject _groundPrefab;
        private readonly ObstacleFactoryBehaviour _obstacleFactory;
        private readonly FloatingBlockFactoryBehaviour _floatingBlockFactory;

        public GroundedLevelGenerator(Vector2 origin,
                                      GameObject groundPrefab,
                                      ObstacleFactoryBehaviour obstacleFactory,
                                      FloatingBlockFactoryBehaviour floatingBlockFactory) {
            _origin = origin;
            _groundPrefab = groundPrefab;
            _obstacleFactory = obstacleFactory;
            _floatingBlockFactory = floatingBlockFactory;
        }

        public void GenerateLevel(IEnumerable<Section> sections) {
            var groundStart = _origin - new Vector2(Camera.main.orthographicSize * Camera.main.aspect, 0f);
            var groundProgress = 0f;
            for (var i = 0; i < NumGrounds; i++) {
                var ground = (GameObject) Object.Instantiate(_groundPrefab, groundStart + new Vector2(groundProgress, 0f), Quaternion.identity);
                groundProgress += ground.GetComponent<BoxCollider2D>().size.x;
            }

            var progress = 0f;
            var previousSection = Section.LargeGap;
            foreach (var section in sections) {
                if (section.IsGap()) {
                    progress += GetGapSize(previousSection.IsObstacle() ? Section.LargeGap : section);
                } else if (section.IsObstacle()) {
                    if (previousSection == Section.FloatingBlock) {
                        progress += GetGapSize(Section.NormalGap);
                    }

                    var position = _origin + new Vector2(progress, 0f);
                    var obstacle = section == Section.SmallObstacle
                            ? _obstacleFactory.CreateSmallObstacle(position)
                            : _obstacleFactory.CreateLargeObstacle(position);

                    progress += obstacle.GetComponent<GameObjectDimensionsBehaviour>().Size.x;
                } else if (section == Section.FloatingBlock) {
                    if (previousSection.IsObstacle()) {
                        progress += GetGapSize(Section.NormalGap);
                    }

                    var position = _origin + new Vector2(progress, 0.5f);
                    var block = _floatingBlockFactory.CreateMiddle(position);
                    progress += block.GetComponent<GameObjectDimensionsBehaviour>().Size.x;
                }
                previousSection = section;
            }
        }

        private static float GetGapSize(Section section) {
            switch (section) {
                case Section.SmallGap:
                    return SmallGapSize;
                case Section.NormalGap:
                    return NormalGapSize;
                case Section.LargeGap:
                    return LargeGapSize;
                default:
                    return 0;
            }
        }
    }
}