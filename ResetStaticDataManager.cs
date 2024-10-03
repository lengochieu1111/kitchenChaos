using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : RyoMonoBehaviour
{

    protected override void Awake()
    {
        base.Awake();

        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();

    }

}
