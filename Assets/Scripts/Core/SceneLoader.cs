using System.Collections;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoader : ISceneLoader
    {
        private const float MinLoadingTime = 1f;
        
        private readonly ICoroutineRunner _coroutineRunner;
        
        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string name) => 
            _coroutineRunner.StartCoroutine(LoadScene(name));

        private IEnumerator LoadScene(string nextScene)
        {
            var startTime = Time.time;
            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            waitNextScene.allowSceneActivation = false;

            while (waitNextScene.progress <= 0.9f && Time.time - startTime <= MinLoadingTime)
            {
                yield return null;
            }
            
            waitNextScene.allowSceneActivation = true;
        }
    }
}