using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : RyoMonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kischenObjectSO;
    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return this._kischenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this._kitchenObjectParent != null)
        {
            this._kitchenObjectParent.ClearKitchenObject();
        }

        this._kitchenObjectParent = kitchenObjectParent;

        //if (this._kitchenObjectParent.HasKitchenObject() )
        //{
        //    Debug.Log("I Kitchen Object Parent has Kitchent Object");
        //}

        this._kitchenObjectParent.SetKitchenObject(this);

        this.transform.parent = kitchenObjectParent.GetkitchenObjectFollowTransform();
        this.transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return this._kitchenObjectParent;
    }

    public void DestroySelf()
    {
        this.GetKitchenObjectParent().ClearKitchenObject();

        Destroy(this.gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }    
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }

}
