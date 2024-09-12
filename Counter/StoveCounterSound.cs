using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : RyoMonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;

    private AudioSource _audioSource;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this._audioSource = GetComponent<AudioSource>();
        this._stoveCounter = GetComponentInParent<StoveCounter>();
    }

    protected override void Start()
    {
        base.Start();

        this._stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            this._audioSource.Play();
        }
        else
        {
            this._audioSource.Pause();
        }
    }


}
