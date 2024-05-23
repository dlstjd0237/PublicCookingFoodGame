using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bun : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsIngredient;

    private int _distance = 1;

    private Vector3 _dir;
    private Transform _rayPos;
    private CookedIngredient _cookedIngredient;

    private void Awake()
    {
        _cookedIngredient = GetComponent<CookedIngredient>();
        _rayPos = transform.Find("RayPos").GetComponent<Transform>();
    }

    private void Start()
    {
        if (_cookedIngredient.ingredientType == IngredientChildType.Bun_Bottom)
            _dir = Vector3.down;
        else if (_cookedIngredient.ingredientType == IngredientChildType.Bun_Top)
            _dir = Vector3.up;
    }

    private void Update()
    {
        if (_cookedIngredient.ingredientType == IngredientChildType.Bun_Top)
            Debug.DrawRay(_rayPos.position, _dir);
    }

    public bool PerfactBurgerCheck()
    {
        return !Physics.Raycast(_rayPos.position, _dir, _distance, whatIsIngredient);
    }
}
