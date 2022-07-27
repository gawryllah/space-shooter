using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    float speed = 3;
    [SerializeField]
    bool canMove;

    Animator animator;


    PolygonCollider2D polygonCollider2d;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        polygonCollider2d = GetComponent<PolygonCollider2D>();
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            transform.position -= new Vector3((speed * Time.deltaTime) , 0f);

        if(transform.position.x < -9.25f)
        {
            Destroy(transform.gameObject);
            PlayerController.takeDmg();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {

            Destroy(transform.gameObject, 1.25f);
            polygonCollider2d.enabled = false;
            Destroy(collision.gameObject);
            canMove = false;
            animator.enabled = false;
            

            GameManager.instance.addPoint();
        }

      
    }

}
