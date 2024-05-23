using System;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum NpcStateEnum
{
    Enter,
    Sit,
    Stand,
    Exit,
}

public class Npc : MonoBehaviour
{
    #region 컴포넌트
    public Animator AnimationCompo { get; protected set; }
    public NavMeshAgent NavMeshAgentCompo { get; protected set; }
    public CapsuleCollider CapsuleColliderCompo { get; protected set; }
    public Rigidbody RigidbodyCompo { get; protected set; }
    public Transform WaitBarTrm { get; protected set; }
    #endregion
    public NpcStateMachine StateMachine { get; protected set; }

    [HideInInspector] public int sitIndex;
    [HideInInspector] public Transform placeTrm;

    [Header("Sit Collider Value")] 
    [SerializeField] private Vector3 sitCenter;
    [SerializeField] private Vector3 sitVisualOffset;
    [SerializeField] private float sitHeight;
    private Vector3 _defaultSitCenter;
    private float _defaultHeight;

    public float waitTime;
    public float failDamage;

    private Transform _visual;
    public Transform Visual => _visual;
    
    private Transform _geometry;
    private Transform _currentVisual;

    [SerializeField] private FoodDataSOList _npcFoodList;
    [HideInInspector] public FoodType _npcFood;

    private Material _foodImageMat;
    
    private void Awake()
    {
        WaitBarTrm = transform.Find("WaitBarTrm");
        _visual = transform.Find("Visual");
        _geometry = _visual.Find("Geometry");

        _foodImageMat = WaitBarTrm.Find("FoodImage").GetComponent<MeshRenderer>().material;
        AnimationCompo = _visual.GetComponent<Animator>();
        NavMeshAgentCompo = GetComponent<NavMeshAgent>();
        RigidbodyCompo = GetComponent<Rigidbody>();
        CapsuleColliderCompo = GetComponent<CapsuleCollider>();
        
        _defaultHeight = CapsuleColliderCompo.height;
        _defaultSitCenter = CapsuleColliderCompo.center;
        
        StateMachine = new NpcStateMachine();

        foreach (NpcStateEnum stateEnum in Enum.GetValues(typeof(NpcStateEnum)))
        {
            string typeName = stateEnum.ToString();

            try
            {
                Type t = Type.GetType($"Npc{stateEnum}State");
                NpcState state = Activator.CreateInstance(t, this, StateMachine, typeName) as NpcState;
                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception e)
            {
                Debug.LogError($"{typeName} is loading error! check Message");
                Debug.LogError(e.Message);
            }
        }
        waitTime = GameModeManager.Instance.WaitTime;
    }

    private void OnEnable()
    {
        if (_currentVisual == null)
        {
            int random = Random.Range(0, _geometry.childCount);
            _visual.localRotation = Quaternion.Euler(Vector3.zero);
            _currentVisual = _geometry.GetChild(random);
            _currentVisual.gameObject.SetActive(true);
            StateMachine.Init(NpcStateEnum.Enter);
        }
    }

    private void OnDisable()
    {
        PoolManager.ReturnToPool(gameObject);
        CancelInvoke();
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public void SetMovement(Vector3 pos)
    {
        NavMeshAgentCompo.SetDestination(pos);
    }

    public void StopImmediately(bool isStop)
    {
        NavMeshAgentCompo.isStopped = isStop;
    }

    public void SitSetCompo()
    {
        StopImmediately(true);
        NavMeshAgentCompo.enabled = false;

        CapsuleColliderCompo.height = sitHeight;
        CapsuleColliderCompo.center = sitCenter;
        
        transform.position = placeTrm.Find("Chair/SitPos").position;
        RigidbodyCompo.isKinematic = false;
        
        _visual.localPosition = Vector3.zero;
        _visual.localPosition += sitVisualOffset;
        _visual.localRotation = Quaternion.Euler(Vector3.zero);
        transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
    }

    public void StandSetCompo()
    {
        NavMeshAgentCompo.enabled = true;
        StopImmediately(false);
        
        RigidbodyCompo.isKinematic = true;
        CapsuleColliderCompo.height = _defaultHeight;
        CapsuleColliderCompo.center = _defaultSitCenter;
        
        _visual.localRotation = Quaternion.Euler(Vector3.zero);
        _visual.localPosition = Vector3.zero;
    }
    
    public void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public void VisualInit()
    {
        _currentVisual.gameObject.SetActive(false);
        _currentVisual = null;
    }

    public void RandomFoodPick()
    {
        int random = Random.Range(0, _npcFoodList.foodDataSOList.Count);
        FoodDataSO randomFood = _npcFoodList.foodDataSOList[random];
        _npcFood = randomFood.foodType;
        _foodImageMat.mainTexture = randomFood.foodImage;
    }
}
