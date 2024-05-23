using UnityEngine;

public class ReturnPool : MonoBehaviour
{
    private void OnDisable()
    {
        PoolManager.ReturnToPool(this.gameObject);
    }
}
