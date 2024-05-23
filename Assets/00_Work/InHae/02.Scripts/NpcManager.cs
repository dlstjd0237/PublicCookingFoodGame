using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] private Transform sits;
    private List<int> _randomIndexList = new List<int>();
    [HideInInspector] public List<Transform> _sitTrmList = new List<Transform>();
    private Transform _exitTrm;
    public Transform ExitTrm => _exitTrm;


    private void Awake()
    {
        Initialized();
    }
    public void Initialized()
    {
        _sitTrmList.Clear();
        _exitTrm = transform.Find("ExitTrm");
        for (int i = 0; i < sits.childCount; ++i)
        {
            _sitTrmList.Add(sits.GetChild(i));
            _randomIndexList.Add(i);
        }
    }

    public int RandomNumPick()
    {
        int randomIndex = Random.Range(0, _randomIndexList.Count);
        int randomItem = _randomIndexList[randomIndex];
        _randomIndexList.RemoveAt(randomIndex);

        return randomItem;
    }

    public void AddList(int random)
    {
        _randomIndexList.Add(random);
    }

    public bool NoRemainList()
    {
        return _randomIndexList.Count < 1;
    }
}

