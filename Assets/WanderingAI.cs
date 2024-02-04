using System.Collections;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private float speed = 1.0f;
    private float obstacleRange = 2.0f;
    private bool _alive;

    private void Start()
    {
        _alive = true;
        fireballPrefab = Resources.Load<GameObject>("Assets/Fireball.prefab");
        StartCoroutine(ShootFireballCoroutine());
    }

    private void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * obstacleRange, Color.red);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, obstacleRange) && hit.collider.gameObject != gameObject)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    private IEnumerator ShootFireballCoroutine()
    {
        while (_alive)
        {
            // if (fireballPrefab != null)
            // {
            GameObject fireball = Instantiate(fireballPrefab, transform.position, transform.rotation);
            fireball.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
            // }

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    public void ReactToHit()
    {
        _alive = false;
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}