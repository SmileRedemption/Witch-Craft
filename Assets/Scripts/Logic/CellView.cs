using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Logic
{
    public class CellView : MonoBehaviour
    {

        [SerializeField] private Image _potion;
        [SerializeField] private Button _button;
        [SerializeField] private Image _frame;
        [SerializeField] private AudioSource _audioSource;
        
        private Vector3 _normalScale;
        private Vector3 _selectScale;
        
        public Sprite Sprite => _potion.sprite;
        public Image Potion => _potion;

        public PotionType PotionType { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public event Action<int, int> OnButtonClicked;

        private void Awake()
        {
            _normalScale = transform.localScale;
            _selectScale += _normalScale * 1.5f;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() => OnButtonClicked?.Invoke(X, Y));
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void InitializeCell(Sprite potionSprite, int x, int y, PotionType potionType)
        {
            _potion.sprite = potionSprite;
            SetPosition(x, y);
            PotionType = potionType;
        }
        
        public void ChangeSprite(Sprite candy) => 
            _potion.sprite = candy;

        public void Select()
        {
            _frame.transform.localScale = _selectScale;
            _audioSource.Play();
        }

        public void UnSelect() => _frame.transform.localScale = _normalScale;

        private void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}