using System.Collections.Generic;
using UnityEngine;

public class MMBackGroundController : MonoBehaviour
{

    public GameObject bg1, bg2, bg3;
    public List<GameObject> backgroundObj;
    float bgScrollingSpeed = 1.1f;

    private void FixedUpdate()
    {
        backgroundMovment();
    }

    private void backgroundMovment()
    {
        bg1.transform.position -= new Vector3(bgScrollingSpeed * Time.deltaTime, 0f);
        bg2.transform.position -= new Vector3(bgScrollingSpeed * Time.deltaTime, 0f);
        bg3.transform.position -= new Vector3(bgScrollingSpeed * Time.deltaTime, 0f);

        if (bg1.transform.position.x < -14f)
        {
            var bgObj = Instantiate(backgroundObj[(int)Random.Range(0, backgroundObj.Count)], new Vector3(15f, 0), Quaternion.identity);

            Destroy(bg1);

            bg1 = bg2;
            bg2 = bg3;
            bg3 = bgObj;
        }

    }
}
