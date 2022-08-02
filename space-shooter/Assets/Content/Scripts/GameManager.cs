using System.Collections;
using UnityEngine;

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
    public int bossScore;

    public float bgScrollingSpeed;

    public GameObject[] powerUps;

    public bool canSpawn;

    public GameObject bossPrefab;


    public bool boss;
    public bool isBossSpawned;
    public bool bossMode;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        isGameOn = true;
        boss = false;
        isBossSpawned = false;
        bossMode = false;
    }

    // Start is called before the first frame update
    void Start() {

        EnemyScript.speed = EnemyScript.baseSpeed;
        setScore();
        StartCoroutine(spawner());
        StartCoroutine(powerUpSpawner());
        InvokeRepeating("enemySpeedUp", 0.1f, 12.5f);

        if (PlayerPrefs.HasKey("SS-Boss"))
            if (PlayerPrefs.GetString("SS-Boss").Equals("true"))
                bossMode = true;

        if (PlayerPrefs.HasKey("SS-TBP"))
            bossScore = PlayerPrefs.GetInt("SS-TBP");
        else
            bossScore = 60;

    }

    

    private IEnumerator spawner()
    {
        while (isGameOn)
        {
            yield return new WaitForSecondsRealtime(randomDel);
            if(isGameOn && canSpawn)
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

        if(bossMode && score >= bossScore && !isBossSpawned)
        {
            StartCoroutine(bossFlagSwitch());
            isBossSpawned = true;
            canSpawn = false;
        }
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
        bossScore = 60;
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
        StopAllCoroutines();
    }

    public void playerWon()
    {
        isGameOn = false;
        if (score > hiScore)
            saveScore();
        Time.timeScale = 0f;

        UIManager.instance.ShowWinUI();
        StopAllCoroutines();
    }

    void spawnBoss()
    {
        Instantiate(bossPrefab);
    }

    public float getRandomHeight()
    {
        return randomHeight;
    }

    private void enemySpeedUp()
    {
        EnemyScript.speed *= 1.1f;
        //Debug.Log($"Sped up: {EnemyScript.speed}");

        if (EnemyScript.speed > 5f)
        {
            bgScrollingSpeed *= 1.05f;
            //Debug.Log($"bg speed: {bgScrollingSpeed}");

            if(maxRespDelay > 2f)
            {
                maxRespDelay -= 0.2f; 
            }
        }
    }

    private IEnumerator powerUpSpawner()
    {
        while (isGameOn && !isBossSpawned)
        {
          
            yield return new WaitForSecondsRealtime(Random.Range(13 + PlayerController.health, 22 + PlayerController.health));

            if (PlayerController.health < PlayerController.maxHealth)
            {
                Instantiate(powerUps[Mathf.RoundToInt(Random.Range(0f, (float)powerUps.Length - 1))], new Vector3(13f, randomHeight), Quaternion.identity);
            }
            else
            {
                if(powerUps.Length > 1)
                    Instantiate(powerUps[Mathf.RoundToInt(Random.Range(1f, (float)powerUps.Length - 1))], new Vector3(13f, randomHeight), Quaternion.identity);
            }
            Debug.Log($"Spawned powerup");
        }
    }

    private IEnumerator bossFlagSwitch()
    {
        yield return new WaitUntil(() => FindObjectsOfType<EnemyScript>().Length == 0 && FindObjectsOfType<ObstacleScript>().Length == 0 && 
                                            FindObjectsOfType<BGParticleScript>().Length == 0 && FindObjectsOfType<PowerUpScript>().Length == 0 && FindObjectsOfType<AsteroidScript>().Length == 0);
        boss = true;
        spawnBoss();
        UIManager.instance.tillBossText.enabled = false;
        StopAllCoroutines();
    }

  
}
