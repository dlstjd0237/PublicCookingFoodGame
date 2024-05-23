using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameModeManager : MonoSingleton<GameModeManager>
{
    private ChapterLevel _currentChapterLevel;
    public ChapterLevel CurrentChapterLevel { get => _currentChapterLevel; set => _currentChapterLevel = value; }
    public event Action HappeyEndingEvent;

    public Dictionary<ChapterLevel, float> BestTimeDictionary;
    private int _money = 0;
    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            if (_money >= 1000)
            {
                HappeyEndingEvent?.Invoke(); //±Þ»ó½Â
            }
        }
    }


    private void Awake()
    {
        BestTimeDictionary = new Dictionary<ChapterLevel, float>();

        foreach (ChapterLevel item in Enum.GetValues(typeof(ChapterLevel)))
        {
            string keyName = $"{item.ToString()}_Score";
            if (PlayerPrefs.HasKey(keyName) == true)
                BestTimeDictionary.Add(item, PlayerPrefs.GetFloat(keyName));
            else
                BestTimeDictionary.Add(item, 0);
        }
    }

    public void SaveTime(float time)
    {
        if (BestTimeDictionary[_currentChapterLevel] < time)
        {
            PlayerPrefs.SetFloat($"{_currentChapterLevel.ToString()}_Score", time);
            BestTimeDictionary[_currentChapterLevel] = time;
        }
    }

    public string GetChapterScore(ChapterLevel level)
    {
        string score = $"{(int)(BestTimeDictionary[level] / 60)}:{(int)(BestTimeDictionary[level] % 60)}";
        return score;
    }


    private float _spawnDelay = 5;

    public float SpawnDelay
    {
        get => _spawnDelay;
        set => _spawnDelay = value;
    }

    private float _waitTime = 60;
    public float WaitTime
    {
        get => _waitTime;
        set => _spawnDelay = value;
    }

    private float _cookingTime;
    public float CookingTIme
    {
        get => _cookingTime;
        set => _cookingTime = value;
    }

    public float SpawnDelayInit()
    {
        switch (CurrentChapterLevel)
        {
            case ChapterLevel.Tutorial:
                _spawnDelay = 25;
                _cookingTime = 5;
                _waitTime = 90;
                break;
            case ChapterLevel.Easy:
                _spawnDelay = 25;
                _cookingTime = 4;
                _waitTime = 80;
                break;
            case ChapterLevel.Normal:
                _spawnDelay = 25;
                _cookingTime = 3;
                _waitTime = 65;
                break;
            case ChapterLevel.Hard:
                _spawnDelay = 20;
                _cookingTime = 2.5f;
                _waitTime = 55;
                break;
            case ChapterLevel.Hell:
                _spawnDelay = 15;
                _cookingTime = 2;
                _waitTime = 40;
                break;
        }
        return _spawnDelay;
    }


}
