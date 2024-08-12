using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : RyoMonoBehaviour
{
    public static string IS_WALKING = "IsWalking";

    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._animator == null)
        {
            this._animator = GetComponent<Animator>();
        }

        if (this._player == null)
        {
            this._player = GetComponentInParent<Player>();
        }
            
    }

    private void Update()
    {
        this._animator.SetBool(IS_WALKING, this._player.IsWalking());
    }

}
