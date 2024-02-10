using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Material enemyMaterial;

    private GameObject _enemy;

    Renderer enemyRenderer;

    void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = new Vector3(0.5f, 1.1f, 0);

            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);

            float randomHeight = Random.Range(0.5f, 2.0f);
            _enemy.transform.localScale = new Vector3(1, randomHeight, 1);

            Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            _enemy.GetComponent<Renderer>().material.color = randomColor;

            enemyRenderer = _enemy.GetComponent<Renderer>();
            enemyRenderer.material = enemyMaterial;
        }
    }
}

