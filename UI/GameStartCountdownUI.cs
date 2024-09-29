using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : RyoMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;

    protected override void Start()
    {
        base.Start();

        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        this.Hide();
    }

    private void Update()
    {
        this._countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
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
