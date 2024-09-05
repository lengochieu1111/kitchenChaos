using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : RyoMonoBehaviour
{
    [SerializeField] private PlatesCounter _platesCounter;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;

    private List<GameObject> _plateVisualGameObjectList = new List<GameObject>();

    protected override void Start()
    {
        base.Start();

        this._platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        this._platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved; ;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = this._plateVisualGameObjectList[this._plateVisualGameObjectList.Count - 1];
        this._plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(this._plateVisualPrefab, this._counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * this._plateVisualGameObjectList.Count, 0);

        this._plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
