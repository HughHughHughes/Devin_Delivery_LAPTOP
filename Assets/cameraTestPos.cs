using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTestPos : MonoBehaviour
{
    Camera cam1;
    float x, y, z;

    void Start()
    {
        cam1.transform.rotation = Quaternion.Euler(55, 23, 44);
    }

    // Update is called once per frame
    void Update()
    {
        cam1.transform.rotation = Quaternion.Euler(55, 23, 44);
    }
}
