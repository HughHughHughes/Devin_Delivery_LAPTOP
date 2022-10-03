using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFP : MonoBehaviour
{
    GameObject gamemanager;

    [Header("Orientation")]
    [SerializeField] Transform orientation;

    [SerializeField] Transform cam;
    [SerializeField] Transform player_Hand_Pivot;
    [SerializeField] Transform player_BodyGfx_Pivot;


    public float changeSenseSliderValue;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;




    private void Awake()
    {
        
        gamemanager = GameObject.Find("GameManager");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        changeSenseSliderValue = 180f;


    }
    private void Start()
    {
        
    }
    private void LateUpdate() //changed to lateupdate 2022, moving physics objs look smoother, still not perfect
    {
       
        MyInput();
        sensX = changeSenseSliderValue;
        sensY = changeSenseSliderValue;

    }
    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        player_Hand_Pivot.rotation = cam.transform.rotation;
        player_BodyGfx_Pivot.rotation = cam.transform.rotation;
    }

}
