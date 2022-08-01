using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    static float speed = 4.5f;

    private void Start()
    {
        speed *= 1.08f;
        Debug.Log($"Aktualna predkosc {this}: {speed}");
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0);
    }
}
