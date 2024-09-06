using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : RyoMonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    protected override void Awake()
    {
        this._iconTemplate.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();

        this._plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        this.UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in this.transform)
        {
            if (child == this._iconTemplate) continue;

            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in this._plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform =  Instantiate(this._iconTemplate, this.transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>()?.SetKitchenObjectSo(kitchenObjectSO);
        }
    }


}
