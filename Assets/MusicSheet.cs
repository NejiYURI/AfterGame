using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSheet : ScriptableObject
{
    public string MusicName;
    public AudioClip music;
    public List<SheetData> sheetDatas;

    public List<SheetGroup> PlayGroup;
}
