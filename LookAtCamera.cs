using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : RyoMonoBehaviour
{
    public enum Mode
    {
        LookAt,
        LookAtInverted,
        LookAtForward,
        LookAtForwardInverted,
    }

    [SerializeField] private Mode _mode;

    private void LateUpdate()
    {
        switch(this._mode)
        {
            case Mode.LookAt:
                this.transform.LookAt(Camera.main.transform);
                break; 
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = this.transform.position - Camera.main.transform.position;
                this.transform.LookAt(this.transform.position + dirFromCamera);
                break;
            case Mode.LookAtForward:
                this.transform.forward = Camera.main.transform.forward;
                break; 
            case Mode.LookAtForwardInverted:
                this.transform.forward = -Camera.main.transform.forward;
                break; 

        }
    }

}
