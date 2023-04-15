using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RythmGameManager : MonoBehaviour
{
    public static RythmGameManager instance;

    private void Awake()
    {
        instance = this;
    }
    public MusicSheet musicSheet;
    public Canvas MainCanvas;
    public GameObject PlayerObj;
    public GameObject GameOverPanel;
    public GameObject BeatRsltLabel;
    public Transform MouseObj;

    [SerializeField]
    private int EnemyNum;
    public int EnemyStartNum = 8;
    public int EnemyMaxNum = 20;

    public int PlayerHealth;

    public float SpawnFreq = 0.5f;
    public float BeatTime=1f;

    public GameObject enemy;

    public List<Transform> SpawnPoints;

    public float SpawnX;
    public float SpawnY;

    private AudioSource musicPlayer;
    public List<AudioClip> MoveSound;
    public AudioClip SlashSound;
    public AudioClip MissSound;
    public AudioClip KillSound;



    private List<SheetData> sheetDatas;
    private List<SheetData> sheetDatasCount;
    [SerializeField]
    private int CurrentBeat;

    private string Beat_Index;

    private int EnemySlashedCount;
    private int Score;

    private RectTransform CanvasRect;
    public TextMeshProUGUI SlashRemain;
    public TextMeshProUGUI HealthTxt;
    public TextMeshProUGUI GameOverLabel;
    public TextMeshProUGUI SongDetailTxt;
    public TextMeshProUGUI ScoreTxt;
    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        GameOverPanel.SetActive(false);
        CanvasRect = MainCanvas.GetComponent<RectTransform>();
        EnemySlashedCount = 0;
        Score = 0;
        AddScore(0);
        if (HealthTxt != null) HealthTxt.text = PlayerHealth.ToString();
        for (int i = 0; i < EnemyStartNum; i++)
        {
            SpawnEnemy();
        }
        if (GameEventManager.instance)
        {
            GameEventManager.instance.EnemyDead.AddListener(EnemyDead);
            GameEventManager.instance.BeatOver.AddListener(SetSlashNotice);
        }
        ReadData();
    }

    void ReadData()
    {
        if (GameSettingScript.instance) musicSheet = GameSettingScript.instance.Sheet;

        musicPlayer.clip = musicSheet.music;
        sheetDatas = new List<SheetData>();
        sheetDatasCount = new List<SheetData>();
        sheetDatas.AddRange(musicSheet.sheetDatas);
        sheetDatasCount.AddRange(musicSheet.sheetDatas);
        this.BeatTime = musicSheet.BeatTime;
        this.SpawnFreq = musicSheet.SpawnFreq;
        if (SlashRemain != null) SlashRemain.text = GetDisToNextSlash().ToString();
        if (SongDetailTxt != null) SongDetailTxt.text = musicSheet.MusicName + " - " + musicSheet.Composer;
        StartCoroutine(GameStartCountDown());
    }

    void GameStart()
    {
        PlayAudio();
        StartCoroutine(EnemySpawnCounter());
    }

    public void PlayAudio()
    {
        if (musicSheet && musicPlayer)
        {
            musicPlayer.Play();
            Invoke("GameClear", musicPlayer.clip.length);
        }
    }

    private void Update()
    {
        if (musicPlayer.isPlaying && sheetDatas.Count > 0)
        {
            while (sheetDatas.Count > 0 && sheetDatas[0].position - musicPlayer.time <= this.BeatTime)
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

    public void SetSlashNotice()
    {
        CurrentBeat++;
        if (SlashRemain != null)
        {
            int nextS = GetDisToNextSlash();
            if (nextS > 0) SlashRemain.text = GetDisToNextSlash().ToString();
            else if (nextS == 0) SlashRemain.text = "Slash!";
            else SlashRemain.text = "";
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

    IEnumerator EnemySpawnCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnFreq);
            if (EnemyNum < EnemyMaxNum)
            {
                SpawnEnemy();
            }
        }

    }

    IEnumerator GameStartCountDown()
    {
        yield return new WaitForSeconds(3f);
        GameStart();
    }

    private int GetDisToNextSlash()
    {
        for (int i = CurrentBeat; i < sheetDatasCount.Count; i++)
        {
            if (sheetDatasCount[i].IsEnd)
            {
                return i - CurrentBeat;
            }
        }
        return -1;
    }

    public void SpawnEnemy()
    {
        if (enemy != null && SpawnPoints.Count > 0 && PlayerObj)
        {
            int randIndex = -1;
            do
            {
                randIndex = Random.Range(0, SpawnPoints.Count);
            } while (Vector2.Distance(SpawnPoints[randIndex].position, this.PlayerObj.transform.position) <= 2f);

            Vector2 s_Pos = SpawnPoints[randIndex].position;
            //Vector2 s_Pos = new Vector2(Random.Range(-SpawnX, SpawnX), Random.Range(-SpawnY, SpawnY));
            GameObject obj = Instantiate(enemy, s_Pos, Quaternion.identity);
            if (obj.GetComponent<EnemyScript>()) obj.GetComponent<EnemyScript>().MainTarget = this.PlayerObj.transform;
            EnemyNum++;
        }
    }

    void EnemyDead()
    {
        EnemyNum--;
        // SpawnEnemy();
    }

    public void PlayerGetDamage()
    {
        Debug.Log("GetDamage");
        PlayerHealth = Mathf.Clamp(PlayerHealth - 1, 0, 100);
        if (HealthTxt != null) HealthTxt.text = PlayerHealth.ToString();
        if (PlayerHealth <= 0) GameOver();
    }

    void GameClear()
    {
        StopAllCoroutines();
        GameOverLabel.text = "Clear";
        GameOverPanel.SetActive(true);
    }

    public void GameOver()
    {
        StopAllCoroutines();
        GameOverLabel.text = "Failed";
        GameOverPanel.SetActive(true);
        StartCoroutine(GameOverTimer());
    }

    IEnumerator GameOverTimer()
    {
        float t_cnt = 0;
        while (t_cnt < 1)
        {
            t_cnt += Time.deltaTime;
            musicPlayer.pitch -= Time.deltaTime * 0.9f;
            yield return null;
        }
        musicPlayer.Stop();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void ShowBeatResult(bool IsSuccess, bool IsSlash)
    {
        if (!IsSuccess && AudioController.instance) AudioController.instance.PlaySound(MissSound);
        if (IsSuccess)
        {
            if (IsSlash)
            {
                PlaySlashSound();
                if (IsSlash && EnemySlashedCount > 0)
                {
                    if (AudioController.instance)
                        StartCoroutine(playkillsound());
                }
            }
            else PlayMoveSound();
            AddScore(100);
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

    public void PlayMoveSound()
    {
        if (AudioController.instance && MoveSound.Count > 0) AudioController.instance.PlaySound(MoveSound[Random.Range(0, MoveSound.Count)]);
    }

    public void PlaySlashSound()
    {
        if (AudioController.instance) AudioController.instance.PlaySound(SlashSound);
    }

    IEnumerator playkillsound()
    {
        yield return new WaitForSeconds(0.1f);
        AudioController.instance.PlaySound(KillSound);
        yield return new WaitForSeconds(0.2f);
        if (RythmGameManager.instance) RythmGameManager.instance.SpawnEnemy();
    }

    void AddScore(int amount)
    {
        Score += amount;
        if(amount>0 && GameEventManager.instance) GameEventManager.instance.AddScore.Invoke();
        if (ScoreTxt) ScoreTxt.text = Score.ToString("00000");
    }

    public void AddEnemyScore()
    {
        AddScore(150);
    }

    public void EnemySlashed()
    {
        EnemySlashedCount++;
    }
}
