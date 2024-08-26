using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : RyoMonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveGameObject;
    [SerializeField] private GameObject _particleGameObject;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._stoveCounter == null)
        {
            this._stoveCounter = GetComponentInParent<StoveCounter>();
        }

    }

    protected override void Start()
    {
        base.Start();

        this._stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        this._stoveGameObject.SetActive(showVisual);
        this._particleGameObject.SetActive(showVisual);
    }


}
