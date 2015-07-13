using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts {
    public class LevelGeneratorBehaviour : MonoBehaviour {
        private enum Section {
            Ground,
            SmallGap,
            LargeGap
        }

        // TODO: This is pretty hacky and questionable
        private const float PlatformWidth = 2f;
        private const float LargeGapMultiplier = 1.75f;
        private const float FishParabolaHeight =2f;


        private static readonly IDictionary<Section, float> SectionWeights = new Dictionary<Section, float> {
            {Section.Ground, 4f},
            {Section.SmallGap, 1f},
            {Section.LargeGap, 2f}
        };

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
        private readonly int _sectionCount = 100;

        [SerializeField]
        private readonly float _gapSize = 2.5f;

        public void Start() {
            var previousSection = Section.SmallGap;
            var progress = 0f;
            var sections = GenerateSections(_sectionCount);
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

        private static IEnumerable<Section> GenerateSections(int sectionCount) {
            var cumulativeSectionWeights = new OrderedDictionary();
            var cumulativeWeight = 0f;
            foreach (var pair in SectionWeights) {
                cumulativeWeight += pair.Value;
                cumulativeSectionWeights.Add(pair.Key, cumulativeWeight);
            }

            var sections = new List<Section> {Section.Ground};
            for (var i = 0; i < sectionCount; i++) {
                foreach (var entry in cumulativeSectionWeights) {
                    var dictionaryEntry = (DictionaryEntry) entry;
                    if (Random.Range(0f, cumulativeWeight) <= (float) dictionaryEntry.Value) {
                        var section = (Section) dictionaryEntry.Key;
                        sections.Add(section);

                        if (section == Section.SmallGap || section == Section.LargeGap) {
                            sections.Add(Section.Ground);
                        }
                        break;
                    }
                }
            }
            sections.Add(Section.SmallGap);

            return sections;
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
            float halfWidthSquared = (parabolaWidth / 2) * (parabolaWidth / 2);
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