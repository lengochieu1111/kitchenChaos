using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : RyoMonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kischenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return this._kischenObjectSO;
    }

}
