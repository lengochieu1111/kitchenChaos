using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] protected KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!this.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(this._kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>()?.SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
    
}
