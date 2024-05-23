using UnityEngine;

public class TableServingCheck : MonoBehaviour
{
    [HideInInspector] public FoodType plateFoodType;
    [HideInInspector] public Plate plate;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Plate currentPlate))
        {
            plate = currentPlate;
            plateFoodType = plate.FoodkindCheck();
        }
    }
}
