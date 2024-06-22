using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class PotionMatchAnimation
    {
        public void AnimateDestroy(Transform element, Action onCallback)
        {
            var sequence = DOTween.Sequence();

            var normalScale = element.transform.localScale;
            sequence.Append(element.DOScale(Vector3.zero, 0.5f));
            sequence.AppendCallback(onCallback.Invoke);
            sequence.Append(element.DOScale(normalScale, 0.5f));

            sequence.Play()
                .OnComplete(() => sequence.Kill());
        }

        public void AnimateSpriteChange(Image image, Sprite newSprite, float animationDuration, Action onAction = null)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(image.DOFade(0f, animationDuration / 2f));

            sequence.AppendCallback(() => image.sprite = newSprite);

            sequence.Append(image.DOFade(1f, animationDuration / 2f));

            if (onAction != null)
                sequence.AppendCallback(onAction.Invoke);

            sequence.Play()
                .OnComplete(() => sequence.Kill());
        }
    }
}