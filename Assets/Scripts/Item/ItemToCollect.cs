using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToCollect : MonoBehaviour
{
    [HideInInspector] public int ProductNumber;
    [HideInInspector] public CollectLogics _colecter;

    private bool isColected;

    [SerializeField] private GameObject[] _views;
    private void Awake()
    {
        ProductNumber = Random.Range(0, _views.Length);
        _views[ProductNumber].SetActive(true);

        _colecter = FindObjectOfType<CollectLogics>();
    }
    private void FixedUpdate()
    {
        if(this.transform.position.x < -5)
        {
            SpawnItems.s._trash.Add(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (isColected) return;
        isColected = true;

        Quests.q._kash.Add(this.gameObject);
        Quests.q.AddItem(ProductNumber);

        Debug.Log("ColectThisItem");
        _colecter.TransferPurposeObject(this.transform);
    }
}
