using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    { 
        Idle,
        Frying,
        Fried,
        Burned
    }


    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSOArray;
    [SerializeField] private State _state;

    private FryingRecipeSO _fryingRecipeSO;
    private BurningRecipeSO _burningRecipeSO;
    private float _fryingTimer;
    private float _buringTimer;


    protected override void Start()
    {
        base.Start();

        this._state = State.Idle;

    }

    private void Update()
    {
        if (this.HasKitchenObject())
        {
            switch (this._state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    this._fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = this._fryingTimer / this._fryingRecipeSO.fryingTimeMax
                    });

                    if (this._fryingTimer >= this._fryingRecipeSO.fryingTimeMax)
                    {
                        this.GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(this._fryingRecipeSO.output, this);

                        this._state = State.Fried;
                        this._buringTimer = 0;
                        this._burningRecipeSO = this.GetBurningRecipeSOWithInput(this.GetKitchenObject().GetKitchenObjectSO());

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = this._state,
                        });

                    }

                    break;
                case State.Fried:
                    this._buringTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = this._buringTimer / this._burningRecipeSO.burningTimeMax
                    });

                    if (this._buringTimer >= this._burningRecipeSO.burningTimeMax)
                    {
                        this.GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(this._burningRecipeSO.output, this);

                        this._state = State.Burned;

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = this._state,
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                    break;
                case State.Burned:
                    break;
            }
        }
        
    }

    public override void Interact(Player player)
    {
        if (this.HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(this.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        this.GetKitchenObject().DestroySelf();

                        this._state = State.Idle;

                        OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                        {
                            state = this._state,
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);

                this._state = State.Idle;

                OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                {
                    state = this._state,
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (this.HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    this._fryingRecipeSO = this.GetFryingRecipeSOWithInput(this.GetKitchenObject().GetKitchenObjectSO());
                    this._fryingTimer = 0;
                    this._state = State.Frying;

                    OnStateChange?.Invoke(this, new OnStateChangeEventArgs
                    {
                        state = this._state,
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = this._fryingTimer / this._fryingRecipeSO.fryingTimeMax
                    });
                }
            }
        }
    }

    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = this.GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }

    public KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = this.GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }

        return null;
    }

    public FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in this._fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }
    
    public BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in this._burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }

        return null;
    }


}
