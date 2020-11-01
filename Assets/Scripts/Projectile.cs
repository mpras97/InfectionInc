using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _Speed;
    [SerializeField]
    private float _LifeTime;
    [SerializeField]
    private int _Damage;
    // 0 for player and 1 for enemy
    [SerializeField]
    private float _ProjectileType;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", _LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * _Speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_ProjectileType == 1)
        {
            if (collision.tag == "Player")
            {
                // Take damage for player
                Debug.Log("Collision with player");
                collision.GetComponent<Player>().TakeDamage(_Damage);
            }
        }
        else if (_ProjectileType == 0)
        {
            if (collision.tag == "Enemy")
            {
                // Take damage to the enemy
                Debug.Log("Collision with enemy");
                collision.GetComponent<Enemy>().TakeDamage(_Damage);
            }
        }
        DestroyProjectile();
    }
}
