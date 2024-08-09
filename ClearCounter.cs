using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : RyoMonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kischenObjectSO;
    [SerializeField] private Transform _counterTopPoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._counterTopPoint  == null)
        {
            this._counterTopPoint = this.transform.Find("CounterTopPoint");
        }
    }

    public void Interact()
    {
/*        Transform tomato = KitchenObjectSpawner.Instance.Spawn(KitchenObjectSpawner.Tomato, this._counterTopPoint.position, Quaternion.identity);
        tomato.localPosition = Vector3.zero;
        tomato.gameObject.SetActive(true);*/

        Transform kitchenObjectTransform = Instantiate(this._kischenObjectSO.prefab, _counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);

    }
}
