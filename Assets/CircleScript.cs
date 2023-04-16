using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    public float DeathTime = 1f;
    private bool IsGameOver;
    void Start()
    {
        if (GameEventManager.instance) GameEventManager.instance.GameOver.AddListener(GameOverFunc);
        this.transform.localScale = Vector2.one + new Vector2(0.1f, 0.1f);
        this.transform.LeanScale(Vector2.zero + new Vector2(0.1f, 0.1f), DeathTime);
        Destroy(gameObject, DeathTime);
    }
    void GameOverFunc()
    {
        IsGameOver = true;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        if (BeatController.instance && !IsGameOver) BeatController.instance.RemoveBeat(this.gameObject);
    }
}
