using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform PlayerTransform;

    public int Health;
    public int DropChance;
    public GameObject[] Drops;
    public float _TimeBetweenShots;

    // Start is called before the first frame update
    public virtual void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Take damage");
        int newHealth = Health - damage;
        if (newHealth <= 0)
        {
            DestroyEnemy();
        }
        else
        {
            Health = newHealth;
        }
    }

    public void DestroyEnemy()
    {
        int randomNo = Random.Range(0, 100);
        Debug.Log(randomNo);
        if (randomNo < DropChance)
        {
            int randomPickupIndex = Random.Range(0, Drops.Length);
            int randomNoPickups = Random.Range(1, 4);
            for (int i = 0; i < randomNoPickups; i++)
            {
                Instantiate(Drops[randomPickupIndex], transform.position, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }
}
