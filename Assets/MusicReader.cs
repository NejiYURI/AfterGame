using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicReader : MonoBehaviour
{
    private AudioSource musicSource;
    public AudioClip clip;
    public Transform MoveObj;
    public GameObject spawnSheetPoint;
    public Transform Obj_1;
    public Transform Obj_2;

    public float minRms;

    public float MaxRms;

    private int BeatCounter;

    [SerializeField]
    private float LastRms;
    [SerializeField]
    private float LastPos;
    [SerializeField]
    private List<SheetData> sheets;
    [SerializeField]
    private float[] samples_L;
    [SerializeField]
    private float[] samples_R;
    [SerializeField]
    private float Rms_L;
    [SerializeField]
    private float Rms_R;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        if (musicSource)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        samples_L = new float[1024];
        samples_R = new float[1024];
        sheets = new List<SheetData>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (musicSource && musicSource.isPlaying)
        //{
        //    musicSource.GetOutputData(samples_L, 0);
        //    musicSource.GetOutputData(samples_R, 1);
        //    float sum_L = 0, sum_R = 0;
        //    for (int i = 0; i < 1024; i++)
        //    {
        //        sum_L += samples_L[i] * samples_L[i];
        //        sum_R += samples_R[i] * samples_R[i];
        //    }
        //    Rms_L = Mathf.Sqrt(sum_L / 1024);
        //    Rms_R = Mathf.Sqrt(sum_R / 1024);
        //    if (Obj_1 != null) Obj_1.localScale = new Vector3(Rms_L * 10f, Rms_L * 10f, Rms_L * 10f);
        //    if (Obj_2 != null) Obj_2.localScale = new Vector3(Rms_R * 10f, Rms_R * 10f, Rms_R * 10f);
        //    MoveObj.position = new Vector2(musicSource.time, 1f);
        //    if (!WaitForLow && (GetRF(Rms_L) >= MaxRms && GetRF(Rms_R) >= MaxRms))
        //    {
        //        WaitForLow = true;
        //        LastRms = GetRF(Rms_R > Rms_L ? Rms_R : Rms_L);
        //        sheets.Add(new SheetData(BeatCounter.ToString(), musicSource.time, Rms_R > Rms_L ? Rms_R : Rms_L));
        //        BeatCounter++;
        //        Instantiate(spawnSheetPoint, (Vector2)MoveObj.position - new Vector2(0, -2), Quaternion.identity);
        //    }
        //    else if (WaitForLow && (LastRms - GetRF(Rms_R) >= minRms) && (LastRms - GetRF(Rms_L) >= minRms))
        //    {
        //        WaitForLow = false;
        //    }

        //}
    }

    public void LeftPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sheets.Add(new SheetData(BeatCounter.ToString(), GetRF(musicSource.time), 0));
            BeatCounter++;
        }
    }

    public void RightPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sheets.Add(new SheetData(BeatCounter.ToString(), GetRF(musicSource.time), 0, true));
            BeatCounter++;
        }
    }

    public void CreateFile()
    {
        if (sheets.Count > 0)
        {
            MusicSheet newFile = ScriptableObject.CreateInstance<MusicSheet>();
            newFile.sheetDatas = new List<SheetData>();
            newFile.sheetDatas.AddRange(sheets);
#if UNITY_EDITOR
            var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath("Assets/NewSheet.asset");
            AssetDatabase.CreateAsset(newFile, uniqueFileName);
            //EditorUtility.SetDirty(newFile);
            AssetDatabase.SaveAssets();
#endif
        }
    }

    float GetRF(float Target)
    {
        return (float)System.Math.Round((decimal)Target, 2);
    }
    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(0.4f);
    }
}
