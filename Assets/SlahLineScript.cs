using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlahLineScript : MonoBehaviour
{
    public LineRenderer lR;

    public PolygonCollider2D polygonCollider;
    public float width = 0.1f;
    private List<EnemyScript> enemies;

    //public EdgeCollider2D edgeCollider;
    private void Start()
    {
        enemies = new List<EnemyScript>();
        if (GameEventManager.instance)
        {
            GameEventManager.instance.DealDamage.AddListener(DealDamage);
            GameEventManager.instance.DamageFalied.AddListener(DestoryObj);
        }
    }
    public void SetLineRender(Vector2 Start, Vector2 End)
    {
        lR.SetPosition(0, Start);
        lR.SetPosition(1, (End - Start) * 0.5f + Start);
        lR.SetPosition(2, End);
        //List<Vector2> points = new List<Vector2> { Start-(Vector2)this.transform.position, End- (Vector2)this.transform.position };
        //edgeCollider.SetPoints(points);
        polygonCollider.SetPath(0, GetPoints());
    }

    private List<Vector2> GetPoints()
    {
        Vector3[] positions = new Vector3[lR.positionCount];
        lR.GetPositions(positions);


        float m = (positions[2].x - positions[0].x) != 0 ? (positions[2].y - positions[0].y) / (positions[2].x - positions[0].x) : 1f;
        float dx = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float dy = (width / 2f) * (1 / Mathf.Pow(m * m + 1, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-dx, dy);
        offsets[1] = new Vector3(dx, -dy);
        Vector2 curPos = (Vector2)this.transform.position;
        List<Vector2> colliderPos = new List<Vector2> {
        positions[0]+offsets[0]-(Vector3)curPos,
        positions[2]+offsets[0]-(Vector3)curPos,
        positions[2]+offsets[1]-(Vector3)curPos,
        positions[0]+offsets[1]-(Vector3)curPos
        };
        return colliderPos;
    }

    void DealDamage()
    {
        foreach (var item in enemies)
        {
            item.DisableMove();
            item.GetDamage(1);
        }

        if (GameEventManager.instance)
        {
            GameEventManager.instance.DealDamage.RemoveListener(DealDamage);
            GameEventManager.instance.DamageFalied.RemoveListener(DestoryObj);
        }
        this.polygonCollider.enabled = false;
        StartCoroutine(SlashTimer());
        //DestoryObj();
    }

    void DestoryObj()
    {
        if (GameEventManager.instance)
        {
            GameEventManager.instance.DealDamage.RemoveListener(DealDamage);
            GameEventManager.instance.DamageFalied.RemoveListener(DestoryObj);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            if (collision.GetComponent<EnemyScript>()) enemies.Add(collision.GetComponent<EnemyScript>());
            if (GameEventManager.instance) GameEventManager.instance.EnemySlashed.Invoke();
        }

    }

    IEnumerator SlashTimer()
    {
        lR.startColor = Color.red;
        lR.endColor = Color.red;
        yield return new WaitForSeconds(0.2f);
        Color n_color = Color.white;
        n_color.a = Random.Range(0.005f, 0.03f);
        float timer = 1;
        while (timer > 0)
        {
            yield return null;
            timer -= Time.deltaTime;
            lR.startColor = Color.Lerp(n_color, Color.red, timer / 1f);
            lR.endColor = Color.Lerp(n_color, Color.red, timer / 1f);
        }


    }
}
