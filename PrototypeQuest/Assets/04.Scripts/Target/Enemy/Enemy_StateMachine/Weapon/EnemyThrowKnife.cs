using System.Collections;
using UnityEngine;

public class EnemyThrowKnife : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform knifeModel;
    private Transform player;

    private float speed;
    private Vector3 direction;

    [SerializeField] private float timer;

    public void KnifeSetup(float speed, Transform player, float timer)
    {
        this.speed = speed;
        this.player = player;
        this.timer = timer;
    }

    private void Start()
    {
        StartCoroutine(ReturnKnifeAfterTime(5f));
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
            direction = player.position + Vector3.up - transform.position;

        rb.velocity = direction.normalized * speed;

        transform.forward = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null)
        {
            GameObject newFx = ObjectPool.instance.GetObject(particle);
            newFx.transform.position = transform.position;

            ObjectPool.instance.ReturnObject(gameObject);
            ObjectPool.instance.ReturnObject(newFx, 1f);
        }
    }

    private IEnumerator ReturnKnifeAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        ObjectPool.instance.ReturnObject(gameObject);
    }
}
