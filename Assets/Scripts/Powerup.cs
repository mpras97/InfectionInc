using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private int PowerupType;

    private PowerupSelector _BtnInteraction;

    // Start is called before the first frame update
    void Start()
    {
        _BtnInteraction = GameObject.FindGameObjectWithTag("BtnInteraction").GetComponent<PowerupSelector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bool success = _BtnInteraction.updatePowerupUI(PowerupType);
            if (success)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
