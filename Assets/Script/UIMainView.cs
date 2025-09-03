using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
// using DG.Tweening;

public class UIMainView : MonoBehaviour
{
    public Action OnClickR;
    public Action OnClickL;

    [SerializeField] private Button _lBtn;
    [SerializeField] private Button _rBtn;
    [SerializeField] private TextMeshProUGUI _playTime;
    [SerializeField] private float scaleUp = 1.2f;
    [SerializeField] private float duration = 0.1f;

    public void SetPlayTime(string text)
    {
        _playTime.text = text;
    }
    private void Start()
    {
        _lBtn.onClick.AddListener(OnClickLBtn);
        _rBtn.onClick.AddListener(OnClickRBtn);
    }

    private void OnClickLBtn()
    {
        OnClickL?.Invoke();
        BtnEffect(_lBtn);
    }
    
    private void OnClickRBtn()
    {
        OnClickR?.Invoke();
        BtnEffect(_rBtn);
    }

    private void BtnEffect(Button btn)
    {
        Vector3 originalScale = btn.transform.localScale;
        // btn.transform.DOScale(originalScale * scaleUp, duration)
        //     .SetEase(Ease.OutQuad)
        //     .OnComplete(() =>
        //     {
        //         btn.transform.DOScale(originalScale, duration)
        //             .SetEase(Ease.OutQuad);
        //     });
    }
}
