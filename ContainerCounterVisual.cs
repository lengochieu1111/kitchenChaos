using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : RyoMonoBehaviour
{
    public string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter _containerCounter;
    [SerializeField] private Animator _animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._containerCounter == null)
        {
            this._containerCounter = GetComponentInParent<ContainerCounter>();
        }
        
        if (this._animator == null)
        {
            this._animator = GetComponent<Animator>();
        }
    }

    protected override void Start()
    {
        base.Start();

        this._containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        this._animator.SetTrigger(OPEN_CLOSE);
    }
}
