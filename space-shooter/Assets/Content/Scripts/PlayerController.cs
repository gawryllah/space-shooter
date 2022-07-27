using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float movmentSpeed;

    [SerializeField]
    GameObject bullet;

    public static int health;


    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = new Vector3(-8.25f, 0f);
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
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > -4.75f)
        {
            transform.position -= new Vector3(0, (movmentSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("Obstacle"))
        {
            GameManager.instance.GameOver();
        }

        
    }
}
