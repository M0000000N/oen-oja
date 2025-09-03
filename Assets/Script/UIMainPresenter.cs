using System;
using UnityEngine;

public class UIMainPresenter : MonoBehaviour
{
    [SerializeField] UIMainView _uiMainView;
    private float playTime;

    void Update()
    {
        playTime += Time.deltaTime;
        TimeSpan ts = TimeSpan.FromSeconds(playTime);
        string formatted = string.Format("{0:D2}:{1:D2}:{2:D2}",
            ts.Minutes,
            ts.Seconds,
            ts.Milliseconds / 10);
        _uiMainView.SetPlayTime(formatted);
    }
}
