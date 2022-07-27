using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
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

        
        transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
    }
}
