using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Rat : MonoBehaviour
{
    private readonly int _walkAnimHash = Animator.StringToHash("Walk");
    private readonly int _idleAnimHash = Animator.StringToHash("Idle");
    
    [SerializeField] private float speed;
    [SerializeField] private Vector3 size;
    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;
    [SerializeField] private LayerMask whatIsIngredient;
    [SerializeField] private Transform ingredientCastTrm;

    [SerializeField] private Transform destinationTrm;
    [SerializeField] private Transform _foodTrm;
    private Vector3 startPos;

    private float _findTime;
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private Collider[] _colliders;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _colliders = new Collider[1];
    }

    private void Start()
    {
        _animator.SetBool(_idleAnimHash, true);
        startPos = transform.position;
        _findTime = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        if (!_animator.GetBool(_walkAnimHash))
            _findTime -= Time.deltaTime;
        
        if (_findTime <= 0)
        {
            StartCoroutine(FindRoutine());
        }
    }

    private IEnumerator FindRoutine()
    {
        _findTime = Random.Range(minTime, maxTime);

        _animator.SetBool(_idleAnimHash, false);
        _animator.SetBool(_walkAnimHash, true);
        
        _navMeshAgent.SetDestination(destinationTrm.position);
        while (true)
        {
            bool foodCheck = FindIngredient() >= 1;
            if (foodCheck)
            {
                _navMeshAgent.SetDestination(_colliders[0].transform.position);
                if (_colliders[0].isTrigger)
                {
                    StartCoroutine(ReturnRoutine());
                    yield break;
                }
            }
            
            if (Vector3.Distance(transform.position, _navMeshAgent.destination) < 0.25f)
            {
                StartCoroutine(ReturnRoutine());
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator ReturnRoutine()
    {
        _navMeshAgent.SetDestination(startPos);
        while (true)
        {
            if (Vector3.Distance(transform.position, _navMeshAgent.destination) < 0.5f)
            {
                _animator.SetBool(_idleAnimHash, true);
                _animator.SetBool(_walkAnimHash, false);
                if (_foodTrm.childCount > 0)
                {
                    Transform currentFoodTrm = _foodTrm.GetChild(0);
                    currentFoodTrm.SetParent(PoolManager.Instance.transform);
                    currentFoodTrm.gameObject.SetActive(false);
                }
                break;
            }

            yield return null;
        }
    }

    private int FindIngredient()
    {
        return Physics.OverlapBoxNonAlloc(ingredientCastTrm.position, size, _colliders, transform.rotation, whatIsIngredient);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider == _colliders[0])
        {
            other.transform.position = _foodTrm.position;
            other.transform.SetParent(_foodTrm);
            other.transform.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ingredientCastTrm.position, size);
    }
}
