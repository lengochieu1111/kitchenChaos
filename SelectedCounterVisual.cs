using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : RyoMonoBehaviour
{
    [SerializeField] private ClearCounter _clearCounter;
    [SerializeField] private GameObject _virtualGameObject;


    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._clearCounter == null)
        {
            this._clearCounter = GetComponentInParent<ClearCounter>();
        }

        if (this._virtualGameObject == null)
        {
            this._virtualGameObject = this.transform.GetChild(0)?.gameObject;
        }

    }

    protected override void Start()
    {
        base.OnEnable();

        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;

    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == this._clearCounter)
        {
            this.Show();
        }
        else
        {
            this.Hiden();
        }
    }

    private void Show()
    {
        this._virtualGameObject.SetActive(true);
    }
    
    private void Hiden()
    {
        this._virtualGameObject.SetActive(false);
    }

}
