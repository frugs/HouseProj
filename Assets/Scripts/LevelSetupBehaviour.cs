﻿using System.Linq;
using Assets.Scripts.LevelGeneration;
using UnityEngine;

namespace Assets.Scripts {
    public class LevelSetupBehaviour : MonoBehaviour {
        public static Level ActiveLevel { get; set; }

        [SerializeField]
        private SpriteRenderer _backgroundRenderer;

        public void Start() {
            var levelInfo = ActiveLevel == Level.Indoor
                    ? (ILevelInfo) GetComponentsInChildren<IndoorLevelInfoBehaviour>(true).First()
                    : GetComponentsInChildren<AlleyLevelInfoBehaviour>(true).First();


            JukeboxBehaviour.Instance.GetComponent<AudioSource>().clip = levelInfo.Bgm;
            JukeboxBehaviour.Instance.StartMusic();

            _backgroundRenderer.sprite = levelInfo.Background;

            var levelGenerator = new LevelGenerator(Vector2.zero, levelInfo);
            var sectionGenerator = new SectionGenerator(levelInfo.SectionWeights);
            var sections = levelInfo.SectionSanitiser.SanitiseSections(sectionGenerator.GenerateSections(120));
            var levelSchematics = levelGenerator.GenerateLevel(sections);
            levelInfo.FishPlacer.LayFish(Vector2.zero, levelInfo.FishFactory, levelSchematics);
        }
    }
}