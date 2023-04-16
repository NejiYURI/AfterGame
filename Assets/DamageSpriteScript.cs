using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpriteScript : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer) spriteRenderer.color = startColor;
        if (GameEventManager.instance) GameEventManager.instance.PlayerDamage.AddListener(StartSprite);
    }

    void StartSprite()
    {
        StartCoroutine(FadeTimer());
    }

    IEnumerator FadeTimer()
    {
        float timer = 0.5f;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (spriteRenderer) spriteRenderer.color = Color.Lerp(startColor, endColor, timer / 0.5f);
            yield return null;
        }

    }
}
