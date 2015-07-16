using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public interface IFishPlacer {
        void PlaceFish(Vector2 origin, FishFactoryBehaviour fishFactory, IList<KeyValuePair<Section, Vector2>> levelSchematics);
    }
}