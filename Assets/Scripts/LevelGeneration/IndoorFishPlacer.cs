using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public class IndoorFishPlacer : IFishPlacer {
        public void PlaceFish(Vector2 origin,
                              FishFactoryBehaviour fishFactory,
                              IList<KeyValuePair<Section, Vector2>> levelSchematics) {
            var progress = 0f;
            foreach (var section in levelSchematics) {
                if (section.Key.IsPlatform()) {
                    fishFactory.CreateFishStretch(origin + new Vector2(progress, 0f), section.Value.x);
                }
                progress += section.Value.x;
            }
        }
    }
}