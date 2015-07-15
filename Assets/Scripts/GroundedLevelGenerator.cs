using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGeneration {
    public class GroundedLevelGenerator : ILevelGenerator {

        private ObstacleFactoryBehaviour _obstacleFactory;

        public void GenerateLevel(IEnumerable<Section> sections) {

        }
    }

    public class ObstacleFactoryBehaviour : MonoBehaviour {}
}