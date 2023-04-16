using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : TutorialStateMachine
{
    public Transform PlayrtCharacter;
    public Transform MouseObj;
    public GameObject enemy;

    public Vector2 ResetPos;

    public Canvas MainCanvas;
    public GameObject BeatRsltLabel;

    public AudioClip TutorialMusic;

    public TextMeshProUGUI DescTxt;
    public TextMeshProUGUI CounterTxt;

    private int EnemySlashedCount;

    private RectTransform CanvasRect;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (MainCanvas) CanvasRect = MainCanvas.GetComponent<RectTransform>();
        if (audioSource)
        {
            audioSource.clip = TutorialMusic;
            audioSource.Play();
        }
        if (GameEventManager.instance)
        {
            GameEventManager.instance.BeatResult.AddListener(BeatResult);
            GameEventManager.instance.EnemySlashed.AddListener(EnemySlashed);
        }
        SetCounter("");
        EnemySlashedCount = 0;
        SetState(new MoveTutorial(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToTitle();
        }
    }

    public void SetDesc(string content)
    {
        if (DescTxt) DescTxt.text = content;
    }

    public void SetCounter(string content)
    {
        if (CounterTxt)
        {
            CounterTxt.text = content;
        }
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void BeatResult(bool IsSuccess, bool IsSlash)
    {
        if (!IsSuccess && GameEventManager.instance) GameEventManager.instance.BeatMiss.Invoke();
        if (IsSuccess)
        {
            State.BeatSuccess(IsSlash);
            if (IsSlash)
            {
                if (GameEventManager.instance) GameEventManager.instance.DamageAction.Invoke(EnemySlashedCount > 0);
            }
        }
        if (IsSlash) EnemySlashedCount = 0;
        SpawnTxt(IsSuccess ? "Great!" : "Miss!", MouseObj.position, IsSuccess);
    }

    void SpawnTxt(string _desc, Vector2 _pos, bool Success)
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(_pos);

        Vector2 WorldObject_ScreenPosition = WorldtoScreenPos(ViewportPosition);

        GameObject _obj = Instantiate(BeatRsltLabel);
        _obj.transform.SetParent(MainCanvas.transform);
        TextMeshProUGUI _txt = _obj.GetComponentInChildren<TextMeshProUGUI>();
        RectTransform rect = _obj.GetComponent<RectTransform>();
        if (rect != null && _txt != null)
        {
            rect.localScale = Vector2.one;
            rect.anchoredPosition = WorldObject_ScreenPosition;
            _txt.text = _desc;
            _txt.color = Success ? Color.green : Color.red;
        }
        Destroy(_obj, 0.3f);
    }

    private Vector2 WorldtoScreenPos(Vector2 _VP)
    {
        float Xpos = (_VP.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f);
        float YPos = (_VP.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f);
        return new Vector2(Xpos, YPos);
    }

    public void ResetCharacter()
    {
        if (PlayrtCharacter) PlayrtCharacter.transform.position = ResetPos;
    }

    public void SpawnEnemy(Vector2 pos,bool SetTarget)
    {
        GameObject obj = Instantiate(enemy, pos, Quaternion.identity);
        if (SetTarget && obj.GetComponent<EnemyScript>()) obj.GetComponent<EnemyScript>().MainTarget = PlayrtCharacter;
    }

    public void EnemySlashed()
    {
        EnemySlashedCount++;
    }
}
