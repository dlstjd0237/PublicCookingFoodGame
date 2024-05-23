using System;
using UnityEngine;
public abstract class FoodInteractable : MonoBehaviour
{
    protected bool _isPickedUp = false;
    public bool IsPickedUp => _isPickedUp;
    protected Arm _currentArm;
    int _interactLayerHash;
    int _defaultLayerHash;
    protected virtual void Awake()
    {
        _interactLayerHash = LayerMask.NameToLayer("Ingredient");
        _defaultLayerHash = LayerMask.NameToLayer("Default");
    }
    public void PickUp(Arm arm)
    {
        _currentArm = arm;
        PickUpInteract(arm);
    }

    public void Drop(Arm arm)
    {
        DorpInteract(arm);
    }

    public virtual void DorpInteract(Arm arm)
    {
        _isPickedUp = false;
        //gameObject.layer = _interactLayerHash;
    }

    public virtual void PickUpInteract(Arm arm)
    {
        _isPickedUp = true;
        //gameObject.layer = _defaultLayerHash;
    }
}
