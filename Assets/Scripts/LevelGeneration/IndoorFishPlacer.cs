using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public class IndoorFishPlacer : IFishPlacer {
        public void PlaceFish(Vector2 origin,
                              FishFactoryBehaviour fishFactory,
                              IList<KeyValuePair<Section, Vector2>> levelSchematics) {
            PlaceFishOnPlatforms(origin, fishFactory, levelSchematics);

            var progress = Vector2.zero;
            for (var i = 3; i < levelSchematics.Count; i++) {
                var seg1 = levelSchematics[i - 3];
                var seg2 = levelSchematics[i - 2];

                if (seg1.Key.IsGap()) {
                    float gapSize;
                    int fishCount;

                    if (seg1.Key.IsGap() && seg2.Key.IsGap()) {
                        gapSize = seg1.Value.x + seg2.Value.x;
                        fishCount = 5;
                        i++;
                    } else {
                        gapSize = seg1.Value.x;
                        fishCount = seg1.Key == Section.LargeGap
                                ? 5
                                : seg1.Key == Section.NormalGap
                                        ? 3 : 1;
                    }

                    var parabolaWidth = gapSize * 1.2f;
                    fishFactory.CreateFishParabola(origin + progress + new Vector2((gapSize - parabolaWidth) / 2, 0), parabolaWidth, 3.3f, fishCount);
                    progress += new Vector2(gapSize, 0f);
                } else {
                    progress += new Vector2(seg1.Value.x, 0f);
                }
            }
        }

        private static void PlaceFishOnPlatforms(Vector2 origin, FishFactoryBehaviour fishFactory, IList<KeyValuePair<Section, Vector2>> levelSchematics) {
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