using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{


    public GameObject bg1, bg2, bg3;
    public List<GameObject> backgroundObj;

    public List<Sprite> sprites;
    public GameObject planetPlaceHolder;
    public GameObject asteroid;

    public GameObject obstaclePrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(backgroundParticles());
    }

    private void FixedUpdate()
    {
        if(GameManager.instance.isGameOn)
            backgroundMovment();
    }


    private void backgroundMovment()
    {
        bg1.transform.position -= new Vector3(GameManager.instance.bgScrollingSpeed * Time.deltaTime, 0f);
        bg2.transform.position -= new Vector3(GameManager.instance.bgScrollingSpeed * Time.deltaTime, 0f);
        bg3.transform.position -= new Vector3(GameManager.instance.bgScrollingSpeed * Time.deltaTime, 0f);

        if(bg1.transform.position.x < -14f)
        {
            var bgObj = Instantiate(backgroundObj[(int)Random.Range(0, backgroundObj.Count)], new Vector3(14f, 0), Quaternion.identity);

            Destroy(bg1);

            bg1 = bg2;
            bg2 = bg3;
            bg3 = bgObj;
        }
    }

    private IEnumerator backgroundParticles()
    {
        while (GameManager.instance.isGameOn)
        {
            yield return new WaitForSecondsRealtime(Random.Range(10f, 20f));
            if (Random.Range(0f, 1f) < 0.68f)
            {
                GameObject go = Instantiate(planetPlaceHolder, new Vector3(12, GameManager.instance.getRandomHeight() + 1), Quaternion.identity);
                float scale = Random.Range(0.6f, 1f);
                go.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count - 1)];
                go.transform.localScale = new Vector3(scale, scale, scale);

                Debug.Log("Spawned planet");
            }
            else
            {
                if (Random.Range(0f, 1f) > 0.55f)
                {
                    Instantiate(obstaclePrefab, new Vector3(12, GameManager.instance.getRandomHeight()), Quaternion.identity);

                    Debug.Log("Spawned obstacle");
                }
                else
                {
                    Instantiate(asteroid, new Vector3(12, GameManager.instance.getRandomHeight() + 1), Quaternion.identity);
                    Debug.Log("Spawned asteroid");

                }

            }

        }

    }
}
