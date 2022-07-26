using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    float movmentSpeed;

    [SerializeField]
    GameObject bullet;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-8.25f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W) && transform.position.y < 4.75f)
        {
            transform.position += new Vector3(0, movmentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > -4.75f)
        {
            transform.position += new Vector3(0, (movmentSpeed * Time.deltaTime) * -1);   
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("shoot", 0.2f);
        }

    }

    /*
    private IEnumerator shooting()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject respedBullet = Instantiate(bullet);
                if (!respedBullet.GetComponent<BulletScript>().isDirSet) { 
                    respedBullet.transform.position = transform.GetChild(0).position;
                    respedBullet.transform.parent = transform.GetChild(1);
                }
                Destroy(respedBullet, 3f);
                yield return new WaitForSecondsRealtime(0.5f);
            }

            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
    */

    void shoot()
    {
        GameObject respedBullet = Instantiate(bullet);
        respedBullet.transform.position = transform.GetChild(0).position;
        
        Destroy(respedBullet, 3f);
    }
    
}
