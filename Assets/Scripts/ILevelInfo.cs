using System.Collections.Generic;
using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts {
    public interface ILevelInfo {
        Sprite Background { get; }
        AudioClip Bgm { get; }

        IDictionary<Section, float> SectionWeights { get; }
        ISectionSanitiser SectionSanitiser { get; }

        FishFactoryBehaviour FishFactory { get; }
        ObstacleFactoryBehaviour ObstacleFactory { get; }
        FloatingBlockFactoryBehaviour FloatingBlockFactory { get; }
        
        GameObject PlatformLeftPrefab { get; }
        GameObject PlatformMiddlePrefab { get; }
        GameObject PlatformRightPrefab { get; }
        
        float SmallGapSize { get; }
        float NormalGapSize { get; }
        float LargeGapSize { get; }
        
        bool IsGroundLevel { get; }
    }
}