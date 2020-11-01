using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _AttackJoystick;

    [SerializeField]
    private Transform _ShotPoint;
    [SerializeField]
    private float _TimeBetweenShots;
    [SerializeField]
    private GameObject _Projectile;
    [SerializeField]
    private GameObject _FireProjectile;

    private float _ShotTime;
    private float _PowerupSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*public void UpdatePowerupSelected(float selectedPowerup)
    {
        _PowerupSelected = selectedPowerup;
    }*/

    public void changePowerupToFire()
    {
        _PowerupSelected = 1;
        Invoke("ChangeToDefault", 5);
    }

    public void changePowerupToLaser()
    {
        _PowerupSelected = 2;
        Invoke("ChangeToDefault", 5);
    }

    public void ChangeToDefault()
    {
        _PowerupSelected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalDirection = _AttackJoystick.GetComponent<Joystick>().Horizontal;
        float verticalDirection = _AttackJoystick.GetComponent<Joystick>().Vertical;
        Vector2 direction = new Vector2(horizontalDirection, verticalDirection);
        if (!Vector2.Equals(direction, Vector2.zero))
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = rotation;
            if (Time.time >= _ShotTime)
            {
                if (_PowerupSelected == 0)
                {
                    Instantiate(_Projectile, _ShotPoint.position, transform.rotation);
                }
                else if (_PowerupSelected == 1)
                {
                    Instantiate(_FireProjectile, _ShotPoint.position, transform.rotation);
                }
                else if (_PowerupSelected == 2)
                {
                    for (int j=0; j < 10; j++)
                    {
                        Instantiate(_Projectile, _ShotPoint.position, transform.rotation);
                    }
                }
                _ShotTime += _TimeBetweenShots;
            }
        }
    }
}
