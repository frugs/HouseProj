﻿using System.Collections.Generic;
using Assets.Scripts.LevelGeneration;

namespace Assets.Scripts.LevelSelect {
    public class AlleyEasyLevelInfoBehaviour : AbstractDelegatingLevelInfoBehaviour<AlleyLevelInfoBehaviour> {
        public override ISectionSanitiser SectionSanitiser {
            get { return new SectionSanitiserImpl(); }
        }

        private class SectionSanitiserImpl : ISectionSanitiser {
            public IList<Section> SanitiseSections(IEnumerable<Section> sections) {
                return new List<Section> {
                    Section.SmallGap,
                    Section.LargeGap,
                    Section.LargeGap,
                    Section.SmallGap,
                    Section.SmallObstacle,
                    Section.SmallGap,
                    Section.LargeGap,
                    Section.LargeGap,
                    Section.LargeGap,
                    Section.SmallGap,
                    Section.LargeObstacle,
                    Section.SmallGap,
                    Section.LargeGap,
                    Section.LargeGap,
                    Section.SmallGap,
                    Section.FloatingBlockMid,
                    Section.SmallGap,
                    Section.LargeGap,
                    Section.LargeGap,
                    Section.LargeGap,
                    Section.SmallGap,
                    Section.EndLevel
                };
            }
        }
    }
}