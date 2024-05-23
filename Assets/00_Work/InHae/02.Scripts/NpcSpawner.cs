using System;
using UnityEngine;

public class NpcSpawner : MonoSingleton<NpcSpawner>
{
    [SerializeField] private float spawnDelay=> GameModeManager.Instance.SpawnDelayInit();
    private float _currentTime;
    private bool isStop;
    private Transform _spawnTrm;
    private NpcManager _npcManager;

    private void Awake()
    {
        _spawnTrm = transform.Find("SpawnPoint");


    }

    private void Start()
    {
        // PoolManager.SpawnFromPool("NPC", _spawnTrm.position);
        _npcManager = FindAnyObjectByType<NpcManager>();
    }

    public void StartSpawn()
    {
        isStop = false;
    }

    public void StopSpawn()
    {
        isStop = true;
    }

    private void Update()
    {
        if(isStop)
            return;
        _currentTime -= Time.deltaTime;
        if (_npcManager.NoRemainList())
        {
            return;
        }

        if (_currentTime <= 0)
        {
            PoolManager.SpawnFromPool("NPC", _spawnTrm.position);
            _currentTime = spawnDelay;
        }

    }
}
