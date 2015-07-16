using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public class IndoorFishPlacer : IFishPlacer {
        public void LayFish(Vector2 origin, FishFactoryBehaviour fishFactory, IList<KeyValuePair<Section, Vector2>> levelSchematics) {
            float progress = 0f;
            foreach (var section in levelSchematics) {
                if (section.Key.IsPlatform()) {
                    fishFactory.CreateFishLine(origin + new Vector2(progress, 1f), section.Value.x, Mathf.FloorToInt(section.Value.x / 1.5f));
                }
                progress += section.Value.x;
            }
        }
    }
}