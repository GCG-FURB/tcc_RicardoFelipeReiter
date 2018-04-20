﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public class SceneController : MonoBehaviour, CubeMarkerInteractor {

        public List<Scene> scenes = new List<Scene>();
        public Scene EmptyScenePrefab;
        public GameObject DesactivatedScenes;
        public CubeMarkerController CubeMarker;
        public RecorderController RecorderController;
        public AnimationController AnimationController;

        private int currentScene = -1;

        public int CurrentScene {
            get {
                return currentScene;
            }
            set {
                // TODO: criar listener
                CubeMarker.ResetAttached();
                RecorderController.StopRecording();
                AnimationController.SceneChanged();

                if (currentScene > -1) {
                    GetCurrentScene().Map.transform.parent = DesactivatedScenes.transform;
                }
                currentScene = value;
                GetCurrentScene().Map.transform.parent = this.transform;
            }
        }

        public void AddNewScene() {
            Scene newScene = GameObject.Instantiate(EmptyScenePrefab, this.transform);
            scenes.Add(newScene);
            CurrentScene = scenes.Count() - 1;
        }

        public Scene GetCurrentScene() {
            return scenes[currentScene];
        }

        public bool ObjectReceived(GameObject obj) {
            obj.transform.parent = GetCurrentScene().Map.transform;
            return true;
        }

        public void ObjectRemoved(GameObject obj) {

        }

    }
}
