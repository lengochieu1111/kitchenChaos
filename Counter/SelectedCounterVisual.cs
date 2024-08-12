using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : RyoMonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private List<GameObject> _virtualGameObjects;


    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._baseCounter == null)
        {
            this._baseCounter = GetComponentInParent<BaseCounter>();
        }

        if (this._virtualGameObjects.Count <= 0)
        {
            foreach (Transform child in transform)
            {
                this._virtualGameObjects.Add(child.gameObject);
            }
            
        }

    }

    protected override void Start()
    {
        base.OnEnable();

        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;

    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == this._baseCounter)
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
        foreach (GameObject child in this._virtualGameObjects)
        {
            child.SetActive(true);
        }
    }
    
    private void Hiden()
    {
        foreach (GameObject child in this._virtualGameObjects)
        {
            child.SetActive(false);
        }
    }

}
