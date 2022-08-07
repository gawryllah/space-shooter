using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParticleScript : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {

        if (GameManager.instance.isBossSpawned && !sr.isVisible)
            Destroy(gameObject);

        transform.position -= new Vector3((GameManager.instance.bgScrollingSpeed * Time.deltaTime) - 0.01f, 0f);

        if(transform.position.x < -12f)
        {
            Destroy(transform.gameObject);
        }
    }
}
