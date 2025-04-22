using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UiTool
{
    public class OverlayToast : MonoBehaviour
    {
        [SerializeField] private RectTransform errorScreen;
        [SerializeField] private CanvasGroup errorScreenCanvasGroup;
        [SerializeField] private TextMeshProUGUI errorText;
        [SerializeField] private Canvas canvas;
        

        private void Awake()
        {
            canvas.worldCamera = Camera.main;
        }

        private void OnEnable()
        {
            errorScreen.gameObject.SetActive(false);
        }

        private Tweener tweener;

        public void ShowToast(string text, float stayDuration = 1f)
        {
            tweener?.Complete();
            errorScreen.DOComplete();
            errorScreenCanvasGroup.DOComplete();
            errorScreen.gameObject.SetActive(true);
            errorText.text = text;
            errorScreen.anchoredPosition = Vector2.zero + Vector2.down * 25;
            errorScreen.DOAnchorPosY(25, stayDuration);
            errorScreenCanvasGroup.alpha = 0;
            errorScreenCanvasGroup.DOFade(1f, .2f);
            var t = 0f;
            tweener = DOTween.To(() => t, (v) => t = v, 1f, stayDuration - 0.2f).OnComplete(() =>
            {
                errorScreenCanvasGroup.DOFade(0f, .2f).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        }
    }
}