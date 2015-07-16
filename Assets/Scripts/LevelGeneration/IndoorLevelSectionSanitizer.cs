using System.Collections.Generic;

namespace Assets.Scripts.LevelGeneration {
    public class IndoorLevelSectionSanitizer : ISectionSanitiser {
        public IList<Section> SanitiseSections(IEnumerable<Section> sections) {
            var sanitised = new List<Section> {
                Section.PlatformLeft,
                Section.PlatformMid,
                Section.PlatformMid,
                Section.PlatformRight,
                Section.NormalGap,
                Section.PlatformLeft,
                Section.PlatformRight,
                Section.PlatformLeft,
                Section.FloatingBlockLeft,
                Section.FloatingBlockMid,
                Section.FloatingBlockRight,
                Section.PlatformRight,
                Section.SmallGap,
                Section.PlatformLeft
            };

            var previousSection = Section.PlatformLeft;
            foreach (var section in sections) {
                if (section.IsFloatingBlock()) {
                    if (previousSection.IsGap()) {
                        if (previousSection == Section.SmallGap) {
                            sanitised.Add(Section.SmallGap);
                        }
                        sanitised.Add(Section.PlatformLeft);
                        sanitised.Add(Section.FloatingBlockLeft);
                    } else {
                        sanitised.Add(previousSection.IsFloatingBlock()
                                ? Section.FloatingBlockMid
                                : Section.FloatingBlockLeft);
                    }
                } else {
                    if (previousSection.IsFloatingBlock()) {
                        sanitised.Add(Section.FloatingBlockRight);
                    }

                    if (section.IsGap()) {
                        if (previousSection.IsGap()) {
                            sanitised.Add(Section.PlatformLeft);
                        }

                        sanitised.Add(Section.PlatformRight);
                        sanitised.Add(section);
                    } else if (section.IsPlatform()) {
                        sanitised.Add(previousSection.IsGap()
                                ? Section.PlatformLeft
                                : Section.PlatformMid);
                    }
                }
                previousSection = section;
            }
            return sanitised;
        }
    }
}