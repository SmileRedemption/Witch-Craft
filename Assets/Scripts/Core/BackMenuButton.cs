using System;
using Core.Interfaces;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class BackMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        
        private ISceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;
        
        public void Initialize(ISceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        private void Start()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        private void OnBackButtonClicked()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load("Menu");
        }
    }
}