using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider trig)
    {
        if (trig.GetComponent<Collider>().tag == "Ground")
        {
        }

        if (trig.GetComponent<Collider>().name == "Portal - Return_To_Main_St")
        {
            //print("You Touched Portal");
        }

        
    }
    private void OnTriggerStay(Collider trig)
    {
        if (trig.GetComponent<Collider>().tag == "Ground")
        {
            //transform.parent = trig.transform;
        }
    }

    private void OnTriggerExit(Collider trig)
    {
        transform.parent = null;
    }
}
