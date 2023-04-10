using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = Vector2.one;
        this.transform.LeanScale(Vector2.zero, 1f);
        Destroy(gameObject,1f);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        if (BeatController.instance) BeatController.instance.RemoveBeat(this.gameObject);
    }
}
