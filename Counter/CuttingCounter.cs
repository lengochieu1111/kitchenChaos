using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSO;
    private int _cuttingProgress;

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
                    this._cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = this.GetCuttingRecipeSOWithInput(this.GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)this._cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
        }
    }

    public override void InteractAlternate(Player player) 
    { 
        if (this.HasKitchenObject() && this.HasRecipeWithInput(this.GetKitchenObject().GetKitchenObjectSO()))
        {
            this._cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = this.GetCuttingRecipeSOWithInput(this.GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                progressNormalized = (float)this._cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (this._cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO kitchenObjectSO = this.GetOutputForInput(this.GetKitchenObject().GetKitchenObjectSO());

                this.GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            }
        }
    }

    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = this.GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    public KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = this.GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }

        return null;
    }
    
    public CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in this._cuttingRecipeSO)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }

}
