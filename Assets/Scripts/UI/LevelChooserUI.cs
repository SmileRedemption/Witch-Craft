using Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelChooserUI : MonoBehaviour
    {
        [SerializeField] private LevelButton[] _levels;
        
        private ISceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;

        public void Initialize(ISceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            foreach (var levelButton in _levels) 
                levelButton.Button.onClick.AddListener(()=> OnLevelButtonClicked(levelButton.Level));
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnLevelButtonClicked(int level)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load($"Level {level}");
        }
    }
}