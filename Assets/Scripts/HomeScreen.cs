using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerScore"))
        {
            _ScoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("PlayerScore", 0);
            _ScoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame()
    {
        Debug.Log("Go to game");
        SceneManager.LoadScene(1);
    }
}
