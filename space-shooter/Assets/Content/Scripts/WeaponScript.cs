using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    static float speed = 8f;

    private void FixedUpdate()
    {
        
        transform.position -= new Vector3(speed * Time.deltaTime, 0);

        if(transform.position.x < -12f)
        {
            Destroy(transform.gameObject);
        }
    }
}
