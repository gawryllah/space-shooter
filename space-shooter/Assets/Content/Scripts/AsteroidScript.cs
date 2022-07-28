using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(0.5f * rotationSpeed, 1.5f * rotationSpeed);
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isGameOn)
        {
            return;
        }

        transform.position -= new Vector3((GameManager.instance.bgScrollingSpeed * Time.deltaTime) + 0.1f, 0f);

        transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
    }
}
