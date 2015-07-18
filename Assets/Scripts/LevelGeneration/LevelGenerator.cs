using System;
using System.Collections.Generic;
using Assets.Scripts.LevelSelect;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.LevelGeneration {
    public class LevelGenerator {
        private const int NumGrounds = 100;
        private const float CrouchHeight = 0.7f;

        private readonly Vector2 _origin;
        private readonly ILevelInfo _levelInfo;
        private readonly ObstacleFactoryBehaviour _obstacleFactory;
        private readonly FloatingBlockFactoryBehaviour _floatingBlockFactory;
        private readonly GameObject _platformLeftPrefab;
        private readonly GameObject _platformMiddlePrefab;
        private readonly GameObject _platformRightPrefab;

        public LevelGenerator(Vector2 origin, ILevelInfo levelInfo) {
            _levelInfo = levelInfo;
            _origin = origin;
            _obstacleFactory = _levelInfo.ObstacleFactory;
            _floatingBlockFactory = _levelInfo.FloatingBlockFactory;
            _platformLeftPrefab = _levelInfo.PlatformLeftPrefab;
            _platformMiddlePrefab = _levelInfo.PlatformMiddlePrefab;
            _platformRightPrefab = _levelInfo.PlatformRightPrefab;
        }

        public IList<KeyValuePair<Section, Vector2>> GenerateLevel(IEnumerable<Section> sections) {
            if (_levelInfo.IsGroundLevel) {
                var groundStart = _origin - new Vector2(Camera.main.orthographicSize * Camera.main.aspect, 0f);
                var groundProgress = 0f;
                for (var i = 0; i < NumGrounds; i++) {
                    var ground = (GameObject) Object.Instantiate(_platformMiddlePrefab, groundStart + new Vector2(groundProgress, 0f), Quaternion.identity);
                    groundProgress += ground.GetComponent<BoxCollider2D>().size.x;
                }
            }

            IList<KeyValuePair<Section, Vector2>> levelSchematics = new List<KeyValuePair<Section, Vector2>>();
            var progress = 0f;
            foreach (var section in sections) {
                if (section.IsGap()) {
                    var gapSize = GetGapSize(section);
                    progress += gapSize;
                    levelSchematics.Add(new KeyValuePair<Section, Vector2>(section, new Vector2(gapSize, 0f)));
                } else {
                    var position = section.IsFloatingBlock()
                            ? _origin + new Vector2(progress, CrouchHeight)
                            : _origin + new Vector2(progress, 0f);

                    var objDict = new Dictionary<Section, Func<GameObject>> {
                        {Section.SmallObstacle, () => _obstacleFactory.CreateSmallObstacle(position)},
                        {Section.LargeObstacle, () => _obstacleFactory.CreateLargeObstacle(position)},
                        {Section.PlatformLeft, () => (GameObject) Object.Instantiate(_platformLeftPrefab, position, Quaternion.identity)},
                        {Section.PlatformMid, () => (GameObject) Object.Instantiate(_platformMiddlePrefab, position, Quaternion.identity)},
                        {Section.PlatformRight, () => (GameObject) Object.Instantiate(_platformRightPrefab, position, Quaternion.identity)},
                        {Section.FloatingBlockLeft, () => _floatingBlockFactory.CreateLeft(position)},
                        {Section.FloatingBlockMid, () => _floatingBlockFactory.CreateMiddle(position)},
                        {Section.FloatingBlockRight, () => _floatingBlockFactory.CreateRight(position)},
                        {Section.EndLevel, () => _levelInfo.EndLevelFactory.CreateEndLevel(position)}
                    };
                    var sectionSize = objDict[section]().GetComponent<GameObjectDimensionsBehaviour>().Size;
                    progress += sectionSize.x;
                    levelSchematics.Add(new KeyValuePair<Section, Vector2>(section, sectionSize));
                }
            }
            return levelSchematics;
        }

        private float GetGapSize(Section section) {
            switch (section) {
                case Section.SmallGap:
                    return _levelInfo.SmallGapSize;
                case Section.NormalGap:
                    return _levelInfo.NormalGapSize;
                case Section.LargeGap:
                    return _levelInfo.LargeGapSize;
                default:
                    return 0;
            }
        }
    }
}