using System;
using Core;
using Core.Data;
using Core.Interfaces;
using UI;
using UnityEngine;

namespace CompositionRoot
{
    public class Root : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Menu _menu;
        private ISceneLoader _sceneLoader;
        private IPlayerProgressSaver _playerProgressSaver;

        private void Awake()
        {
            _sceneLoader = new SceneLoader(this);
            _playerProgressSaver = new PlayerProgressSaver();
            _menu.Initialize(_sceneLoader, _playerProgressSaver);
        }
    }
}