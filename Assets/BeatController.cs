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

    }

    public void RemoveBeat(GameObject _target)
    {
        if (Beats.Where(x => x.Obj == _target).Count() > 0)
        {

            BeatData rslt = Beats.Where(x => x.Obj == _target).First();

            if (!rslt.IsUsed)
            {
                Debug.Log("Miss");
                if (RythmGameManager.instance) RythmGameManager.instance.ShowBeatResult(false);
                if (GameEventManager.instance) GameEventManager.instance.BeatOver.Invoke();
                if (rslt.IsSlash && GameEventManager.instance) GameEventManager.instance.DamageFalied.Invoke();
            }
            Beats.Remove(rslt);

        }

    }

    public bool BeatCorrect(bool IsSlash)
    {
        if (Beats.Count <= 0) return false;
        if (Beats[0].Obj.transform.localScale.x >= 0.6f) return false;
        bool Rslt = Beats[0].Obj.transform.localScale.x <= 0.25f && Beats[0].IsSlash == IsSlash;
        if (Beats[0].IsSlash && !Rslt && GameEventManager.instance) GameEventManager.instance.DamageFalied.Invoke();
        Beats[0].IsUsed = true;
        Destroy(Beats[0].Obj);
        Beats.RemoveAt(0);
        if (RythmGameManager.instance) RythmGameManager.instance.ShowBeatResult(Rslt);
        if (GameEventManager.instance) GameEventManager.instance.BeatOver.Invoke();
        return Rslt;
    }



    void SpawnBeat()
    {
        Beats.Add(new BeatData(Instantiate(BeatCircle, MousePos), false));
    }

    void SpawnSlash()
    {
        Beats.Add(new BeatData(Instantiate(SlashCircle, MousePos), true));
    }
}
