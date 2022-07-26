using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float speed;


    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);
    }


}
