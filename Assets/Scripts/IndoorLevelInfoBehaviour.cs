using System.Collections.Generic;
using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts {
    [RequireComponent(typeof (FishFactoryBehaviour), typeof (ObstacleFactoryBehaviour), typeof (FloatingBlockFactoryBehaviour))]
    public class IndoorLevelInfoBehaviour : MonoBehaviour, ILevelInfo {
        [SerializeField]
        private AudioClip _bgm;

        [SerializeField]
        private Sprite _background;

        [SerializeField]
        private GameObject _platformLeftPrefab;

        [SerializeField]
        private GameObject _platformMiddlePrefab;

        [SerializeField]
        private GameObject _platformRightPrefab;

        public Sprite Background {
            get { return _background; }
        }

        public AudioClip Bgm {
            get { return _bgm; }
        }

        public IDictionary<Section, float> SectionWeights {
            get {
                return new Dictionary<Section, float> {
                    {Section.PlatformMid, 3.5f},
                    {Section.SmallGap, 0.25f},
                    {Section.NormalGap, 2f},
                    {Section.LargeGap, 0.25f},
                    {Section.FloatingBlockMid, 8f}
                };
            }
        }

        public ISectionSanitiser SectionSanitiser {
            get { return new IndoorLevelSectionSanitizer(); }
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
            get { return 3.2f; }
        }

        public float NormalGapSize {
            get { return 4.3f; }
        }

        public float LargeGapSize {
            get { return 5.7f; }
        }

        public bool IsGroundLevel {
            get { return false; }
        }
    }
}