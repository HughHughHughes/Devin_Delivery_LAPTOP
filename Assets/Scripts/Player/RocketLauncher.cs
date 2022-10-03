using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public GameObject gunPos; //pos and transform location // Transforms to act as start and end markers for the journey.
    public bool gunVisuals; // if this is true, set the rend on or off in the rocketLaucnher_setchildren
    public Renderer gunvis;
    public Transform gunPosA; //start marker (offscreen)
    public Transform gunPosB; //start marker (onscreen)
    public Transform muzzle_flash_spawn_loc;
                              // Transforms to act as start and end markers for the journey.


    // Movement speed in units per second.
    public float speed = 0.5F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    float moveY;
    public GameObject refToGameManager;

    public GameObject[] objPrefab;
    public Transform Spawn_Projectile_Here;
    public float shootForce = 20f;

    public float chargeExplosionForce; // copy and paste over the explosion force of bomb projectile.
    float chargeDelayTimer;

    int blobbAmmoType = 0;

    public float blobmagazine = 1; // if blomb magazine > 0 then u can keep shooting blobbs out
    public float maxBlobMagazine = 100;
    public bool shootIntervalDelay;
     float shootIntTimer;

    public bool inAirWhenShot;

    private void Awake()
    {
      
        player = GameObject.Find("Player");
        refToGameManager = GameObject.Find("GameManager");
        blobbAmmoType = 0;
        //shootForce = 50;
     
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(gunPosA.position, gunPosB.position);

        chargeExplosionForce = 100f;

    }
  
    void Start()
    {
        chargeExplosionForce = 100f;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(gunPosA.position, gunPosB.position);
    }

    private void Update()
    {
        if (refToGameManager.GetComponent<InGameManager>().playerHP > 0 && refToGameManager.GetComponent<InGameManager>().playerWins == false)
        {


            if (refToGameManager.GetComponent<InGameManager>().pauseScreenActive == false)
            {
                if (refToGameManager.GetComponent<InGameManager>().rockLauncherEquiped == false)
                {
                    speed = 0.5f;
                    gunvis.enabled = false;
                    gunVisuals = false;
                    gunPos.transform.position = gunPosA.transform.position;

                    //gunPos.SetActive(false);
                }

                if (refToGameManager.GetComponent<InGameManager>().rockLauncherEquiped == true)
                {
                    gunvis.enabled = enabled;
                    gunVisuals = true; // for children
                    GunLoadingAndAnim(); //place in hand, once this is done and locked in, player can shoot.

                    if (shootIntervalDelay == true)
                    {
                        shootIntTimer += Time.deltaTime;
                        if (shootIntTimer >= 0.33f)
                        {
                            shootIntTimer = 0;
                            shootIntervalDelay = false;
                        }
                    }

                    if (refToGameManager.GetComponent<InGameManager>().playerAmmo <= maxBlobMagazine)
                    {
                        refToGameManager.GetComponent<InGameManager>().playerAmmo += Time.deltaTime;
                    }

                }
            }
        }

        if (inAirWhenShot == true)
        {

        }
    }
    void GunLoadingAndAnim()
    {
        
        //if (gunVisuals)
        //{
        //    // Distance moved equals elapsed time times speed..
        //    float distCovered = (Time.time - startTime) * speed;

        //    // Fraction of journey completed equals current distance divided by total distance.
        //    float fractionOfJourney = distCovered / journeyLength;

        //    // Set our position as a fraction of the distance between the markers.
        //    gunPos.transform.position = Vector3.Lerp(gunPosA.position, gunPosB.position, fractionOfJourney);

        //}

        //after set amount of time enable below.
        GunPermanentlyEquipped();

    }
    void GunPermanentlyEquipped()
    {
        BlobbShootVelocity(); // needs to change when player is moving on y axis.

        if (refToGameManager.GetComponent<InGameManager>().playerAmmo >= 0 && shootIntervalDelay == false)
        {
            if (refToGameManager.GetComponent<InGameManager>().redAmmoCollected == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    shootIntervalDelay = true;
                    //refToGameManager.GetComponent<InGameManager>().playerAmmo -= 10;


                    chargeDelayTimer = 0;

                    if (blobmagazine > 0)
                    {
                        GameObject go = Instantiate(objPrefab[0], Spawn_Projectile_Here.position, Quaternion.identity);
                        //shootForce = 50;
                        Rigidbody rb = go.GetComponent<Rigidbody>();
                        rb.velocity = cam.transform.forward * shootForce;
                    }

                    if (blobmagazine > 0)
                    {
                        GameObject FLASH = Instantiate(objPrefab[2], muzzle_flash_spawn_loc.position, Quaternion.identity);
                        Destroy(FLASH, 1f);
                    }

                }
            }

            //YELLOW
            if (refToGameManager.GetComponent<InGameManager>().yellowAmmoCollected == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shootIntervalDelay = true;
                    //refToGameManager.GetComponent<InGameManager>().playerAmmo -= 10;


                    chargeDelayTimer = 0;

                    if (blobmagazine > 0)
                    {
                        GameObject go = Instantiate(objPrefab[1], Spawn_Projectile_Here.position, Quaternion.identity);
                        //shootForce = 50;
                        Rigidbody rb = go.GetComponent<Rigidbody>();
                        rb.velocity = cam.transform.forward * shootForce;
                    }

                    if (blobmagazine > 0)
                    {
                        GameObject FLASH = Instantiate(objPrefab[2], muzzle_flash_spawn_loc.position, Quaternion.identity);
                        Destroy(FLASH, 1f);
                    }


                }
            }
        }
    }
    void BlobbShootVelocity()
    {

    }
}