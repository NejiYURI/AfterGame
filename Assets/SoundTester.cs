using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour
{
    public AudioClip audio;
    public float Freq;
    void Start()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Freq);
            if (AudioController.instance) AudioController.instance.PlaySound(audio);
        }
       
    }
}
