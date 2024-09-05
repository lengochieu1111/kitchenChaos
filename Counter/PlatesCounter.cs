using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private float _spawnPlateTimerMax = 4f;
    [SerializeField] private int _plateSpawnedAmountMax = 5;
    private float _spawnPlateTimer;
    private int _plateSpawnedAmount;

    private void Update()
    {
        this._spawnPlateTimer += Time.deltaTime;

        if (this._spawnPlateTimer >= this._spawnPlateTimerMax)
        {
            this._spawnPlateTimer = 0;

            if (this._plateSpawnedAmount < this._plateSpawnedAmountMax)
            {
                this._plateSpawnedAmount++;

                KitchenObject.SpawnKitchenObject(this._kitchenObjectSO, this);

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (this._plateSpawnedAmount > 0)
            {
                this._plateSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(this._kitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }


}
