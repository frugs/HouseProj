using System.Collections.Generic;
using Assets.Scripts.LevelGeneration;

namespace Assets.Scripts.LevelSelect {
    public class AlleyLevelInfoBehaviour : AbstractLevelInfoBehaviour {
        public override IDictionary<Section, float> SectionWeights {
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

        public override ISectionSanitiser SectionSanitiser {
            get { return new AlleyLevelSectionSanitiser(); }
        }

        public override IFishPlacer FishPlacer {
            get { return new AlleyFishPlacer(); }
        }

        public override float SmallGapSize {
            get { return 3f; }
        }

        public override float NormalGapSize {
            get { return 4f; }
        }

        public override float LargeGapSize {
            get { return 7f; }
        }

        public override bool IsGroundLevel {
            get { return true; }
        }
    }
}