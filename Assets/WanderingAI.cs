using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
    private float speed = 1.0f;
    private float obstacleRange = 1.0f;
    private bool _alive;

    void Start()
    {
        _alive = true;
    }

    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        Ray ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * obstacleRange, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, obstacleRange))
        {
            if (hit.collider.gameObject != gameObject)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }

        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (_fireball == null)
                {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position =
                        transform.TransformPoint(Vector3.forward * 1.5f);
                    _fireball.transform.rotation = transform.rotation;
                }
            }
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
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



