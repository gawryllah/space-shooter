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

    private bool autoShooted;



    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();

        hpBarSlider = hpBar.GetComponent<Slider>();
        hpBarSlider.maxValue = hp;
        hpBarSlider.value = hp;
        autoShooted = false;
        StartCoroutine(showOnScene(4f));
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(collision.gameObject);
            takeDmg();
        }
    }

    private void shoot()
    {
        Instantiate(weapon, transform.position, Quaternion.identity);
    }


    private void playShootingAnim()
    {
        animator.Play("bossAttacking", 0, 0);
    }

    private void takeDmg()
    {
        hp--;
        hpBarSlider.value = hp;
        if (hp <= 0)
            GameManager.instance.playerWon();
    }

    private IEnumerator attack()
    {

        while (GameManager.instance.isGameOn)
        {
            yield return new WaitForSecondsRealtime(Random.Range(2f, 5f));
            var value = Random.Range(0f, 1f);

            if (value <= 0.65f)
            {
                playShootingAnim();
                shoot();
                autoShooted = false;
            }
            else if (value > 0.65f && value <= 0.9f)
            {
                playShootingAnim();
                shoot();
                yield return new WaitForSecondsRealtime(0.15f);
                playShootingAnim();
                shoot();
                autoShooted = false;
            }
            else if (!autoShooted)
            {
                autoShooted = true;
                StartCoroutine(autoShooting(7.5f));

            }
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

    IEnumerator showOnScene(float duration)
    {
        yield return new WaitUntil(() => FindObjectsOfType<EnemyScript>().Length == 0);

        PlayerController.canShoot = false;
        float time = 0;
        Vector2 startPosition = new Vector2(10f, 0f);
        Vector2 targetPosition = new Vector2(7.75f, transform.position.y);
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;


        StartCoroutine(attack());
        PlayerController.canShoot = true;
        StartCoroutine(LerpPosition(new Vector2(transform.position.x, Random.Range(-4f, 3.65f))));
        
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
