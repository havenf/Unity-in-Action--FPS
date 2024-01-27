using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    private Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnGUI()
    {
        if (hitPoint != null)
        {
            GUI.Label(new Rect(10, 10, 100, 20), hitPoint.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera != null && Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit " + hit.point);
                StartCoroutine(SphereIndicator(hit.point));
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = hit.point - _camera.transform.position;
                    rb.AddForce(forceDirection.normalized * 1000f);
                }
                hitPoint = hit.point;
            }
        }
    }

    //  Coroutines are processes specifically denoted by the use of IEnumerator.
    //  StartCoroutine() above takes in IEnumerator type parameter to run a task 
    //  called SphereIndicator below. 
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
        hitPoint = new Vector3();
    }
}