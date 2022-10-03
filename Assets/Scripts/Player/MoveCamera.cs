using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//apply to camera holder not on player and parent of camera.
public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;

    private void Awake()
    {
       
    }
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
