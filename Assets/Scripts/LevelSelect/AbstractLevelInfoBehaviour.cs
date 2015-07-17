using System.Collections.Generic;
using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts.LevelSelect {
    [RequireComponent(typeof (FishFactoryBehaviour), typeof (ObstacleFactoryBehaviour), typeof (FloatingBlockFactoryBehaviour))]
    public abstract class AbstractLevelInfoBehaviour : MonoBehaviour, ILevelInfo {
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

        public abstract IDictionary<Section, float> SectionWeights { get; }
        public abstract ISectionSanitiser SectionSanitiser { get; }
        public abstract IFishPlacer FishPlacer { get; }

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

        public abstract float SmallGapSize { get; }
        public abstract float NormalGapSize { get; }
        public abstract float LargeGapSize { get; }
        public abstract bool IsGroundLevel { get; }
    }
}