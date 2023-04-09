using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    public GameObject BeatCircle;
    public GameObject SlashCircle;
    private void Start()
    {
        if (GameEventManager.instance)
        {
            GameEventManager.instance.SpawnBeat.AddListener(SpawnBeat);
            GameEventManager.instance.SpawnSlash.AddListener(SpawnSlash);
        }
    }

    void SpawnBeat()
    {
        Instantiate(BeatCircle, this.transform.position, Quaternion.identity);
    }

    void SpawnSlash()
    {
        Instantiate(SlashCircle, this.transform.position, Quaternion.identity);
    }
}
