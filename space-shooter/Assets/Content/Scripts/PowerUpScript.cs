using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(floating());
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3((GameManager.instance.bgScrollingSpeed * Time.deltaTime) + 0.01f, 0f);

        if (transform.position.x < -12f)
        {
            Destroy(transform.gameObject);
        }
    }


    private IEnumerator floating()
    {
        while (GameManager.instance.isGameOn)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            for (int i = 0; i < 30; i++)
            {
                transform.position -= new Vector3(0f, 0.01f);
                yield return new WaitForSecondsRealtime(0.02f);
            }

            yield return new WaitForSecondsRealtime(0.2f);

            for (int i = 0; i < 30; i++)
            {
                transform.position += new Vector3(0f, 0.01f);
                yield return new WaitForSecondsRealtime(0.02f);
            }
        }
    }


}
