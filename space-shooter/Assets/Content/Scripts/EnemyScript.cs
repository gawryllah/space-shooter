using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public static float speed = 3;
    [SerializeField]
    bool canMove;

    Animator animator;

    public GameObject explosionPrefab;
    private GameObject explosion;

    private SpriteRenderer sr;
   



    PolygonCollider2D polygonCollider2d;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        polygonCollider2d = GetComponent<PolygonCollider2D>();
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
      

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            transform.position -= new Vector3((speed * Time.deltaTime) , 0f);

        if(transform.position.x < -10.5f)
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

            explosion = Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
            Destroy(explosion.transform.gameObject, 0.6f);
            StartCoroutine(fadeAway());
            
            

            GameManager.instance.addPoint();
        }
    }

    private IEnumerator fadeAway()
    {
        Color currColor = sr.color;
        float alpha = sr.color.a;

        while (sr.color.a > 0)
        {
            alpha -= 0.10f;
            sr.color = new Color(currColor.r, currColor.g, currColor.g, alpha);
            yield return new WaitForSecondsRealtime(0.015f);
        }
    }

    private void speedUp()
    {
        speed *= 1.05f;
        Debug.Log($"Sped up: {speed}");
    }




}
