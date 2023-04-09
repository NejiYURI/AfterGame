using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int MaxHealth;
    private int Health;

    public GameObject DeathObj;

    private void Start()
    {
        Health = MaxHealth;
    }
    public void GetDamage(int amount)
    {
        Health-=amount;
        if (Health <= 0)
        {
            Instantiate(DeathObj,this.transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
