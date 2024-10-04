using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : RyoMonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _messageText;

    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failedSprite;
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _failedColor;

    protected override void Start()
    {
        base.Start();

        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        this.Hide();
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        this.Show();

        this._background.color = this._failedColor;
        this._iconImage.sprite = this._failedSprite;
        this._messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        this.Show();

        this._background.color = this._successColor;
        this._iconImage.sprite = this._successSprite;
        this._messageText.text = "DELIVERY\nSUCCESS";
    }

    private void Show()
    {
        this.transform.gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        this.transform.gameObject.SetActive(false);
    }

}
