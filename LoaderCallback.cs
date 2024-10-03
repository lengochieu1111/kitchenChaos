using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : RyoMonoBehaviour
{
    private bool _isFirstUpdate = true;

    private void Update()
    {
        if (this._isFirstUpdate)
        {
            this._isFirstUpdate = false;

            Loader.LoaderCallback();
        }

    }
}
