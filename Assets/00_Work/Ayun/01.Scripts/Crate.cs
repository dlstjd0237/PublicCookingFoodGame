using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;

    private Transform _spawnTrm;
//q
    private void Awake()
    {
        _spawnTrm = transform.Find("SpawnTrm").GetComponent<Transform>();
    }


    public void IngredientSpawn() // �÷��̾� Ŭ�� �ϴ°Ŷ� ������
    {
        GameObject qwer = PoolManager.SpawnFromPool(spawnPrefab.name, _spawnTrm.position);
    }
}
