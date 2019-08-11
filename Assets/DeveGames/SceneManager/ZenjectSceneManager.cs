using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Zenject;

namespace DeveGames.SceneManager
{
    public class ZenjectSceneManager
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly List<Scene> _loadedScenes;

        private Action _onSceneLoaded;
        private Action _onSceneUnloaded;
        
        public ZenjectSceneManager(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            
            _loadedScenes = new List<Scene>();
        }
        
        public void LoadSceneAsync(string sceneName, Action onSceneLoaded = null)
        {
            if (_loadedScenes.Any(item => item.name == sceneName)) return;
                        
            _sceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            _onSceneLoaded = onSceneLoaded;
            
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
        }

        public void UnloadSceneAsync(string sceneName, Action onSceneUnloaded = null)
        {
            if (_loadedScenes.All(item => item.name != sceneName)) return;
            
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            _onSceneUnloaded = onSceneUnloaded;
            
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += SceneManager_OnSceneUnloaded; 
        }

        public void SetActiveScene(string sceneName)
        {
            if (_loadedScenes.All(item => item.name != sceneName)) return;

            Scene scene = _loadedScenes.FirstOrDefault(item => item.name == sceneName);
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
        }
        
        #region Event Listeners

        private void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            _loadedScenes.Add(scene);

            _onSceneLoaded?.Invoke();
            _onSceneLoaded = null;
        }
        
        private void SceneManager_OnSceneUnloaded(Scene scene)
        {
            _loadedScenes.Remove(scene);
            
            _onSceneUnloaded?.Invoke();
            _onSceneUnloaded = null;
        }

        #endregion
    }
}