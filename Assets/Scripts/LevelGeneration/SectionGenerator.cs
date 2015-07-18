using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public enum Section {
        PlatformLeft,
        PlatformMid,
        PlatformRight,
        FloatingBlockLeft,
        FloatingBlockMid,
        FloatingBlockRight,
        SmallGap,
        NormalGap,
        LargeGap,
        SmallObstacle,
        LargeObstacle,
        EndLevel
    }

    public static class SectionExtensions {
        private static readonly HashSet<Section> GapSections = new HashSet<Section> {
            Section.SmallGap, Section.NormalGap, Section.LargeGap
        };
        
        private static readonly HashSet<Section> FloatingBlockSections = new HashSet<Section> {
            Section.FloatingBlockLeft, Section.FloatingBlockMid, Section.FloatingBlockRight
        };
        
        private static readonly HashSet<Section> PlatformSections = new HashSet<Section> {
            Section.PlatformLeft, Section.PlatformMid, Section.PlatformRight
        };
        
        public static bool IsGap(this Section section) {
            return GapSections.Contains(section);
        }

        public static bool IsObstacle(this Section section) {
            return section == Section.SmallObstacle || section == Section.LargeObstacle;
        }

        public static bool IsFloatingBlock(this Section section) {
            return FloatingBlockSections.Contains(section);
        }

        public static bool IsPlatform(this Section section) {
            return PlatformSections.Contains(section);
        }
    }

    public class SectionGenerator {
        private readonly IDictionary<Section, float> _sectionWeights;

        public SectionGenerator(IDictionary<Section, float> sectionWeights) {
            _sectionWeights = sectionWeights;
        }

        public IEnumerable<Section> GenerateSections(int sectionCount) {
            var cumulativeSectionWeights = new OrderedDictionary();
            var cumulativeWeight = 0f;
            foreach (var pair in _sectionWeights) {
                cumulativeWeight += pair.Value;
                cumulativeSectionWeights.Add(pair.Key, cumulativeWeight);
            }

            var sections = new List<Section> {Section.PlatformMid};
            for (var i = 0; i < sectionCount; i++) {
                foreach (var entry in cumulativeSectionWeights) {
                    var dictionaryEntry = (DictionaryEntry) entry;
                    if (Random.Range(0f, cumulativeWeight) <= (float) dictionaryEntry.Value) {
                        var section = (Section) dictionaryEntry.Key;
                        sections.Add(section);
                        break;
                    }
                }
            }
            sections.Add(Section.NormalGap);

            return sections;
        }
    }
}