﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts {
    public class LevelGenerator {
        // TODO: This is pretty hacky and questionable
        private const float PlatformWidth = 2f;
        private const float SmallGapMultiplier = 0.75f;
        private const float NormalGapSize = 4.3f;
        private const float LargeGapMultiplier = 1.33f;
        private const float FishParabolaHeight = 3.3f;
        private const float CrouchHeight = 0.75f;

        private readonly GameObject _platformLeftPrefab;
        private readonly GameObject _platformMiddlePrefab;
        private readonly GameObject _platformRightPrefab;

        private readonly FloatingBlockFactoryBehaviour _floatingBlockFactory;
        private readonly FishFactoryBehaviour _fishFactory;

        private readonly Vector2 _origin;

        private float _progress;

        public LevelGenerator(GameObject platformLeftPrefab,
                              GameObject platformMiddlePrefab,
                              GameObject platformRightPrefab,
                              FloatingBlockFactoryBehaviour floatingBlockFactory,
                              FishFactoryBehaviour fishFactory,
                              Vector2 origin) {
            _platformLeftPrefab = platformLeftPrefab;
            _platformMiddlePrefab = platformMiddlePrefab;
            _platformRightPrefab = platformRightPrefab;
            _floatingBlockFactory = floatingBlockFactory;
            _fishFactory = fishFactory;
            _origin = origin;
        }

        public void GenerateLevel(IEnumerable<Section> sections) {
            var previousSection = Section.Ground;
            foreach (var section in sections) {
                if (section.IsGap()) {
                    if (previousSection == Section.FloatingBlock) {
                        CreateRightFloatingBlock();
                    } else {
                        CreateFishOnPlatform();
                    }
                    CreateRightPlatform();
                    _progress += PlatformWidth;

                    var gapSize = GetGapSize(section);
                    var parabolaSize = gapSize * 1.2f;
                    var parabolaOrigin = new Vector2(_origin.x + _progress - ((parabolaSize - gapSize) / 2), _origin.y);
                    CreateFishParabola(parabolaOrigin,
                                       parabolaSize,
                                       FishParabolaHeight, FishCountForGap(section));
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

        private static float GetGapSize(Section section) {
            switch (section) {
                case Section.SmallGap:
                    return NormalGapSize * SmallGapMultiplier;
                case Section.NormalGap:
                    return NormalGapSize;
                case Section.LargeGap:
                    return NormalGapSize * LargeGapMultiplier;
                default:
                    return 0;
            }
        }

        private static int FishCountForGap(Section section) {
            switch (section) {
                case Section.SmallGap:
                    return 2;
                case Section.NormalGap:
                    return 3;
                case Section.LargeGap:
                    return 5;
                default:
                    return 0;
            }
        }

        private void CreateLeftFloatingBlock() {
            var position = new Vector2(_origin.x + _progress - (PlatformWidth / 2), _origin.y + CrouchHeight);
            _floatingBlockFactory.CreateLeft(position);
        }

        private void CreateMiddleFloatingBlock() {
            var position = new Vector2(_origin.x + _progress - (PlatformWidth / 2), _origin.y + CrouchHeight);
            _floatingBlockFactory.CreateMiddle(position);
        }

        private void CreateRightFloatingBlock() {
            var position = new Vector2(_origin.x + _progress - (PlatformWidth / 2), _origin.y + CrouchHeight);
            _floatingBlockFactory.CreateRight(position);
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

        private void CreateFishParabola(Vector2 origin, float parabolaWidth, float parabolaHeight, int fishCount) {
            var halfWidthSquared = (parabolaWidth / 2) * (parabolaWidth / 2);
            Func<float, float> parabola = x => x * (parabolaWidth - x) * (parabolaHeight / halfWidthSquared);

            for (var i = 1; i <= fishCount; i++) {
                var fishXOffset = parabolaWidth * i / (fishCount + 1);
                var fishPosition = new Vector2(origin.x + fishXOffset, origin.y + parabola(fishXOffset));
                _fishFactory.CreateFish(fishPosition);
            }
        }
    }
}