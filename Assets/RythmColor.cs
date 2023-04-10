using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmColor : MonoBehaviour
{
    public Color minColor;
    public Color maxColor;

    public Camera cam;
    void Update()
    {
        if (MusicInteractContoller.instance)
        {
            cam.backgroundColor = Color.Lerp(minColor,maxColor, MusicInteractContoller.instance.GetRatio());
        }
    }
}
