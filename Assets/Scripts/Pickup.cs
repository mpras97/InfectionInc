using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // pickup type 1 for fire and 2 for else
    [SerializeField]
    private float _PickupType;
    [SerializeField]
    private int _PickupPoint;
    [SerializeField]
    private float _LifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyPickup", _LifeTime);
    }

    void DestroyPickup()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Give the points addition to the player
            if (_PickupType == 1)
            {
                collision.GetComponent<Player>().TakeDamage(-2);
            }
            else
                collision.GetComponent<Player>().AddPoints(_PickupPoint);
            DestroyPickup();
        }
    }
}
