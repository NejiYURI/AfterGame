using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTxtScript : MonoBehaviour
{
    public float MaxSize = 2f;
    public TextMeshProUGUI ScoreLabel;
    public Color OriginalColor;
    public Color ScoreColor;
    private bool StopAction;
    private Coroutine Coroutine;
    void Start()
    {
        if (GameEventManager.instance)
        {
            GameEventManager.instance.AddScore.AddListener(ScoreAdd);
        }
        if (ScoreLabel) ScoreLabel.color = OriginalColor;
    }

    private void Update()
    {
        if (StopAction) return;
        this.transform.localScale = new Vector2(Mathf.Clamp(this.transform.localScale.x - Time.deltaTime * 3f, 1f, 100f), Mathf.Clamp(this.transform.localScale.y - Time.deltaTime * 3f, 1f, 100f));
        if (ScoreLabel) ScoreLabel.color = Color.Lerp(OriginalColor, ScoreColor, (this.transform.localScale.x - 1f) / MaxSize);
    }

    void ScoreAdd()
    {
        this.transform.localScale = new Vector2(MaxSize, MaxSize);
        if (ScoreLabel) ScoreLabel.color = ScoreColor;
        if (Coroutine != null) StopCoroutine(Coroutine);
        Coroutine = StartCoroutine(stopAction());
    }

    IEnumerator stopAction()
    {
        StopAction = true;
        yield return new WaitForSeconds(0.3f);
        StopAction = false;
    }
}
