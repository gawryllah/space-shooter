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
    public TMP_Text tillBossText;
    public TMP_Text hiScoreText;

    public GameObject gameOverUI;
    public GameObject winUI;

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
        winUI.SetActive(false);
    }


    public void UpdateUI()
    {
        scoreText.text = $"Score: {GameManager.instance.score}";
        tillBossText.text = $"Till Boss: {(GameManager.instance.bossScore - GameManager.instance.score < 0 ? "0" : GameManager.instance.bossScore - GameManager.instance.score)}";
        
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

    public void ShowWinUI()
    {
        winUI.SetActive(true);
    }

    public void DestroyHeart()
    {
        int lastChildIndex = hearts.transform.childCount - 1;
        //Debug.Log(lastChildIndex);
        Destroy(hearts.transform.GetChild(lastChildIndex).gameObject);
    }

    public void AddHeart()
    {
        var obj = Instantiate(hearts.transform.GetChild(0));
        obj.SetParent(hearts.gameObject.transform);

        for(int i = 0; i < hearts.transform.childCount; i++)
        {
            hearts.transform.GetChild(i).localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
