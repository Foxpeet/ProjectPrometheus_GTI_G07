using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRay : MonoBehaviour
{
    public Camera cam;
    public float cameraRange = 2;
    public Ray ray; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = Vector3.forward;
        ray = new Ray(cam.transform.position, cam.transform.TransformDirection(cameraPosition * cameraRange));
        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(cameraPosition * cameraRange));
    }
}
