using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    

    [SerializeField]
    float movmentSpeed;

    [SerializeField]
    GameObject bullet;

    public static int health;
    public static int maxHealth = 6;

    public GameObject engine1;
    public GameObject engine2;

    private float onScale = 4.25f;
    private float offScale = 3f;

    public GameObject immortalityPrefab;
    private bool immortal;

    private bool autoShootingOn;

    public static bool canShoot;

    // Start is called before the first frame update
    private void Awake()
    {
        autoShootingOn = false;
        immortal = false;
        transform.position = new Vector3(-7.4f, 0f);
        health = 3;
        canShoot = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOn)
            StopAllCoroutines();

        Move();

        if (Input.GetKeyDown(KeyCode.Space) && canShoot && !autoShootingOn)
        {
            Invoke("shoot", 0.2f);
            //SoundManager.instance.playerAttack.
        }

    }


    void shoot()
    {
        GameObject respedBullet = Instantiate(bullet, transform.GetChild(0).position, Quaternion.identity);
        Destroy(respedBullet, 3f);
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W) && transform.position.y < 4.75f)
        {
            transform.position += new Vector3(0, movmentSpeed * Time.deltaTime);
            turnOnEngine();
        }
        else if (Input.GetKey(KeyCode.S) && transform.position.y > -4.75f)
        {
            transform.position -= new Vector3(0, (movmentSpeed * Time.deltaTime));
            turnOnEngine();
        }
        else
        {
            turnOffEngine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Obstacle")) && !immortal)
        {
            GameManager.instance.GameOver();
        }
        else if (collision.gameObject.tag.Equals("Heart") && health < maxHealth) 
        {
            Destroy(collision.gameObject);
            health++;
            //Debug.Log($"Health: {health}");
            UIManager.instance.AddHeart();

        }else if (collision.gameObject.tag.Equals("Immortality"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(immortalityHandler());

        }else if(collision.gameObject.tag.Equals("Ammo"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(autoShooting(8f));

        }

        
    }

    public static void takeDmg()
    {
        health -= 1;
        UIManager.instance.DestroyHeart();
        if (health <= 0)
        {
            GameManager.instance.GameOver();
            
        }
            
    }

    private void turnOnEngine()
    {
        engine1.transform.localScale = new Vector3(onScale, onScale, onScale);
        engine2.transform.localScale = new Vector3(onScale, onScale, onScale);
    }

    private void turnOffEngine()
    {
        engine1.transform.localScale = new Vector3(offScale, offScale, offScale);
        engine2.transform.localScale = new Vector3(offScale, offScale, offScale);
    }

    private IEnumerator immortalityHandler()
    {
        
        GameObject go =  Instantiate(immortalityPrefab, transform.position, Quaternion.identity);
        go.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        go.transform.localScale = new Vector3(4.16f, 4.16f, 4.16f);
        SpriteRenderer gosr = go.GetComponent<SpriteRenderer>();
        Color gosrColor = gosr.color;
        immortal = true;
        yield return new WaitForSecondsRealtime(7f);

        for (int i = 0; i < 3; i++)
        {
            gosr.color = Color.clear;
            yield return new WaitForSecondsRealtime(0.5f);
            gosr.color = gosrColor;
            yield return new WaitForSecondsRealtime(0.5f);
        }

        //yield return new WaitForSecondsRealtime(2f);
        Destroy(go);
        immortal = false;
    }

    private IEnumerator autoShooting(float duration)
    {
        autoShootingOn = true;
        var end = Time.time + duration;
        while (Time.time < end && GameManager.instance.isGameOn)
        { 
            shoot();
            yield return new WaitForSecondsRealtime(0.15f);
        }
        autoShootingOn = false;
    }
}
