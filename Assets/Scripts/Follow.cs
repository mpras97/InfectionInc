using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : Enemy
{
    [SerializeField]
    private float _StopDistance;
    [SerializeField]
    private int _Damage;
    [SerializeField]
    private float _AttackSpeed;
    [SerializeField]
    private float _Speed;
    [SerializeField]
    private float _AttackTime;

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform != null)
        {
            if (Vector2.Distance(transform.position, PlayerTransform.position) > _StopDistance)
                transform.position = Vector2.MoveTowards(transform.position, PlayerTransform.position, _Speed * Time.deltaTime);
            else
            {
                if (Time.time >= _AttackTime)
                {
                    StartCoroutine("Attack");
                    _AttackTime = Time.time + _TimeBetweenShots;
                }
            }
        }
    }

    IEnumerator Attack()
    {

        Vector2 originalPos = transform.position;
        Vector2 targetPos = PlayerTransform.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * _AttackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPos, targetPos, formula);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(_Damage);
        }
    }
}
