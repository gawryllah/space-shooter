using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    float speed = 3;
    [SerializeField]
    bool canMove;


    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        boxCollider2D = GetComponent<BoxCollider2D>();

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
            boxCollider2D.enabled = false;
            Destroy(collision.gameObject);
            canMove = false;

            GameManager.instance.addPoint();
        }

      
    }

}
