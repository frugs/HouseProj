using System.Collections.Generic;

namespace Assets.Scripts.LevelGeneration {
    public interface ISectionSanitiser {
        IList<Section> SanitiseSections(IEnumerable<Section> sections);
    }
}