using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public static SpawnItems s;

    public List<GameObject> _trash;
    [Range(0f, 30f)] [SerializeField] private float SpeedMoved;

    [SerializeField] private Transform _moveObject;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private float Interval;
    private void Start()
    {
        s = GetComponent<SpawnItems>();
    }
    private void Awake()
    {
        StartCoroutine(_spawnLogick());
    }
    private void LateUpdate()
    {
        Vector3 pos = _moveObject.transform.position;
        pos.x -= SpeedMoved;
        _moveObject.transform.position = pos;

        if(_trash.Count > 20)
        {
            for(int i = 0; i < _trash.Count; i -= 1)
            {
                Destroy(_trash[i]);
                _trash.RemoveAt(i);
            }
        }
    }
    private IEnumerator _spawnLogick()
    {
        yield return new WaitForSeconds(Interval);
        GameObject g = Instantiate(_itemPrefab, SpawnPoint.position, Quaternion.identity);
        g.transform.SetParent(_moveObject.transform);

        StartCoroutine(_spawnLogick());
    }
}
