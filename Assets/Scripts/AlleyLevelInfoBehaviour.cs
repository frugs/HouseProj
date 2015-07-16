using System.Collections.Generic;
using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof(FishFactoryBehaviour), typeof(ObstacleFactoryBehaviour), typeof(FloatingBlockFactoryBehaviour))]
    public class AlleyLevelInfoBehaviour : MonoBehaviour, ILevelInfo {
        [SerializeField]
        private GameObject _platformLeftPrefab;

        [SerializeField]
        private GameObject _platformMiddlePrefab;

        [SerializeField]
        private GameObject _platformRightPrefab;

        [SerializeField]
        private Sprite _background;

        [SerializeField]
        private AudioClip _bgm;

        public Sprite Background {
            get { return _background; }
        }

        public AudioClip Bgm {
            get { return _bgm; }
        }

        public IDictionary<Section, float> SectionWeights {
            get {
                return new Dictionary<Section, float> {
                    {Section.SmallObstacle, 1f},
                    {Section.LargeObstacle, 1f},
                    {Section.SmallGap, 2f},
                    {Section.NormalGap, 3f},
                    {Section.LargeGap, 1f},
                    {Section.FloatingBlockMid, 10f}
                };
            }
        }

        public ISectionSanitiser SectionSanitiser {
            get { return new AlleyLevelSectionSanitiser(); }
        }

        public IFishPlacer FishPlacer {
            get { return new AlleyFishPlacer(); }
        }

        public FishFactoryBehaviour FishFactory {
            get { return GetComponent<FishFactoryBehaviour>(); }
        }

        public ObstacleFactoryBehaviour ObstacleFactory {
            get { return GetComponent<ObstacleFactoryBehaviour>(); }
        }

        public FloatingBlockFactoryBehaviour FloatingBlockFactory {
            get { return GetComponent<FloatingBlockFactoryBehaviour>(); }
        }

        public GameObject PlatformLeftPrefab {
            get { return _platformLeftPrefab; }
        }

        public GameObject PlatformMiddlePrefab {
            get { return _platformMiddlePrefab; }
        }

        public GameObject PlatformRightPrefab {
            get { return _platformRightPrefab; }
        }

        public float SmallGapSize {
            get { return 3f; }
        }

        public float NormalGapSize {
            get { return 4f; }
        }

        public float LargeGapSize {
            get { return 7f; }
        }

        public bool IsGroundLevel {
            get { return true; }
        }
    }
}