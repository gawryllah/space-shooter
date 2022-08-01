using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float speed;

    private void FixedUpdate()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0);
    }
}
