using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BeatController : MonoBehaviour
{
    public static BeatController instance;

    public GameObject BeatCircle;
    public GameObject SlashCircle;

    [SerializeField]
    private List<BeatData> Beats;



    public Transform MousePos;

    public float BeatTime = 1f;




    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Beats = new List<BeatData>();
        if (GameEventManager.instance)
        {
            GameEventManager.instance.SpawnBeat.AddListener(SpawnBeat);
            GameEventManager.instance.SpawnSlash.AddListener(SpawnSlash);
        }
        if (GameSettingScript.instance) BeatTime = GameSettingScript.instance.Sheet.BeatTime;

    }

    public void RemoveBeat(GameObject _target)
    {
        if (Beats.Where(x => x.Obj == _target).Count() > 0)
        {

            BeatData rslt = Beats.Where(x => x.Obj == _target).First();

            if (!rslt.IsUsed)
            {
                if (RythmGameManager.instance) RythmGameManager.instance.ShowBeatResult(false, rslt.IsSlash);
                if (GameEventManager.instance) GameEventManager.instance.BeatOver.Invoke();
                if (rslt.IsSlash && GameEventManager.instance) GameEventManager.instance.DamageFalied.Invoke();
            }
            Beats.Remove(rslt);

        }

    }

    public bool BeatCorrect(bool IsSlash)
    {
        if (Beats.Count <= 0) return false;
        if (Beats[0].Obj.transform.localScale.x >= 0.4f) return false;
        bool Rslt = Beats[0].Obj.transform.localScale.x <= 0.3f && Beats[0].IsSlash == IsSlash;
        if (Beats[0].IsSlash && !Rslt && GameEventManager.instance) GameEventManager.instance.DamageFalied.Invoke();
        Beats[0].IsUsed = true;
        Destroy(Beats[0].Obj);
        Beats.RemoveAt(0);
        if (RythmGameManager.instance) RythmGameManager.instance.ShowBeatResult(Rslt, IsSlash);
        if (GameEventManager.instance) GameEventManager.instance.BeatOver.Invoke();
        return Rslt;
    }



    void SpawnBeat()
    {
        Beats.Add(new BeatData(SpawnCircle(BeatCircle), false));
    }

    void SpawnSlash()
    {
       
        Beats.Add(new BeatData(Instantiate(SpawnCircle(SlashCircle), MousePos), true));
    }

    GameObject SpawnCircle(GameObject _target)
    {
        GameObject obj = Instantiate(_target, MousePos);
        if (obj.GetComponent<CircleScript>()) obj.GetComponent<CircleScript>().DeathTime = BeatTime;
        return obj;
    }



}
