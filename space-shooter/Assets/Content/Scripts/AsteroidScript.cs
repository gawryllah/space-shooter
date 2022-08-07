using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public float rotationSpeed = 1f;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rotationSpeed = Random.Range(0.5f * rotationSpeed, 1.5f * rotationSpeed);
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isGameOn)
        {
            return;
        }

        if (GameManager.instance.isBossSpawned && !sr.isVisible)
            Destroy(gameObject);

        transform.position -= new Vector3((GameManager.instance.bgScrollingSpeed * Time.deltaTime) + 0.05f, 0f);

        transform.Rotate(new Vector3(0f, 0f, rotationSpeed));

        if (transform.position.x < -13f)
        {
            Destroy(transform.gameObject);
        }
    }
}
