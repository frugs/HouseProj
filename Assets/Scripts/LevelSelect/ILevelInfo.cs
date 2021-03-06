﻿using System.Collections.Generic;
using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts.LevelSelect {
    public interface ILevelInfo {
        Sprite Background { get; }
        AudioClip Bgm { get; }

        IDictionary<Section, float> SectionWeights { get; }
        ISectionSanitiser SectionSanitiser { get; }

        IFishPlacer FishPlacer { get; }

        FishFactoryBehaviour FishFactory { get; }
        ObstacleFactoryBehaviour ObstacleFactory { get; }
        FloatingBlockFactoryBehaviour FloatingBlockFactory { get; }
        EndLevelFactoryBehaviour EndLevelFactory { get; }
        
        GameObject PlatformLeftPrefab { get; }
        GameObject PlatformMiddlePrefab { get; }
        GameObject PlatformRightPrefab { get; }
        
        float SmallGapSize { get; }
        float NormalGapSize { get; }
        float LargeGapSize { get; }
        
        bool IsGroundLevel { get; }
    }
}