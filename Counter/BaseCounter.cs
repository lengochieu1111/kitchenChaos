using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : RyoMonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaceHere;

    [SerializeField] private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._counterTopPoint == null)
        {
            this._counterTopPoint = this.transform.Find("CounterTopPoint");
        }
    }

    public virtual void Interact(Player player) { }
    public virtual void InteractAlternate(Player player) { }


    // 

    public Transform GetkitchenObjectFollowTransform()
    {
        return this._counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;

        if (this._kitchenObject != null)
        {
            OnAnyObjectPlaceHere?.Invoke(this, EventArgs.Empty);
        }

    }

    public KitchenObject GetKitchenObject()
    {
        return this._kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this._kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this._kitchenObject != null;
    }

}
