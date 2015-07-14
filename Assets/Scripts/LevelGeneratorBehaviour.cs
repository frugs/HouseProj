using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class LevelGeneratorBehaviour : MonoBehaviour {
        // TODO: This is pretty hacky and questionable
        private const float PlatformWidth = 2f;
        private const float LargeGapMultiplier = 1.75f;
        private const float FishParabolaHeight = 2f;

        private static readonly IDictionary<Section, float> SectionWeights = new Dictionary<Section, float> {
            {Section.Ground, 4f},
            {Section.SmallGap, 1f},
            {Section.LargeGap, 2f}
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
        private FishFactoryBehaviour _fishFactory;

        // TODO: This doesn't end up being the actual section count
        [SerializeField]
        private int _sectionCount = 100;

        [SerializeField]
        private float _gapSize = 2.5f;

        public void Start() {
            var previousSection = Section.SmallGap;
            var progress = 0f;
            var sections = _sectionGenerator.GenerateSections(_sectionCount);
            foreach (var section in sections) {
                // TODO: This is pretty ugly, revisit
                switch (previousSection) {
                    case Section.SmallGap:
                    case Section.LargeGap:
                        switch (section) {
                            case Section.SmallGap:
                            case Section.LargeGap:
                                progress += section == Section.SmallGap ? _gapSize : _gapSize * LargeGapMultiplier;
                                break;

                            case Section.Ground:
                                Instantiate(_platformLeftPrefab,
                                            new Vector2(transform.position.x + progress, transform.position.y),
                                            Quaternion.identity);
                                CreateFishOnPlatform(progress);

                                progress += PlatformWidth;
                                break;
                        }
                        break;

                    case Section.Ground:
                        switch (section) {
                            case Section.SmallGap:
                            case Section.LargeGap: {
                                Instantiate(_platformRightPrefab,
                                            new Vector2(transform.position.x + progress, transform.position.y),
                                            Quaternion.identity);
                                CreateFishOnPlatform(progress);
                                progress += PlatformWidth;

                                var gapSize = section == Section.SmallGap ? _gapSize : _gapSize * LargeGapMultiplier;
                                CreateFishParabola(progress, gapSize, FishParabolaHeight);
                                progress += gapSize;
                                break;
                            }

                            case Section.Ground: {
                                Instantiate(_platformMiddlePrefab,
                                            new Vector2(transform.position.x + progress, transform.position.y),
                                            Quaternion.identity);
                                CreateFishOnPlatform(progress);

                                progress += PlatformWidth;
                                break;
                            }
                        }
                        break;
                }

                previousSection = section;
            }
        }

        private void CreateFishOnPlatform(float progress) {
            var fish1Position = new Vector2(transform.position.x + progress + (PlatformWidth / 3),
                                            transform.position.y + 1f);
            _fishFactory.CreateFish(fish1Position);

            var fish2Position = new Vector2(transform.position.x + progress + (PlatformWidth * 2 / 3),
                                            transform.position.y + 1f);
            _fishFactory.CreateFish(fish2Position);
        }

        private void CreateFishParabola(float progress, float parabolaWidth, float parabolaHeight) {
            var halfWidthSquared = (parabolaWidth / 2) * (parabolaWidth / 2);
            Func<float, float> parabola = x => x * (parabolaWidth - x) * (parabolaHeight / halfWidthSquared);
            var origin = transform.position.x + progress;

            var fish1Position = new Vector2(origin + (parabolaWidth / 7), parabola(parabolaWidth / 7));
            _fishFactory.CreateFish(fish1Position);

            var fish2Position = new Vector2(origin + (parabolaWidth / 2), parabola(parabolaWidth / 2));
            _fishFactory.CreateFish(fish2Position);

            var fish3Position = new Vector2(origin + (parabolaWidth * 6 / 7), parabola(parabolaWidth * 6 / 7));
            _fishFactory.CreateFish(fish3Position);
        }
    }
}