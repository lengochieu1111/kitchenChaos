using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : RyoMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipesDelivered;

    protected override void Start()
    {
        base.Start();

        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;

        this.Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            this.Show();

            this._recipesDelivered.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
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
