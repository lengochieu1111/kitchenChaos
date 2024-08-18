using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : RyoMonoBehaviour
{
    public string CUT = "Cut";

    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Animator _animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._cuttingCounter == null)
        {
            this._cuttingCounter = GetComponentInParent<CuttingCounter>();
        }
        
        if (this._animator == null)
        {
            this._animator = GetComponent<Animator>();
        }
    }

    protected override void Start()
    {
        base.Start();

        this._cuttingCounter.OnCut += CuttingCounter_OnCut; ;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        this._animator.SetTrigger(CUT);
    }

}
