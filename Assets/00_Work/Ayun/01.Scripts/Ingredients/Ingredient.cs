using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] protected GameObject[] cookedIngredients; // ������ ������Ʈ
    public bool isCooked = false; // ���� �Ǿ�����

    private void OnDisable()
    {
        PoolManager.ReturnToPool(this.gameObject);
    }

    protected void DisableThisGameobject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Cooking(bool isDestroy = true)
    {
        foreach(GameObject obj in cookedIngredients)
        {
            PoolManager.SpawnFromPool(obj.name, transform.position, Quaternion.identity);
        }

        if (isDestroy)
        {
            isCooked = true;
            StopAllCoroutines();
            DisableThisGameobject();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TrashCan"))
            DisableThisGameobject();
    }
}
