using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AngryCutScene : MonoBehaviour
{
    private Transform setNpc;
    private Transform geometry;
    private Animator _animator;

    private void Awake()
    {
        setNpc = transform.Find("NPC");
        geometry = setNpc.Find("Visual/Geometry");
        _animator = setNpc.GetComponentInChildren<Animator>();
    }
    
    public void Routine()
    {
        StartCoroutine(ReceiverFeedback());
    }

    private IEnumerator ReceiverFeedback()
    {
        NpcSpawner.Instance.StopSpawn();
        int random = Random.Range(0, geometry.childCount);
        geometry.GetChild(random).gameObject.SetActive(true);
        
        Npc[] npcs = PoolManager.Instance.transform.GetComponentsInChildren<Npc>();
        foreach (var npc in npcs)
        {
            npc.gameObject.SetActive(false);
        }
        setNpc.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        _animator.SetBool("Angry", true);
    }
}
