using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
    void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;

            _enemy.transform.position = new Vector3(0.5f, 1.1f, 0);

            float angle = Random.Range(0, 360);

            _enemy.transform.Rotate(0, angle, 0);
        }
    }
}
