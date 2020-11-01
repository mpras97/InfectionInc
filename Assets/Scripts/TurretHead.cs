using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHead : Enemy
{

    [SerializeField]
    private Transform[] _ShotPoints;
    [SerializeField]
    private GameObject _TurretBullet;

    private float _ShotTime;

    private void Start()
    {
        base.Start();
        transform.position = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _ShotTime)
        {

            for (int i = 0; i < _ShotPoints.Length; i++)
            {
                Vector2 direction = _ShotPoints[i].position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Instantiate(_TurretBullet, _ShotPoints[i].position, rotation);
            }
            _ShotTime = Time.time + _TimeBetweenShots;
        }
    }
}
