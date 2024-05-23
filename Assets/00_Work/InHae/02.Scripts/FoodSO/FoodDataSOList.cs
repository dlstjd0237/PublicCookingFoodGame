using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Food/FoodDataList")]
public class FoodDataSOList : ScriptableObject
{
    public List<FoodDataSO> foodDataSOList = new();
}
