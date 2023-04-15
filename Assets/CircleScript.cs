using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    public float DeathTime = 1f;
    void Start()
    {
        this.transform.localScale = Vector2.one + new Vector2(0.1f, 0.1f);
        this.transform.LeanScale(Vector2.zero + new Vector2(0.1f, 0.1f), DeathTime);
        Destroy(gameObject, DeathTime);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        if (BeatController.instance) BeatController.instance.RemoveBeat(this.gameObject);
    }
}
