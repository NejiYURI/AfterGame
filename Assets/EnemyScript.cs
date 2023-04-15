using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int MaxHealth;
    private int Health;

    private bool MoveDisable;
    private bool Attacking;

    public float MoveDistance = 1f;

    public Transform MainTarget;
    public GameObject DeathObj;

    private void Start()
    {
        Health = MaxHealth;
        if (GameEventManager.instance)
        {
            GameEventManager.instance.DealDamage.AddListener(Movement);
            GameEventManager.instance.DamageFalied.AddListener(Movement);
        }
    }
    public void GetDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Instantiate(DeathObj, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (GameEventManager.instance)
        {
            GameEventManager.instance.EnemyDead.Invoke();
            GameEventManager.instance.DealDamage.RemoveListener(Movement);
            GameEventManager.instance.DamageFalied.RemoveListener(Movement);
        }
        if (RythmGameManager.instance) RythmGameManager.instance.AddEnemyScore();
    }

    private void Movement()
    {
        if (MainTarget == null || MoveDisable)
        {
            MoveDisable = false;
            return;
        }
        Vector2 des = (MainTarget.position - this.transform.position).normalized * MoveDistance + this.transform.position;
        StartCoroutine(AttackTimer());
        this.transform.LeanMove(des, 0.05f);
    }

    public void DisableMove()
    {
        MoveDisable = true;
    }

    IEnumerator AttackTimer()
    {
        Attacking = true;
        yield return new WaitForSeconds(0.05f);
        Attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && Attacking)
        {
            if (RythmGameManager.instance) RythmGameManager.instance.PlayerGetDamage();
        }
    }
}
