using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    float speed = 1.75f;
    Color currColor;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currColor = sr.color;
        StartCoroutine(colorChanger());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3((speed * Time.deltaTime), 0f);


        if (transform.position.x < -10f)
        {
            
            Destroy(transform.gameObject);
            PlayerController.health -= 1;
            Debug.Log($"Zycia gracza: {PlayerController.health}");
        }
    }
    
    
    private IEnumerator colorChanger()
    {
        while (true) { 
            yield return new WaitForSecondsRealtime(1.5f);
            sr.color = Color.Lerp(currColor, Color.red, 1.25f);
            yield return new WaitForSecondsRealtime(1.5f);
            sr.color = Color.Lerp(Color.red, currColor, 1.25f);
        }
    }
    
}
