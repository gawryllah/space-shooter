using UnityEngine;

public class PlayerController : MonoBehaviour
{

    

    [SerializeField]
    float movmentSpeed;

    [SerializeField]
    GameObject bullet;

    public static int health;

    public GameObject engine1;
    public GameObject engine2;

    private float onScale = 4.25f;
    private float offScale = 3f;


    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = new Vector3(-7.4f, 0f);
        health = 3;
    }
    // Update is called once per frame
    void Update()
    {

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("shoot", 0.2f);
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
        if (collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Obstacle"))
        {
            GameManager.instance.GameOver();
        }

        
    }

    public static void takeDmg()
    {
        health -= 1;
        UIManager.instance.DestroyHeart();
        if (health <= 0)
            GameManager.instance.GameOver();
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
}
