using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList = new List<KitchenObjectSO>();

    private List<KitchenObjectSO> _kitchenObjectSOList = new List<KitchenObjectSO>();

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!this._validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (this._kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            this._kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }

}
