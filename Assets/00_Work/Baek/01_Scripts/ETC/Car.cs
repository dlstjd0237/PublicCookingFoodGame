using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Vector3 _dir;
    [SerializeField] private float _speed;
    private Vector3 _startPos;
    private void Awake()
    {
        _startPos = transform.position;
        InvokeRepeating("ReStart", 8, 8);
    }

    private void Update()
    {
        transform.position += _dir * _speed * Time.deltaTime;
    }

    private void ReStart()
    {
        transform.position = _startPos;
    }
}
