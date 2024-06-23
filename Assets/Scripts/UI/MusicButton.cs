using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MusicButton : MonoBehaviour
    {
        [SerializeField] private BackgroundMusic _backgroundMusic;
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Sprite _musicOnSprite;
        [SerializeField] private Sprite _musicOffSprite;

        private bool _isMusicPLay = true;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnMusicButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnMusicButtonClick()
        {
            if (_isMusicPLay)
            {
                _isMusicPLay = false;
                _buttonImage.sprite = _musicOffSprite;
                _backgroundMusic.Pause();
            }
            else
            {
                _isMusicPLay = true;
                _buttonImage.sprite = _musicOnSprite;
                _backgroundMusic.Resume();
            }
            
        }
    }
}