using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    static int id = 0;

    static float speed = 8f;

    private bool alreadyHit = false;

    private void FixedUpdate()
    {
        
        transform.position -= new Vector3(speed * Time.deltaTime, 0);

        if(transform.position.x < -12f)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !alreadyHit)
        {
            Destroy(transform.gameObject);
            alreadyHit = true;
            PlayerController.takeDmg();
            Debug.Log($"id: {id}, bool: {alreadyHit}, hp: {PlayerController.health}");
            id++;
        }
    }
}
