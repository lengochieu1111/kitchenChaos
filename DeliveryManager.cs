using Pattern.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Singleton<DeliveryManager>
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO _recipeListSO;

    public List<RecipeSO>  _waittingRecipeSOList = new List<RecipeSO>();

    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waittingRecipesMax = 4;

    private void Update()
    {
        this._spawnRecipeTimer -= Time.deltaTime;
        if (this._spawnRecipeTimer <= 0f)
        {
            this._spawnRecipeTimer = this._spawnRecipeTimerMax;

            if (this._waittingRecipeSOList.Count < this._waittingRecipesMax)
            {
                RecipeSO waittingRecipeSO = this._recipeListSO.recipeSOList[UnityEngine.Random.Range(0, this._recipeListSO.recipeSOList.Count)];
                this._waittingRecipeSOList.Add(waittingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);

                Debug.Log(waittingRecipeSO.recipeName);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < this._waittingRecipeSOList.Count; i++)
        {
            RecipeSO waittingRecipeSO = this._waittingRecipeSOList[i];

            if (waittingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKichenObjectSO in waittingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKichenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (recipeKichenObjectSO == plateKichenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    this._waittingRecipeSOList.RemoveAt(i);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaittingRecipeSOList()
    {
        return this._waittingRecipeSOList;
    }

}
