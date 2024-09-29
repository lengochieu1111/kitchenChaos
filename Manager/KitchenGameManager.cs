using Pattern.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : Singleton<KitchenGameManager>
{

    public event EventHandler OnStateChanged;

    private enum State
    { 
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    [SerializeField] private State _state;
    [SerializeField] private float _waitingToStartTimer = 1f;
    [SerializeField] private float _countdownToStartTimer = 3f;
    [SerializeField] private float _gamePlayingTimerMax = 10f;
    private float _gamePlayingTimer;

    protected override void Start()
    {
        base.Start();

        this._state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (this._state)
        {
            case State.WaitingToStart:
                this._waitingToStartTimer -= Time.deltaTime;
                if (this._waitingToStartTimer < 0f)
                {
                    this._state = State.CountdownToStart;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                this._countdownToStartTimer -= Time.deltaTime;
                if (this._countdownToStartTimer < 0f)
                {
                    this._state = State.GamePlaying;
                    this._gamePlayingTimer = this._gamePlayingTimerMax;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                this._gamePlayingTimer -= Time.deltaTime;
                if (this._gamePlayingTimer < 0f)
                {
                    this._state = State.GameOver;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

    }

    public bool IsGamePlaying()
    {
        return this._state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return this._state == State.CountdownToStart;
    }
    
    public float GetCountdownToStartTimer()
    {
        return this._countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return this._state == State.GameOver;
    }
    
    
    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (this._gamePlayingTimer / this._gamePlayingTimerMax);
    }

}
