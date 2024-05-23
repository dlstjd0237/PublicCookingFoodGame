using UnityEngine;

public class CookedIngredient : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlate;

    public IngredientChildType ingredientType;
    private bool isRatTouch;

    private void Update()
    {
        CheckArrangeIngredient();
    }

    private void OnDisable()
    {
        isRatTouch = false;
        PoolManager.ReturnToPool(this.gameObject);
    }

    void CheckArrangeIngredient() // ��ᰡ �׸� ���� ��ġ �Ǿ� �ִ���
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        bool isHit = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, whatIsPlate);

        if (isHit)
            transform.SetParent(hit.transform);
        else if(!isRatTouch)
            transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TrashCan"))
            gameObject.SetActive(false);

        if (collision.collider.CompareTag("Rat"))
            isRatTouch = true;
    }
}
