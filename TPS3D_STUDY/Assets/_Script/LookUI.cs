using UnityEngine;

public class LookUI : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        if(cam == null)
        {
            //cam = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
            cam = Camera.main;
        }
    }

    void Update()
    {
        if (!cam)
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
                cam.transform.rotation * Vector3.up);
        }
    }
}
