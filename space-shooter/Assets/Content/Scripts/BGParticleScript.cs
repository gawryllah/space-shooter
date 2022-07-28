using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParticleScript : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position -= new Vector3(GameManager.instance.bgScrollingSpeed * Time.deltaTime, 0f);

        if(transform.position.x < -12f)
        {
            Destroy(transform.gameObject);
        }
    }
}
