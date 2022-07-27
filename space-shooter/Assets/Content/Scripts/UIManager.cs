using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text scoreText;
    public TMP_Text hiScoreText;
    public GameObject gameOverUI;
    public Button restartBtn;
    public Button exitBtn;

    public GameObject hearts;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        scoreText.text = $"Score: {GameManager.instance.score}";
    }

    public void SetHiScore()
    {
        hiScoreText.text = $"HiScore: {GameManager.instance.hiScore}";
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void GameExit()
    {
        Debug.Log("Game is shutting down...");
        Application.Quit();
    }

    public void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    public void DestroyHeart()
    {
        int lastChildIndex = hearts.transform.childCount - 1;
        Debug.Log(lastChildIndex);
        Destroy(hearts.transform.GetChild(lastChildIndex).gameObject);
    }
}
