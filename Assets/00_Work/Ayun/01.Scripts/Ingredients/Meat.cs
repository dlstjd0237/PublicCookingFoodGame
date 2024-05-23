using System.Collections;
using UnityEngine;

public class Meat : Ingredient
{
    [SerializeField] private float cookingTime = 10f;
    private AudioSource _audio;
    private float _currentTime = 0;

    private bool _isCooing;

    private void Start()
    {
        cookingTime = GameModeManager.Instance.CookingTIme;
        _audio = GetComponent<AudioSource>();
    }

    private void Cook()
    {
        _isCooing = true;
        _audio.Play();
        StartCoroutine(CookingRoutine());
    }

    private IEnumerator CookingRoutine()
    {
        while (_isCooing)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > cookingTime)
            {
                PoolManager.SpawnFromPool("Smoke", transform.position, Quaternion.identity);
                _audio.Stop();
                Cooking();
                break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pan"))
            Cook();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pan"))
        {
            _audio.Stop();
            _isCooing = false;
        }
    }
}
