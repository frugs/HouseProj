using System.Collections.Generic;
using Assets.Scripts.LevelGeneration;

namespace Assets.Scripts.LevelSelect {
    public class IndoorLevelInfoBehaviour : AbstractLevelInfoBehaviour {
        public override IDictionary<Section, float> SectionWeights {
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

        public override ISectionSanitiser SectionSanitiser {
            get { return new IndoorLevelSectionSanitizer(); }
        }

        public override IFishPlacer FishPlacer {
            get { return new IndoorFishPlacer(); }
        }

        public override float SmallGapSize {
            get { return 2.5f; }
        }

        public override float NormalGapSize {
            get { return 4.3f; }
        }

        public override float LargeGapSize {
            get { return 5.7f; }
        }

        public override bool IsGroundLevel {
            get { return false; }
        }
    }
}