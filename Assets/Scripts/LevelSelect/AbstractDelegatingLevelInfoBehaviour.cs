using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts.LevelSelect {
    public abstract class AbstractDelegatingLevelInfoBehaviour<T> : MonoBehaviour, ILevelInfo where T : MonoBehaviour, ILevelInfo {
        protected ILevelInfo Delegate {
            get { return GetComponentsInParent<T>(true).First(); }
        }

        public Sprite Background {
            get { return Delegate.Background; }
        }

        public AudioClip Bgm {
            get { return Delegate.Bgm; }
        }

        public virtual IDictionary<Section, float> SectionWeights {
            get { return Delegate.SectionWeights; }
        }

        public virtual ISectionSanitiser SectionSanitiser {
            get { return Delegate.SectionSanitiser; }
        }

        public IFishPlacer FishPlacer {
            get { return Delegate.FishPlacer; }
        }

        public FishFactoryBehaviour FishFactory {
            get { return Delegate.FishFactory; }
        }

        public ObstacleFactoryBehaviour ObstacleFactory {
            get { return Delegate.ObstacleFactory; }
        }

        public FloatingBlockFactoryBehaviour FloatingBlockFactory {
            get { return Delegate.FloatingBlockFactory; }
        }

        public EndLevelFactoryBehaviour EndLevelFactory {
            get { return Delegate.EndLevelFactory; }
        }

        public GameObject PlatformLeftPrefab {
            get { return Delegate.PlatformLeftPrefab; }
        }

        public GameObject PlatformMiddlePrefab {
            get { return Delegate.PlatformMiddlePrefab; }
        }

        public GameObject PlatformRightPrefab {
            get { return Delegate.PlatformRightPrefab; }
        }

        public virtual float SmallGapSize {
            get { return Delegate.SmallGapSize; }
        }

        public virtual float NormalGapSize {
            get { return Delegate.NormalGapSize; }
        }

        public virtual float LargeGapSize {
            get { return Delegate.LargeGapSize; }
        }
        
        public bool IsGroundLevel {
            get { return Delegate.IsGroundLevel; }
        }
    }
}