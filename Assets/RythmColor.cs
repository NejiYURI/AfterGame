using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmColor : MonoBehaviour
{
    public Color minColor;
    public Color maxColor;
    public float minSize=5;
    public float MaxSIze=5;

    public Camera cam;
    private void Start()
    {
        if (GameSettingScript.instance)
        {
            minColor = GameSettingScript.instance.Sheet.BKminColor;
            maxColor= GameSettingScript.instance.Sheet.BKmaxColor;
            cam.backgroundColor = minColor;
        }
    }
    void Update()
    {
        if (MusicInteractContoller.instance)
        {
            cam.backgroundColor = Color.Lerp(minColor,maxColor, MusicInteractContoller.instance.GetRatio());
            cam.orthographicSize = Mathf.Lerp(minSize, MaxSIze, MusicInteractContoller.instance.GetRatio());
        }
    }
}
