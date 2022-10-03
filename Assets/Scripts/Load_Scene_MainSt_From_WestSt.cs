using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Scene_MainSt_From_WestSt : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider trig)
    {
        if (trig.GetComponent<Collider>().tag == "Player")
        {
            print("You Triggered Player");
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

        if (trig.GetComponent<Collider>().name == "Player")
        {
           // print("You Triggered Player");
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //print("You Collided With Player");
        }

        
    }
}
