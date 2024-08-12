using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSO;

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
                if (this.HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
    }

    public override void InteractAlternate(Player player) 
    { 
        if (this.HasKitchenObject() && this.HasRecipeWithInput(this.GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectSO kitchenObjectSO = this.GetOutputForInput(this.GetKitchenObject().GetKitchenObjectSO());

            this.GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
        }
    }

    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in this._cuttingRecipeSO)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }

        return false;
    }

    public KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in this._cuttingRecipeSO)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }

        return null;
    }

}
