using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.LevelGeneration;

namespace Assets.Scripts.LevelSelect {
    public class IndoorEasyLevelInfoBehaviour : AbstractDelegatingLevelInfoBehaviour<IndoorLevelInfoBehaviour> {
        public override IDictionary<Section, float> SectionWeights {
            get {
                var weights = new Dictionary<Section, float>(Delegate.SectionWeights);
                weights.Remove(Section.SmallGap);
                weights.Remove(Section.LargeGap);
                return weights;
            }
        }

        public override ISectionSanitiser SectionSanitiser {
            get { return new SectionSanitiserImpl(Delegate.SectionSanitiser); }
        }

        private class SectionSanitiserImpl : ISectionSanitiser {
            private readonly ISectionSanitiser _sectionSanitiser;

            public SectionSanitiserImpl(ISectionSanitiser sectionSanitiser) {
                _sectionSanitiser = sectionSanitiser;
            }

            public IList<Section> SanitiseSections(IEnumerable<Section> sections) {
                var prelim = _sectionSanitiser.SanitiseSections(sections).ToList();

                var sanitised = new List<Section>();
                for (int i = 1; i < prelim.Count; i++) {
                    var seg1 = prelim[i - 1];
                    var seg2 = prelim[i];

                    if (seg1.IsPlatform() && seg2.IsFloatingBlock()) {
                        sanitised.Add(seg1);
                        sanitised.Add(Section.PlatformMid);
                    } else if (seg1.IsFloatingBlock() && seg2.IsPlatform()) {
                        sanitised.Add(seg1);
                        sanitised.Add(Section.PlatformMid);
                    } else {
                        sanitised.Add(seg1);                        
                    }
                }

                return sanitised;
            }
        }
    }
}