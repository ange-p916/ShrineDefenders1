using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;

    public delegate void StartCountdownTimer();
    public event StartCountdownTimer onStartCountdown;

    public delegate void SpawnEnemies();
    public event SpawnEnemies onSpawnEnemies;

    public delegate void OnLevelCleared();
    public event OnLevelCleared onLevelCleared;

    private void Awake()
    {
        instance = this;
    }

    public void startCountdown()
    {
        onStartCountdown?.Invoke();
    }
    public void startSpawning()
    {
        onSpawnEnemies?.Invoke();
    }
    public void levelCleared()
    {
        onLevelCleared?.Invoke();
    }
}
