using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmMovement : MonoBehaviour
{
    public float minScale = 0.8f;
    public float MaxScale = 1.2f;

    // Update is called once per frame
    void Update()
    {
        if (MusicInteractContoller.instance)
        {
            this.transform.localScale = new Vector2(Mathf.Lerp(minScale, MaxScale, MusicInteractContoller.instance.GetRatio()), Mathf.Lerp(minScale,MaxScale, MusicInteractContoller.instance.GetRatio()));
        }
    }
}
