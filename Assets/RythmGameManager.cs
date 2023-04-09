using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmGameManager : MonoBehaviour
{
    public MusicSheet musicSheet;
    private AudioSource musicPlayer;

    private List<SheetData> sheetDatas;

    private string Beat_Index;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        PlayAudio();
    }

    public void PlayAudio()
    {
        if (musicSheet && musicPlayer)
        {
            musicPlayer.clip = musicSheet.music;
            sheetDatas = new List<SheetData>();
            sheetDatas.AddRange(musicSheet.sheetDatas);
            musicPlayer.Play();
        }
    }

    private void Update()
    {
        if (musicPlayer.isPlaying && sheetDatas.Count > 0)
        {
            if (sheetDatas[0].position - musicPlayer.time <= 1)
            {
                Beat_Index = sheetDatas[0].ID;
                if (GameEventManager.instance)
                {
                    if (sheetDatas[0].IsEnd)
                        GameEventManager.instance.SpawnSlash.Invoke();
                    else
                        GameEventManager.instance.SpawnBeat.Invoke();
                }
                sheetDatas.RemoveAt(0);

            }
        }
    }

    public void TrimData()
    {
        float prevPos = -1;
        List<SheetData> RemainData = new List<SheetData>(); ;
        foreach (var item in musicSheet.sheetDatas)
        {
            item.position = GetRF(item.position);
            if (prevPos < 0)
            {
                prevPos = item.position;
                RemainData.Add(item);
                continue;
            }

            if (item.position - prevPos > 0.3f)
            {
                prevPos = item.position;
                RemainData.Add(item);
            }
        }
        musicSheet.sheetDatas = new List<SheetData>();
        musicSheet.sheetDatas.AddRange(RemainData);
    }

    public void ConvertData()
    {
        List<SheetData> _group = new List<SheetData>();
        musicSheet.PlayGroup = new List<SheetGroup>();
        int ID = 1;
        foreach (var item in musicSheet.sheetDatas)
        {
            _group.Add(item);
            if (item.IsEnd)
            {
                musicSheet.PlayGroup.Add(new SheetGroup(ID.ToString(), _group));
                _group = new List<SheetData>();
                ID++;
            }
           
        }
    }

    float GetRF(float Target)
    {
        return (float)System.Math.Round((decimal)Target, 2);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 50), Beat_Index);
    }
}
