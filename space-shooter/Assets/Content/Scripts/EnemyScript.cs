using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    float speed = 3;
    [SerializeField]
    bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
            transform.position += new Vector3(-1 * (speed * Time.deltaTime), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            canMove = false;
            Destroy(this, 2f);
            Destroy(collision.gameObject);
        }
    }
}
