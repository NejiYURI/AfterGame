using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheet : ScriptableObject
{
    public string ID;
    public string MusicName;
    public string Composer;
    public float SpawnFreq=1f;
    public int StartSpawn=3;
    public float BeatTime=1f;
    public Color BKminColor = Color.gray;
    public Color BKmaxColor = Color.gray;
    public AudioClip music;
    public List<SheetData> sheetDatas;

    public List<SheetGroup> PlayGroup;
}
