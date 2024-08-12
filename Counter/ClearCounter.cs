using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] protected KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (this.HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
    }


}
