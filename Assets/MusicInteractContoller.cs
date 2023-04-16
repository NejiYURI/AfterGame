using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInteractContoller : MonoBehaviour
{
    public static MusicInteractContoller instance;
    private void Awake()
    {
        instance = this;
    }

    public AudioSource musicSource;

    public float offset = 0f;
    [SerializeField]
    private float[] samples;
    [SerializeField]
    private float Ratio;

    void Start()
    {
        samples = new float[1024];
    }

    private void Update()
    {
        if (musicSource && musicSource.isPlaying)
        {
            musicSource.GetOutputData(samples, 0);
            float sum = 0;
            for (int i = 0; i < 1024; i++)
            {
                sum += samples[i] * samples[i];
            }
            Ratio = Mathf.Clamp(Mathf.Sqrt((sum+ offset) / 1024)*10f,0f,1f);
        }
    }

    float GetRF(float Target)
    {
        return (float)System.Math.Round((decimal)Target, 4);
    }

    public float GetRatio()
    {
        return GetRF(Ratio);
    }
}
