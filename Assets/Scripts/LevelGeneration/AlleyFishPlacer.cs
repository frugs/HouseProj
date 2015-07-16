using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    internal class AlleyFishPlacer : IFishPlacer {
        private const float SmallObstacleParabolaWidth = 6f;
        private const float LargeObstacleParabolaWidth = 7f;
        private const float DualObstacleParabolaWidth = 8.5f;

        public void PlaceFish(Vector2 origin,
                              FishFactoryBehaviour fishFactory,
                              IList<KeyValuePair<Section, Vector2>> levelSchematics) {
            PlaceFishOnGapSequences(origin, fishFactory, levelSchematics);

            var progress = Vector2.zero;
            for (var i = 4; i < levelSchematics.Count; i++) {
                var seg1 = levelSchematics[i - 4];
                var seg2 = levelSchematics[i - 3];
                var seg3 = levelSchematics[i - 2];
                var seg4 = levelSchematics[i - 1];
                var seg5 = levelSchematics[i];

                if (seg1.Key.IsGap() && seg2.Key.IsObstacle() && seg3.Key.IsGap() && seg4.Key.IsObstacle() && seg5.Key.IsGap()) {
                    // If there is a gap, obstacle, gap, obstacle, gap, place parabolas over the obstacles and join them up in between                
                    var parabola1Width = seg2.Key == Section.SmallObstacle ? SmallObstacleParabolaWidth : LargeObstacleParabolaWidth;

                    var precedingStretchWidth = seg1.Value.x - ((parabola1Width - seg2.Value.x) / 2);
                    var parabola1Origin = origin + progress + new Vector2(precedingStretchWidth, 0f);
                    fishFactory.CreateFishParabola(parabola1Origin, parabola1Width, 4f, 5);

                    var parabola2Width = seg4.Key == Section.SmallObstacle ? SmallObstacleParabolaWidth : LargeObstacleParabolaWidth;
                    var inbetweenStretchWidth = seg3.Value.x - ((parabola1Width - seg2.Value.x) / 2) - ((parabola2Width - seg4.Value.x) / 2);

                    fishFactory.CreateFishStretch(origin + progress + new Vector2(precedingStretchWidth + parabola1Width, 0f), inbetweenStretchWidth);

                    var parabola2Origin = origin + progress + new Vector2(precedingStretchWidth + parabola1Width + inbetweenStretchWidth, 0f);
                    fishFactory.CreateFishParabola(parabola2Origin, parabola2Width, 4f, 5);

                    // This hack stops the rule under this one from overlapping
                    i = i + 3;
                    progress += new Vector2(seg2.Value.x + seg3.Value.x + seg4.Value.x, 0f);
                } else if (seg1.Key.IsGap() && seg2.Key.IsObstacle() && seg3.Key.IsGap()) {
                    // Place a nice parabola over a single obstacle surrounded by gaps, and surround the parabola with stretches of fish                
                    var parabolaWidth = seg2.Key == Section.SmallObstacle ? SmallObstacleParabolaWidth : LargeObstacleParabolaWidth;
                    var precedingStretchWidth = seg1.Value.x - ((parabolaWidth - seg2.Value.x) / 2);

                    fishFactory.CreateFishStretch(origin + progress, precedingStretchWidth);

                    var parabolaOrigin = origin + progress + new Vector2(precedingStretchWidth, 0f);
                    fishFactory.CreateFishParabola(parabolaOrigin, parabolaWidth, 4f, 5);

                    var followingStretchWidth = seg3.Value.x - ((parabolaWidth - seg2.Value.x) / 2);
                    fishFactory.CreateFishStretch(origin + progress + new Vector2(precedingStretchWidth + parabolaWidth, 0f), followingStretchWidth);
                } else if (!seg1.Key.IsObstacle() && seg2.Key.IsGap() && seg3.Key.IsFloatingBlock()) {
                    // Fill a gap preceding a floating block with fish as long as it is not preceded by an obstacle

                    fishFactory.CreateFishStretch(origin + progress + new Vector2(seg1.Value.x, 0f), seg2.Value.x);
                } else if (!seg1.Key.IsGap()
                        && seg2.Key.IsObstacle()
                        && seg3.Key.IsGap()
                        && seg3.Key != Section.SmallGap
                        && seg4.Key.IsFloatingBlock()) {
                    // If there is a gap which is medium or large preceded by an obstacle, and followed by a floating block, fill up the
                    // latter half of the gap with fish

                    var seg2HalfWidth = seg2.Value.x / 2;
                    fishFactory.CreateFishStretch(origin + progress + new Vector2(seg1.Value.x + seg2HalfWidth, 0f), seg2HalfWidth);
                } else if (seg1.Key.IsGap() && seg2.Key.IsObstacle() && seg3.Key.IsObstacle() && seg2.Key != seg3.Key && seg4.Key.IsGap()) {
                    // If there is a small obstacle followed by a large obstacle (or vice versa) and surrounded by a gap on both sides, 
                    // place a nice parabola of fish

                    var parabolaOrigin = origin + progress + new Vector2(seg1.Value.x - ((DualObstacleParabolaWidth - seg2.Value.x - seg3.Value.x) / 2), 0f);

                    fishFactory.CreateFishParabola(parabolaOrigin, DualObstacleParabolaWidth, 4f, 5);
                } else if (seg1.Key.IsObstacle()
                        && seg2.Key.IsObstacle()
                        && seg1.Key == seg2.Key 
                        && seg3.Key.IsGap()
                        && seg3.Key != Section.SmallGap) {
                    // If there are two of the same obstacle followed by a normal or large, create a parabola starting
                    // from the top of the centre of the two obstacles

                    var parabolaOrigin = origin + progress + seg1.Value;
                    fishFactory.CreateFishParabola(parabolaOrigin, LargeObstacleParabolaWidth, 3f, 5);
                }
                progress += new Vector2(seg1.Value.x, 0f);
            }
        }

        /// <summary>
        /// Evenly place fish over sequences of gaps, except for the first and last gaps in the sequence.
        /// </summary>
        private void PlaceFishOnGapSequences(Vector2 origin,
                                             FishFactoryBehaviour fishFactory,
                                             IList<KeyValuePair<Section, Vector2>> levelSchematics) {
            var gapSequence = new List<KeyValuePair<Section, Vector2>>();
            var progress = Vector2.zero;
            foreach (var segment in levelSchematics) {
                if (segment.Key.IsGap()) {
                    gapSequence.Add(segment);
                } else {
                    var totalWidth = gapSequence.Select(gapSeg => gapSeg.Value.x).Aggregate(0f, (a, b) => a + b);
                    if (gapSequence.Count > 2) {
                        var width = totalWidth - gapSequence[0].Value.x - gapSequence[gapSequence.Count - 1].Value.x;
                        fishFactory.CreateFishStretch(origin + progress + new Vector2(gapSequence[0].Value.x, 0f), width);
                    }
                    gapSequence.Clear();
                    progress += new Vector2(totalWidth + segment.Value.x, 0f);
                }
            }
        }
    }
}