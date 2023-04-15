using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingScript : MonoBehaviour
{
    public static GameSettingScript instance;
    public MusicSheet Sheet;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        } 
        instance = this;
        DontDestroyOnLoad(this);
    }
}
