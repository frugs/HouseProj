using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace Assets.Scripts {
    public enum Section {
        Ground,
        SmallGap,
        LargeGap
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

            var sections = new List<Section> {Section.Ground};
            for (var i = 0; i < sectionCount; i++) {
                foreach (var entry in cumulativeSectionWeights) {
                    var dictionaryEntry = (DictionaryEntry) entry;
                    if (Random.Range(0f, cumulativeWeight) <= (float) dictionaryEntry.Value) {
                        var section = (Section) dictionaryEntry.Key;
                        sections.Add(section);

                        if (section == Section.SmallGap || section == Section.LargeGap) {
                            sections.Add(Section.Ground);
                        }
                        break;
                    }
                }
            }
            sections.Add(Section.SmallGap);

            return sections;
        }
    }
}