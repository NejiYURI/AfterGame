using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObj : MonoBehaviour
{
    private void Start()
    {
        if (GameEventManager.instance) GameEventManager.instance.GameOver.AddListener(GameOverFunc);
    }

    void Update()
    {
        this.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void GameOverFunc()
    {
       this.gameObject.SetActive(false);
    }
}
