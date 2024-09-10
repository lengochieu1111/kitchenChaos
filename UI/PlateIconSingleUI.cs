using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : RyoMonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetKitchenObjectSo(KitchenObjectSO kitchenObjectSO)
    {
        this._image.sprite = kitchenObjectSO.sprite;
    }

}
