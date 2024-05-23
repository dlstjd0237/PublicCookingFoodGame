using UnityEngine;
using System;

[Serializable]
public class Arm
{
    public Transform ArmTrm;
    public Transform HandTrm;
    [HideInInspector] public FoodInteractable GetItem;
    public Vector3 DefulteArmRot;
    public Vector3 MoveArmRot;
}
