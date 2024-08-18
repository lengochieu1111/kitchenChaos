using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : RyoMonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image _progressBarImage;

    protected override void Start()
    {
        base.Start();

        this._cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;

        this._progressBarImage.fillAmount = 0f;

        this.Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
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
