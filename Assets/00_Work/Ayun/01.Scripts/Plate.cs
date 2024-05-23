using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private FoodType _foodType;

    private int _foodSum = 0;

    private void Awake()
    {
        GameModeManager.Instance.Money = 0;
    }
    private void Update() // 테스트용
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    FoodkindCheck();
        //}
    }

    public FoodType FoodkindCheck() // 이걸로 어떤 음식인지 체크하면 됨
    {
        _foodSum = 0;

        for (int i = 0; i < transform.childCount; ++i)
        {
            CookedIngredient cookedIngredient = transform.GetChild(i).GetComponent<CookedIngredient>();

            _foodSum += (int)cookedIngredient.ingredientType;
        }

        switch (_foodSum)
        {
            case (int)FoodType.Burger:
                _foodType = BurgerCheck();
                break;
            case (int)FoodType.Ham:
                _foodType = FoodType.Ham;
                break;
            default:
                _foodType = FoodType.Failed;
                break;
        }

        return _foodType;
    }

    private FoodType BurgerCheck()
    {
        int cnt = 0;
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).TryGetComponent(out Bun bun))
            {
                if (bun.PerfactBurgerCheck())
                    cnt++;
            }
        }

        if (cnt == 2)
            return FoodType.Burger;

        return FoodType.Failed;
    }

    public void DestroyFood() // 음식 먹었을 때 이거 실행
    {
        switch (_foodType)
        {
            case FoodType.Failed:
                break;
            case FoodType.Burger:
                GameModeManager.Instance.Money += Random.Range(25, 59);
                break;
            case FoodType.Ham:
                GameModeManager.Instance.Money += Random.Range(13, 37);
                break;
        }

        //foreach (Transform item in transform)
        //{
        //    item.transform.parent = PoolManager.Instance.transform;
        //    item.gameObject.SetActive(false);
        //}

        for (int i = 0; i < transform.childCount; ++i)
        {

            var child = transform.GetChild(i).gameObject;
            Destroy(child);
            //child.transform.parent = PoolManager.Instance.transform;
            //child.gameObject.SetActive(false);
        }
        _foodType = FoodType.Failed;
    }
}
