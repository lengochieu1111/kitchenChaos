using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : RyoMonoBehaviour
{
    [SerializeField] private Transform _contrainer;
    [SerializeField] private Transform _recipeTemplate;

    protected override void Awake()
    {
        this._recipeTemplate.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        this.UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        this.UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        this.UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (Transform child in this._contrainer)
        {
            if (child == this._recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaittingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(this._recipeTemplate, this._contrainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>()?.SetRecipeSO(recipeSO);
        }

    }

}
