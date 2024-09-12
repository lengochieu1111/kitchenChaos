using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : RyoMonoBehaviour
{
    [SerializeField] private Player _player;
    private float _footstepTimer;
    private float _footstepTimerMax = 0.1f;

    protected override void Awake()
    {
        base.Awake();

        this._player = GetComponent<Player>();
    }

    private void Update()
    {
        this._footstepTimer -= Time.deltaTime;

        if (this._footstepTimer < 0f)
        {
            this._footstepTimer = this._footstepTimerMax;

            if (this._player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepSound(this._player.transform.position, volume);
            }
        }
    }

}
