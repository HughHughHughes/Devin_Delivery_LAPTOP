using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Rocket_Projectile : MonoBehaviour
{
    GameObject refPlayer;
    GameObject refToCameraHolder; //camera/shoot/gun stuff
    public Transform cameraRot; //make the rocket shoot out the same angles as cam facing.
    public GameObject explosion;
    public float explosionForce; // CURRENTLY EFFECTS HOW HIGH PLAYER AND HOW FAST PLAYER CAN GO (CHANGE IN FINAL BUILD)
    public float radius;

    public float distFromPlayer;



    private void Awake()
    {
        refPlayer = GameObject.Find("Player");
        refToCameraHolder = GameObject.Find("CameraHolder");
        cameraRot = transform.Find("Camera");
        explosionForce = refToCameraHolder.GetComponent<RocketLauncher>().chargeExplosionForce;
        Quaternion.Euler(0, 0, 0);
        this.transform.Rotate(0, 0, 0);
    }
    private void Start()
    {
        refPlayer = GameObject.Find("Player");
        refToCameraHolder = GameObject.Find("CameraHolder");
        explosionForce = refToCameraHolder.GetComponent<RocketLauncher>().chargeExplosionForce;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Update()
    {
        //SORT ROTATION TRAVELLING
        var oldPosition = this.transform.position;
        // << then you change the cube parent's position here
        var newPosition = this.transform.position;

        var headingDirection = (newPosition - oldPosition).normalized;

        this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, headingDirection);

        distFromPlayer = Vector3.Distance(refPlayer.transform.position, this.transform.position);
        Destroy(gameObject, 20);
    }

    private void OnCollisionEnter(Collision collision)
    {

        // OLD EXPLOSION ON A SPHERE RADIUS

        if (collision.collider.tag == "Ground_Yellow")
        {


            GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(_explosion, 3);
            knockBack();
            Destroy(gameObject);

            if (radius >= Mathf.Abs(distFromPlayer))
            {
                //enable a bool in here which resets the player rb.addforce y to 0;
                refPlayer.GetComponent<PlayerMovementFP>().inAirTime = 0;
                refPlayer.GetComponent<PlayerMovementFP>().currentDownForceWhenInAir = 0;
                refPlayer.GetComponent<PlayerMovementFP>().tickyTimer = 0;
                refPlayer.GetComponent<PlayerMovementFP>().youShotAndHitYou = false;

                refPlayer.GetComponent<PlayerMovementFP>().m_oneTime = true;
            }

        }

        if (collision.collider.tag == "Transparent_YELLOW")
        {


            GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(_explosion, 3);
            knockBack();
            Destroy(gameObject);

            if (radius >= Mathf.Abs(distFromPlayer))
            {
                //enable a bool in here which resets the player rb.addforce y to 0;
                refPlayer.GetComponent<PlayerMovementFP>().inAirTime = 0;
                refPlayer.GetComponent<PlayerMovementFP>().currentDownForceWhenInAir = 0;
                refPlayer.GetComponent<PlayerMovementFP>().tickyTimer = 0;
                refPlayer.GetComponent<PlayerMovementFP>().youShotAndHitYou = false;

                refPlayer.GetComponent<PlayerMovementFP>().m_oneTime = true;
            }

        }

        if (collision.collider.tag == "Ground_Yellow_Add_Force_Right")
        {


            GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(_explosion, 3);
            knockBack();
            Destroy(gameObject);

            if (radius >= Mathf.Abs(distFromPlayer))
            {
                //enable a bool in here which resets the player rb.addforce y to 0;
                refPlayer.GetComponent<PlayerMovementFP>().inAirTime = 0;
                refPlayer.GetComponent<PlayerMovementFP>().currentDownForceWhenInAir = 0;
                refPlayer.GetComponent<PlayerMovementFP>().tickyTimer = 0;
                refPlayer.GetComponent<PlayerMovementFP>().youShotAndHitYou = false;

                refPlayer.GetComponent<PlayerMovementFP>().m_oneTime = true;

                refPlayer.GetComponent<PlayerMovementFP>().hitPlatAddForceRight = true;
            }

        }

        if (collision.collider.tag == "Ground_Yellow_Add_Force_Left")
        {


            GameObject _explosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(_explosion, 3);
            knockBack();
            Destroy(gameObject);

            if (radius >= Mathf.Abs(distFromPlayer))
            {
                //enable a bool in here which resets the player rb.addforce y to 0;
                refPlayer.GetComponent<PlayerMovementFP>().inAirTime = 0;
                refPlayer.GetComponent<PlayerMovementFP>().currentDownForceWhenInAir = 0;
                refPlayer.GetComponent<PlayerMovementFP>().tickyTimer = 0;
                refPlayer.GetComponent<PlayerMovementFP>().youShotAndHitYou = false;

                refPlayer.GetComponent<PlayerMovementFP>().m_oneTime = true;

                refPlayer.GetComponent<PlayerMovementFP>().hitPlatAddForceLeft = true;
            }

        }

        if (collision.collider.tag == "Transparent_RED")
        {



        }

        if (collision.collider.tag == "Ground_Red")
        {


        }

        if (collision.collider.tag == "RED")
        {


        }


        else
        {
            Destroy(gameObject);
        }

    }
    void knockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);


            }

        }

    }
}
