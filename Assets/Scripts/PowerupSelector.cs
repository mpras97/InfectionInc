using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject _LeftJoyBtn;
    [SerializeField]
    private GameObject _BelowJoyBtn;
    [SerializeField]
    private GameObject _RightJoyBtn;

    [SerializeField]
    private GameObject _AttackHead;
    [SerializeField]
    private GameObject _Player;

    public Sprite ShieldBtn;
    public Sprite LaserBtn;
    public Sprite RepairBtn;
    public Sprite SpeedBtn;
    public Sprite FireBtn;
    public Sprite DefaultSprite;

    private int LeftBtnConstant = -1;
    private int BelowBtnConstant = -1;
    private int RightBtnConstant = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 1 -> Fire
    // 2 -> Laser
    // 3 -> Repair
    // 4 -> Shield
    // 5 -> Speed
    public bool updatePowerupUI(int powerupConstant)
    {
        Sprite toChange;
        if (powerupConstant == 1)
            toChange = FireBtn;
        else if (powerupConstant == 2)
            toChange = LaserBtn;
        else if (powerupConstant == 3)
            toChange = RepairBtn;
        else if (powerupConstant == 4)
            toChange = ShieldBtn;
        else if (powerupConstant == 5)
            toChange = SpeedBtn;
        else
        {
            toChange = DefaultSprite;
        }
        if (LeftBtnConstant == -1)
        {
            LeftBtnConstant = powerupConstant;
            _LeftJoyBtn.GetComponent<Image>().sprite = toChange;
            return true;
        }
        else if (BelowBtnConstant == -1)
        {
            BelowBtnConstant = powerupConstant;
            _BelowJoyBtn.GetComponent<Image>().sprite = toChange;
            return true;
        }
        else if (RightBtnConstant == -1)
        {
            RightBtnConstant = powerupConstant;
            _RightJoyBtn.GetComponent<Image>().sprite = toChange;
            return true;
        }
        else
            return false;
    }

    public void LeftJoyBtnSelected()
    {
        if (LeftBtnConstant == -1)
            return;
        if (LeftBtnConstant == 1)
            FirePowerup();
        else if (LeftBtnConstant == 2)
            LaserPowerup();
        else if (LeftBtnConstant == 3)
            RepairPowerup();
        else if (LeftBtnConstant == 4)
            ShieldPowerup();
        else if (LeftBtnConstant == 5)
            SpeedPowerup();
        RevertUISprite("left");
        LeftBtnConstant = -1;
    }

    void RevertUISprite(string toRevert)
    {
        if (toRevert == "left")
        {
            _LeftJoyBtn.GetComponent<Image>().sprite = DefaultSprite;
        }
        else if(toRevert == "below")
        {
            _BelowJoyBtn.GetComponent<Image>().sprite = DefaultSprite;
        }
        else if (toRevert == "right")
        {
            _RightJoyBtn.GetComponent<Image>().sprite = DefaultSprite;
        }
    }

    public void BelowJoyBtnSelected()
    {
        if (BelowBtnConstant == -1)
            return;
        if (BelowBtnConstant == 1)
            FirePowerup();
        else if (BelowBtnConstant == 2)
            LaserPowerup();
        else if (BelowBtnConstant == 3)
            RepairPowerup();
        else if (BelowBtnConstant == 4)
            ShieldPowerup();
        else if (BelowBtnConstant == 5)
            SpeedPowerup();
        RevertUISprite("below");
        BelowBtnConstant = -1;
    }

    public void RightJoyBtnSelected()
    {
        if (RightBtnConstant == -1)
            return;
        if (RightBtnConstant == 1)
            FirePowerup();
        else if (RightBtnConstant == 2)
            LaserPowerup();
        else if (RightBtnConstant == 3)
            RepairPowerup();
        else if (RightBtnConstant == 4)
            ShieldPowerup();
        else if (RightBtnConstant == 5)
            SpeedPowerup();
        RevertUISprite("right");
        RightBtnConstant = -1;
    }

    private void FirePowerup()
    {
        _AttackHead.GetComponent<Weapon>().changePowerupToFire();
        Debug.Log("Fire powerup selected");
    }

    private void LaserPowerup()
    {
        _AttackHead.GetComponent<Weapon>().changePowerupToLaser();
        Debug.Log("Laser powerup selected");
    }

    private void RepairPowerup()
    {
        _Player.GetComponent<Player>().TakeDamage(-2);
        Debug.Log("Repair powerup selected");
    }

    private void ShieldPowerup()
    {
        _Player.GetComponent<Player>().ActivateShield();
        Debug.Log("Shield powerup selected");
    }

    private void SpeedPowerup()
    {
        _Player.GetComponent<Player>().UpdateSpeed();
        Debug.Log("Speed powerup selected");
    }
}
