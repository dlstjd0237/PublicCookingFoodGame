using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform _mainCamTrm;
    private float _xRotation = 0f;

    public float XSensitivity = 30f;
    public float YSensitivity = 30f;
    [SerializeField] private float _maxRot = 60f;
    [SerializeField] private InputReader _input;

    private void OnEnable()
    {
        _input.MouseMoveEvent += ProcessLook;
    }
    private void OnDestroy()
    {
        _input.MouseMoveEvent -= ProcessLook;
    }
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        _xRotation -= (mouseY * Time.deltaTime) * YSensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -80f, _maxRot);

        _mainCamTrm.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * XSensitivity);
    }
}
