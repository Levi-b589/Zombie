using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class pickupcontroller : MonoBehaviour
{

    //public ProjectileGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwarForce;

    public bool equipped;
    //add a inventory size so player can swap between items

    public static bool slotFull;


    //this is just for the start of the game
    //depending on if you start with a weapon or not 
    //implement a world value so depending on what
    //setting they are on they could have a weapon 
    public void Start()
    {
        if (!equipped)
        {
            //gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            //gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }
    

    //picking up a weapon
    private void PickUp()
    {

        equipped = true;
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        //gunScript.enabled = true;


    }
    
    //dropping the weapon 
    private void Drop()
    {

        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.linearVelocity = player.GetComponent<Rigidbody>().linearVelocity;

        //
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropForwardForce, ForceMode.Impulse);

        //this adds random range for throwing and spin 
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        ///gunScript.enabled = false;
    }
}
