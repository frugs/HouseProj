using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts {
    public class LevelGenerator {
        // TODO: This is pretty hacky and questionable
        private const float PlatformWidth = 2f;
        private const float LargeGapMultiplier = 1.75f;
        private const float FishParabolaHeight = 2f;
        private const float CrouchHeight = 0.75f;

        private readonly GameObject _platformLeftPrefab;
        private readonly GameObject _platformMiddlePrefab;
        private readonly GameObject _platformRightPrefab;
        private readonly GameObject _floatingBlockLeftPrefab;
        private readonly GameObject _floatingBlockMiddlePrefab;
        private readonly GameObject _floatingBlockRightPrefab;
        private readonly FishFactoryBehaviour _fishFactory;

        private readonly float _gapSize;

        private readonly Vector2 _origin;

        private float _progress;

        public LevelGenerator(GameObject platformLeftPrefab,
                              GameObject platformMiddlePrefab,
                              GameObject platformRightPrefab,
                              GameObject floatingBlockLeftPrefab,
                              GameObject floatingBlockMiddlePrefab,
                              GameObject floatingBlockRightPrefab,
                              FishFactoryBehaviour fishFactory,
                              float gapSize,
                              Vector2 origin) {
            _platformLeftPrefab = platformLeftPrefab;
            _platformMiddlePrefab = platformMiddlePrefab;
            _platformRightPrefab = platformRightPrefab;
            _fishFactory = fishFactory;
            _gapSize = gapSize;
            _origin = origin;
            _floatingBlockLeftPrefab = floatingBlockLeftPrefab;
            _floatingBlockMiddlePrefab = floatingBlockMiddlePrefab;
            _floatingBlockRightPrefab = floatingBlockRightPrefab;
        }

        public void GenerateLevel(IEnumerable<Section> sections) {
            var previousSection = Section.SmallGap;
            foreach (var section in sections) {
                if (section.IsGap()) {
                    if (previousSection == Section.FloatingBlock) {
                        CreateRightFloatingBlock();
                    } else {
                        CreateFishOnPlatform();
                    }
                    CreateRightPlatform();
                    _progress += PlatformWidth;

                    var gapSize = section == Section.SmallGap ? _gapSize : _gapSize * LargeGapMultiplier;
                    CreateFishParabola(gapSize, FishParabolaHeight);
                    _progress += gapSize;
                } else if (section == Section.Ground) {
                    if (previousSection.IsGap()) {
                        CreateLeftPlatform();
                        CreateFishOnPlatform();
                        _progress += PlatformWidth;
                    } else {
                        if (previousSection == Section.FloatingBlock) {
                            CreateRightFloatingBlock();
                        } else {
                            CreateFishOnPlatform();
                        }
                        CreateMiddlePlatform();
                        _progress += PlatformWidth;
                    }
                } else if (section == Section.FloatingBlock) {
                    if (previousSection.IsGap()) {
                        CreateLeftPlatform(); 
                        _progress += PlatformWidth;

                        CreateLeftFloatingBlock();
                        
                        CreateMiddlePlatform();
                        _progress += PlatformWidth;
                    } else {
                        if (previousSection == Section.FloatingBlock) {
                            CreateMiddleFloatingBlock();
                        } else {
                            CreateMiddlePlatform();
                            _progress += PlatformWidth;

                            CreateLeftFloatingBlock();
                        }
                        CreateMiddlePlatform();
                        _progress += PlatformWidth;
                    }
                }

                previousSection = section;
            }
        }

        private void CreateLeftFloatingBlock() {
            Object.Instantiate(_floatingBlockLeftPrefab,
                               new Vector2(_origin.x + _progress - (PlatformWidth / 2), _origin.y + CrouchHeight),
                               Quaternion.identity);
        }

        private void CreateMiddleFloatingBlock() {
            Object.Instantiate(_floatingBlockMiddlePrefab,
                               new Vector2(_origin.x + _progress - (PlatformWidth / 2), _origin.y + CrouchHeight),
                               Quaternion.identity);
        }

        private void CreateRightFloatingBlock() {
            Object.Instantiate(_floatingBlockRightPrefab,
                               new Vector2(_origin.x + _progress - (PlatformWidth / 2), _origin.y + CrouchHeight),
                               Quaternion.identity);
        }

        private void CreateMiddlePlatform() {
            Object.Instantiate(_platformMiddlePrefab,
                               new Vector2(_origin.x + _progress, _origin.y),
                               Quaternion.identity);
        }

        private void CreateRightPlatform() {
            Object.Instantiate(_platformRightPrefab,
                               new Vector2(_origin.x + _progress, _origin.y),
                               Quaternion.identity);
        }

        private void CreateLeftPlatform() {
            Object.Instantiate(_platformLeftPrefab,
                               new Vector2(_origin.x + _progress, _origin.y),
                               Quaternion.identity);
        }

        private void CreateFishOnPlatform() {
            var fish1Position = new Vector2(_origin.x + _progress + (PlatformWidth / 3),
                                            _origin.y + 1f);
            _fishFactory.CreateFish(fish1Position);

            var fish2Position = new Vector2(_origin.x + _progress + (PlatformWidth * 2 / 3),
                                            _origin.y + 1f);
            _fishFactory.CreateFish(fish2Position);
        }

        private void CreateFishParabola(float parabolaWidth, float parabolaHeight) {
            var halfWidthSquared = (parabolaWidth / 2) * (parabolaWidth / 2);
            Func<float, float> parabola = x => x * (parabolaWidth - x) * (parabolaHeight / halfWidthSquared);
            var origin = _origin.x + _progress;

            var fish1Position = new Vector2(origin + (parabolaWidth / 7), parabola(parabolaWidth / 7));
            _fishFactory.CreateFish(fish1Position);

            var fish2Position = new Vector2(origin + (parabolaWidth / 2), parabola(parabolaWidth / 2));
            _fishFactory.CreateFish(fish2Position);

            var fish3Position = new Vector2(origin + (parabolaWidth * 6 / 7), parabola(parabolaWidth * 6 / 7));
            _fishFactory.CreateFish(fish3Position);
        }
    }
}