using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _Speed;
    private Rigidbody2D rb;

    private Vector2 moveAmount;
    [SerializeField]
    private GameObject _MoveJoystick;

    [SerializeField]
    private Image[] lives;
    public Sprite FullLife;
    public Sprite HalfLife;
    public Sprite EmptyLife;
    [SerializeField]
    private int Health;
    public int Points = 0;

    [SerializeField]
    private Text _PointsText;
    [SerializeField]
    private GameObject _GameOverPanel;
    private bool isGameRunning = true;

    [SerializeField]
    private GameObject _Shield;
    private bool shieldActivated = false;

    [SerializeField]
    private GameObject _WaveSpawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        UpdateHealthUI();
        _Shield.SetActive(false);
        /*UpdatePointsUI();*/
        if (PlayerPrefs.HasKey("PlayerScore"))
        {
            Points = PlayerPrefs.GetInt("PlayerScore");
        }
        else
        {
            PlayerPrefs.SetInt("PlayerScore", 0);
        }
        UpdatePointsUI();
    }

    public void UpdateSpeed()
    {
        _Speed *= 2;
        Invoke("RevertSpeed", 5);
    }

    public void UpdatePointsUI()
    {
        _PointsText.text = Points.ToString();
    }

    public void RevertSpeed()
    {
        _Speed /= 2;
    }

    public void AddPoints(int points)
    {
        Points += points;
        PlayerPrefs.SetInt("PlayerScore", Points);
        UpdatePointsUI();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = _MoveJoystick.GetComponent<Joystick>().Horizontal;
        if (horizontalInput < 0)
        {
            horizontalInput = -1;
        }
        else if (horizontalInput > 0)
            horizontalInput = 1;
        float verticalInput = _MoveJoystick.GetComponent<Joystick>().Vertical;
        if (verticalInput < 0)
            verticalInput = -1;
        else if (verticalInput > 0)
            verticalInput = 1;
        Vector2 moveInput = new Vector2(horizontalInput, verticalInput);
        moveAmount = moveInput.normalized * _Speed;
    }

    private void FixedUpdate()
    {
        if (!isGameRunning)
            return;
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (shieldActivated)
            return;
        int newHealth = Health - damage;
        if (newHealth <= 0)
        {
            // Destroy(this.gameObject);
            EndGame();
        }
        else if (newHealth > 5)
        {
            Health = 5;
        }
        else
        {
            Health = newHealth;
        }
        UpdateHealthUI();
    }

    void EndGame()
    {
        isGameRunning = false;
        _GameOverPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        isGameRunning = true;
        Health = 5;
        UpdateHealthUI();
        _GameOverPanel.SetActive(false);
        _WaveSpawner.GetComponent<WaveSpawner>().RestartWaveSpawner();
    }

    public void GoToLoader()
    {
        SceneManager.LoadScene(0);
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < Health)
            {
                lives[i].sprite = FullLife;
            }
            else
            {
                lives[i].sprite = EmptyLife;
            }
        }
    }

    public void ActivateShield()
    {
        _Shield.SetActive(true);
        shieldActivated = true;
        Invoke("ChangeToDefault", 5);
    }

    public void ChangeToDefault()
    {
        shieldActivated = false;
        _Shield.SetActive(false);
    }
}
