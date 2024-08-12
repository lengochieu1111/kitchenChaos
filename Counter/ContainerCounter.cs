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
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(this._kitchenObjectSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
    
}
