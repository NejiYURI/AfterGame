using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public List<MusicSheet> musicSheets;

    private Dictionary<string, MusicSheet> sheetDictionary;
    private void Start()
    {
        sheetDictionary=new Dictionary<string, MusicSheet>();
        foreach (var item in musicSheets)
        {
            sheetDictionary.Add(item.ID, item);
        }
    }
    public void LoadScene(string MusicID)
    {
        if (sheetDictionary.ContainsKey(MusicID))
        {
            if (GameSettingScript.instance) GameSettingScript.instance.Sheet = sheetDictionary[MusicID];
            SceneManager.LoadScene("RythmGame");
        }
        
    }
}
