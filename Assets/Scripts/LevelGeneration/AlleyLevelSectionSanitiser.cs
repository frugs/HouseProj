using System.Collections.Generic;

namespace Assets.Scripts.LevelGeneration {
    public class AlleyLevelSectionSanitiser : ISectionSanitiser {
        public IList<Section> SanitiseSections(IEnumerable<Section> sections) {
            var sanitised = new List<Section> {
                Section.LargeGap,
                Section.LargeGap,
                Section.LargeGap,
                Section.LargeGap,
            };

            var previousSection = Section.LargeGap;
            foreach (var section in sections) {
                if (section.IsGap()) {
                    sanitised.Add(previousSection.IsObstacle() ? Section.LargeGap : section);
                } else if (section.IsObstacle()) {
                    if (previousSection == Section.FloatingBlockMid) {
                        sanitised.Add(Section.NormalGap);
                    }
                    sanitised.Add(section);
                } else if (section == Section.FloatingBlockMid) {
                    if (previousSection.IsObstacle()) {
                        sanitised.Add(Section.NormalGap);
                    }
                    sanitised.Add(section);
                }
                previousSection = section;
            }
            return sanitised;
        }
    }
}