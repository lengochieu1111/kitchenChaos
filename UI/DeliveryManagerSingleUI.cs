using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : RyoMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeNameUI;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Transform _iconTemplate;

    protected override void Awake()
    {
        this._iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        this._recipeNameUI.text = recipeSO.recipeName;

        foreach (Transform child in this._iconContainer)
        {
            if (child == this._iconTemplate) continue;
            Destroy(child);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(this._iconTemplate, this._iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }


    }

}
