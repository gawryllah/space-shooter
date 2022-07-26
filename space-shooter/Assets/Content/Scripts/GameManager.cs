using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text scoreText;
    public TMP_Text hiScoreText;
    public GameObject gameOverUI;
    public Button restartBtn;
    public Button exitBtn;

    public GameObject enemyPrefab;
    public float maxRespHeight;

    public float minRespDelay;
    public float maxRespDelay;

    private float randomDel;
    private float randomHeight;

    private int score;
    private int hiScore;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {

        gameOverUI.SetActive(false);
        setScore();
        StartCoroutine(spawner());
    }

    private IEnumerator spawner()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(randomDel);
            spawnEnemy();
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(12, randomHeight), Quaternion.identity);

        setRandomDelay();
        setRandomHeight();
    }

    void setRandomDelay()
    {

        this.randomDel = Random.Range(minRespDelay, maxRespDelay);
    }

    void setRandomHeight()
    {
        this.randomHeight = Random.Range(-maxRespHeight, maxRespHeight);
    }

    public void addPoint()
    {
        score += 1;
        UpdateUI();
    }

    private void setScore()
    {
        if (PlayerPrefs.HasKey("SS-score"))
        {
            hiScore = PlayerPrefs.GetInt("SS-score");
        }
        else
        {
            hiScore = 0;
        }

        hiScoreText.text = $"HiScore: {hiScore}";
        score = 0;
    }

    private void saveScore()
    {
        PlayerPrefs.SetInt("SS-score", score);
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
    }


    public void GameOver()
    {
        if (score > hiScore)
            saveScore();
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
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
}
