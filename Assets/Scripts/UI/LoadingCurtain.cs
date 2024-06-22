using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void Show()
        {
            gameObject.SetActive(true);
            _slider.value = _slider.minValue;
            StartCoroutine(Loading());
        }

        private IEnumerator Loading()
        {
            while (_slider.value <= 0.9f)
            {
                _slider.value += 0.1f;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}