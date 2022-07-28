using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    

    public bool isGameOn;

    public GameObject enemyPrefab;
    public float maxRespHeight;

    public float minRespDelay;
    public float maxRespDelay;

    private float randomDel;
    private float randomHeight;

    public int score;
    public int hiScore;

    public float bgScrollingSpeed;

    public List<Sprite> sprites;
    public GameObject planetPlaceHolder;
    public GameObject asteroid;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            isGameOn = true;
        }
    }

    // Start is called before the first frame update
    void Start() {

        setScore();
        StartCoroutine(spawner());
        StartCoroutine(backgroundParticles());
    }

    

    private IEnumerator spawner()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(randomDel);
            if(isGameOn)
                spawnEnemy();
        }
    }

    void spawnEnemy()
    {
        if (isGameOn) {
            Instantiate(enemyPrefab, new Vector3(12, randomHeight), Quaternion.identity);

            setRandomDelay();
            setRandomHeight();
        }
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
        UIManager.instance.UpdateUI();
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

        UIManager.instance.SetHiScore();
        score = 0;
    }

    private void saveScore()
    {
        PlayerPrefs.SetInt("SS-score", score);
    }



    public void GameOver()
    {
        isGameOn = false;
        if (score > hiScore)
            saveScore();
        Time.timeScale = 0f;

        UIManager.instance.ShowGameOverUI();
        StopCoroutine(spawner());
        StopCoroutine(backgroundParticles());
    }

    private IEnumerator backgroundParticles()
    {
        while (isGameOn)
        {
            yield return new WaitForSecondsRealtime(Random.Range(6f, 20f));
            if (Random.Range(0, 1) < 0.85f) {
                GameObject go = Instantiate(planetPlaceHolder, new Vector3(12, randomHeight + 1), Quaternion.identity);
                float scale = Random.Range(0.6f, 1f);
                go.transform.localScale = new Vector3(scale, score, score);
            }
            else
            {
                Instantiate(asteroid, new Vector3(12, randomHeight + 1), Quaternion.identity);
            }
        }

    }

}
