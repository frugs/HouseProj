using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.LevelGeneration {
    public class LevelGenerator : ILevelGenerator {
        private const int NumGrounds = 100;
        private const float CrouchHeight = 0.7f;

        private readonly Vector2 _origin;
        private readonly ObstacleFactoryBehaviour _obstacleFactory;
        private readonly FloatingBlockFactoryBehaviour _floatingBlockFactory;
        private readonly GameObject _platformLeftPrefab;
        private readonly GameObject _platformMiddlePrefab;
        private readonly GameObject _platformRightPrefab;

        private readonly float _smallGapSize;
        private readonly float _normalGapSize;
        private readonly float _largeGapSize;

        private readonly bool _isGroundLevel;

        public LevelGenerator(Vector2 origin, ILevelInfo levelInfo) {
            _origin = origin;
            _obstacleFactory = levelInfo.ObstacleFactory;
            _floatingBlockFactory = levelInfo.FloatingBlockFactory;
            _platformLeftPrefab = levelInfo.PlatformLeftPrefab;
            _platformMiddlePrefab = levelInfo.PlatformMiddlePrefab;
            _platformRightPrefab = levelInfo.PlatformRightPrefab;
            _smallGapSize = levelInfo.SmallGapSize;
            _normalGapSize = levelInfo.NormalGapSize;
            _largeGapSize = levelInfo.LargeGapSize;
            _isGroundLevel = levelInfo.IsGroundLevel;
        }

        public void GenerateLevel(IEnumerable<Section> sections) {
            if (_isGroundLevel) {
                var groundStart = _origin - new Vector2(Camera.main.orthographicSize * Camera.main.aspect, 0f);
                var groundProgress = 0f;
                for (var i = 0; i < NumGrounds; i++) {
                    var ground = (GameObject) Object.Instantiate(_platformMiddlePrefab, groundStart + new Vector2(groundProgress, 0f), Quaternion.identity);
                    groundProgress += ground.GetComponent<BoxCollider2D>().size.x;
                }
            }

            var progress = 0f;
            foreach (var section in sections) {
                if (section.IsGap()) {
                    progress += GetGapSize(section);
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
                        {Section.FloatingBlockRight, () => _floatingBlockFactory.CreateRight(position)}
                    };
                    progress += objDict[section]().GetComponent<GameObjectDimensionsBehaviour>().Size.x;
                }
            }
        }

        private float GetGapSize(Section section) {
            switch (section) {
                case Section.SmallGap:
                    return _smallGapSize;
                case Section.NormalGap:
                    return _normalGapSize;
                case Section.LargeGap:
                    return _largeGapSize;
                default:
                    return 0;
            }
        }
    }
}