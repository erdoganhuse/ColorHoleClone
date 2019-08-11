using System;
using System.Collections.Generic;
using DeveGames.Extensions;
using DeveGames.CoroutineSystem;
using UnityEngine.SceneManagement;

namespace DeveGames.SceneManager
{
    public static class SceneManager
    {
        #region Events

        public static event Action<string> OnSceneLoad;
        public static event Action<string> OnSceneUnload;

        public static readonly List<string> LoadedScenes = new List<string>();

        #endregion

        #region Methods

        public static void LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Additive, Action onComplete = null)
        {
            if (LoadedScenes.Contains(sceneName)) return;

            LoadedScenes.Add(sceneName);

            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadMode) =>
            {
                if (scene.name != sceneName) return;
                onComplete.SafeInvoke();
                OnSceneLoad.SafeInvoke(sceneName);
            };
        }

        public static void UnloadSceneAsync(string sceneName, Action onComplete = null)
        {
            if (!LoadedScenes.Contains(sceneName)) return;

            LoadedScenes.Remove(sceneName);

            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);

            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += (scene) =>
            {
                if (scene.name != sceneName) return;
                onComplete.SafeInvoke();
                OnSceneUnload.SafeInvoke(sceneName);
            };
        }

        public static void LoadAndCloseScene(string sceneName, float closeTime, Action onComplete = null)
        {
            LoadSceneAsync(sceneName, LoadSceneMode.Additive, () =>
            {
                OnSceneLoad.SafeInvoke(sceneName);

                CoroutineManager.DoAfterGivenTime(closeTime, () =>
                {
                    UnloadSceneAsync(sceneName, onComplete.SafeInvoke);
                });
            });
        }

        public static void UnloadAllAsync(Predicate<string> predicate)
        {
            for (int i = 0; i < LoadedScenes.Count; i++)
            {
                if (predicate(LoadedScenes[i]))
                {
                    UnloadSceneAsync(LoadedScenes[i]);
                }
            }
        }

        #endregion       
    }
}
