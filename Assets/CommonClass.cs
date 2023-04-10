using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SheetData
{
    public SheetData(string _id,float _pos,float _beat,bool _isEnd=false)
    {
        ID = _id;
        position = _pos;
        beat = _beat;
        IsEnd = _isEnd;
    }
    public string ID;
    public float position;
    public float beat;
    public bool IsEnd;
}

[System.Serializable]
public class SheetGroup
{
    public SheetGroup(string _id, List<SheetData> datas)
    {
        ID= _id;
        Groups = new List<SheetData>();
        Groups.AddRange(datas);
    }
    public string ID;
    public List<SheetData> Groups;
}

[System.Serializable]
public class BeatData
{
    public BeatData(GameObject obj, bool isSlash)
    {
        this.Obj = obj;
        this.IsSlash = isSlash;
    }

    public GameObject Obj;
    public bool IsSlash;
    public bool IsUsed;
}