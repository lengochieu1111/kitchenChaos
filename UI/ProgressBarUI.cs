using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : RyoMonoBehaviour
{
    [SerializeField] private GameObject _hasProgressGameObject;
    [SerializeField] private Image _progressBarImage;

    private IHasProgress _iHasProgress;

    protected override void Start()
    {
        base.Start();

        this._iHasProgress = this._hasProgressGameObject.GetComponent<IHasProgress>();

        this._iHasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        this._progressBarImage.fillAmount = 0f;

        this.Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        this._progressBarImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            this.Hide();
        }
        else
        {
            this.Show();
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
