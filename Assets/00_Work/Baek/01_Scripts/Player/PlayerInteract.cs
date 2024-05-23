using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputReader _inputRader;
    [SerializeField] private Image _playerUI;
    [SerializeField] private LayerMask WahtIsItem;
    private Transform _cam;
    private int _interactLayer;
    private Player _player;
    private float _distance = 2;

    [SerializeField] private Arm _rightArm, _leftArm;
    [SerializeField] private Color _defultColor, _interactColor;
    private void Awake()
    {
        _player = GetComponent<Player>();
        _cam = Camera.main.transform;
        _distance = _player.Distance;
        _interactLayer = LayerMask.NameToLayer("Ingredient");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        #region 이벤트 구독

        _inputRader.LeftMouseDownEvent += HandleLeftMouseDown;
        _inputRader.RightMouseDownEvent += HandleRightMouseDown;
        _inputRader.LeftMouseUpEvent += HandleLeftMouseUp;
        _inputRader.RightMouseUpEvent += HandleRightMouseUp;

        #endregion
    }
    private void OnDestroy()
    {
        #region 이벤트 해지

        _inputRader.LeftMouseDownEvent -= HandleLeftMouseDown;
        _inputRader.RightMouseDownEvent -= HandleRightMouseDown;
        _inputRader.LeftMouseUpEvent -= HandleLeftMouseUp;
        _inputRader.RightMouseUpEvent -= HandleRightMouseUp;

        #endregion
    }

    void Update()
    {

    }
    private void LateUpdate()
    {
        Ray _ray = new Ray(_cam.transform.position, _cam.transform.forward);

        bool isHit = Physics.Raycast(_ray, out RaycastHit _hitInfo, _distance,  WahtIsItem);

        if (isHit)
        {

                _playerUI.color = _interactColor;
            
        
        }
        else
        {
            _playerUI.color = _defultColor;
        }
    }

    private void HandleLeftMouseDown() => HandleMouseDown(_leftArm);

    private void HandleRightMouseDown() => HandleMouseDown(_rightArm);


    private void HandleMouseDown(Arm arm)
    {
        arm.ArmTrm.DOLocalRotate(arm.MoveArmRot, 0.2f);

        Ray _ray = new Ray(_cam.transform.position, _cam.transform.forward);


        bool isHit = Physics.Raycast(_ray, out RaycastHit _hitInfo, _distance, WahtIsItem);

        if (isHit)
        {
            if (_hitInfo.collider.TryGetComponent<Crate>(out Crate crate))
            {
                crate.IngredientSpawn();
            }

            if (_hitInfo.collider.TryGetComponent<FoodInteractable>(out FoodInteractable _interactable))
            {
                _interactable.PickUp(arm);
            }

        }

    }

    private void HandleLeftMouseUp() => HandleMouseUp(_leftArm);

    private void HandleRightMouseUp() => HandleMouseUp(_rightArm);

    private void HandleMouseUp(Arm arm)
    {
        arm.ArmTrm.DOLocalRotate(arm.DefulteArmRot, 0.2f);
        if (arm.GetItem == null) return; //아이템을 안들고 있다면 리턴  

        arm.GetItem.Drop(arm);
    }
}
