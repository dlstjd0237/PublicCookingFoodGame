using UnityEngine;

public class FoodInteract : FoodInteractable
{
    [SerializeField] private Mesh _foodVisual;
    private Rigidbody _rig;
    private Collider _collider;

    protected override void Awake()
    {
        base.Awake();

        _rig = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    public override void PickUpInteract(Arm arm)
    {
        if (arm.GetItem != null) return; //만약 손에 아이템을 들고 있다면?
        base.PickUpInteract(arm);

        arm.GetItem = this;

        _collider.isTrigger = true;

        _rig.constraints = RigidbodyConstraints.FreezeRotation;
        _rig.useGravity = false;

    }
    public override void DorpInteract(Arm arm)
    {
        base.DorpInteract(arm);

        _rig.constraints = RigidbodyConstraints.None;
        _rig.useGravity = true;

        _collider.isTrigger = false;

        arm.GetItem = null;
    }
    private void Update()
    {
        if (_isPickedUp)
        {
            transform.position = Vector3.Lerp(transform.position, _currentArm.HandTrm.position, 1);
            Transform PlayerTrm = _currentArm.ArmTrm.transform.root.transform;
            Quaternion newRotation = Quaternion.Euler(0, PlayerTrm.eulerAngles.y - 110, 0);
            transform.rotation = newRotation;
        }
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        //if (_foodVisual == null) return;

        //MeshFilter visualMeshFilter = GetComponentInChildren<MeshFilter>();
        //BoxCollider coliider = GetComponent<BoxCollider>();

        //visualMeshFilter.mesh = _foodVisual;

        //Vector3 meshSize = _foodVisual.bounds.size;
        //Vector3 meshCenter = _foodVisual.bounds.center;

        //coliider.size = meshSize;
        //coliider.center = meshCenter;
    }
#endif
}

