using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 1.0f;
    public int damage = 1;

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerCharacter>().Hurt(damage);
        }
        else
        {
            StartCoroutine(Extinguish());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("Player hit");
        }
    }

    private IEnumerator Extinguish()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
        Debug.Log("Fireball extinguished");
    }
}