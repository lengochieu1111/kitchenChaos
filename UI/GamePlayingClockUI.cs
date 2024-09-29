using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : RyoMonoBehaviour
{
    [SerializeField] private Image _timerImage;

    protected override void Start()
    {
        base.Start();

        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        this.Hide();
    }

    private void Update()
    {
        this._timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            this.Show();
        }
        else
        {
            this.Hide();
        }
    }

    private void Show()
    {
        this.gameObject.SetActive(true);
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

}
