using System.Collections.Generic;

namespace Assets.Scripts.LevelGeneration {
    public interface ILevelGenerator {
        void GenerateLevel(IEnumerable<Section> sections);
    }
}