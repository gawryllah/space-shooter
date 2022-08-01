using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

    Animator animator;

    public GameObject weapon;

    public GameObject hpBar;
    private Slider hpBarSlider;
    public int hp;

    public bool canMove;
    private float duration;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(attack());
        hpBarSlider = hpBar.GetComponent<Slider>();
        hpBarSlider.maxValue = hp;
        hpBarSlider.value = hp;
        StartCoroutine(LerpPosition(new Vector2(transform.position.x, Random.Range(-4f, 3.65f))));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(collision.gameObject);
            hp--;
            hpBarSlider.value = hp;
        }
    }

    private void shoot()
    {
        Instantiate(Instantiate(weapon, transform.position, Quaternion.identity));
    }

    private void playShootingAnim()
    {
        animator.Play("bossAttacking", 0, 0);
    }

    private IEnumerator attack()
    {
        while (GameManager.instance.isGameOn)
        {
            yield return new WaitForSecondsRealtime(Random.Range(2f, 5f));
            var value = Random.Range(0f, 1f);

            if(value <= 0.65f)
            {
                playShootingAnim();
                shoot();
            }
            else if(value > 0.65f && value <= 0.9f)
            {
                playShootingAnim();
                shoot();
                yield return new WaitForSecondsRealtime(0.15f);
                playShootingAnim();
                shoot();
            }
            else
            {
                StartCoroutine(autoShooting(7.5f));
            }
            Debug.Log($"Value: {value}");

         

        }
    }

    IEnumerator LerpPosition(Vector2 targetPosition)
    {
        while (GameManager.instance.isGameOn)
        {
            duration = Random.Range(1f, 2f);
            float time = 0;
            Vector2 startPosition = transform.position;
            while (time < duration)
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
                
            }
            transform.position = targetPosition;
            yield return new WaitForSecondsRealtime(Random.Range(1f, 2f));
            targetPosition = new Vector2(transform.position.x, Random.Range(-4f, 3.65f));

            if (Vector2.Distance(transform.position, targetPosition) < 2f)
                targetPosition = new Vector2(transform.position.x, Random.Range(-4f, 3.65f));
        }
    }

    IEnumerator autoShooting(float duration)
    {
   
        var end = Time.time + duration;
        while (Time.time < end && GameManager.instance.isGameOn)
        {
            playShootingAnim();
            shoot();
            yield return new WaitForSecondsRealtime(0.25f);
        }
        
    }


}
