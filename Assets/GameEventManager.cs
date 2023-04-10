using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;

    private void Awake()
    {
        instance = this;
    }
    public UnityEvent<bool> ActionStart;

    public UnityEvent DealDamage;
    public UnityEvent DamageFalied;

    public UnityEvent SpawnBeat;

    public UnityEvent SpawnSlash;

    public UnityEvent EnemyDead;

    public UnityEvent BeatOver;
}
