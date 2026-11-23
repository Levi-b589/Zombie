using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class pickupcontroller : MonoBehaviour
{

    //public ProjectileGun gunScript;
    public GunShoot gunShoot;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwarForce;

    public bool equipped;
    //add a inventory size so player can swap between items

    public static bool slotFull;

    public InventoryBarUI inventoryBar;
    public inventory_hotbar itemData;
    public KnifeAttack knifeAttack;
    private bool addedToInventory;


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

    //Allows you to click E to pick up and Q to drop
    private void Update()
    {
        if (player == null || Keyboard.current == null) return;

        Vector3 distanceToPlayer = player.position - transform.position;

        bool pressedE = Keyboard.current.eKey.wasPressedThisFrame;
        bool pressedQ = Keyboard.current.qKey.wasPressedThisFrame;

        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && pressedE && !slotFull)
        {
            PickUp();
        }

        if (equipped && pressedQ)
        {
            Drop();
        }
    }


    //picking up a weapon
    private void PickUp()
    {
        if (inventoryBar == null || itemData == null || !inventoryBar.AddItem(itemData))
        {
            return;
        }

        addedToInventory = true;

        if (itemData.item_type == itemType.Knife && knifeAttack != null)
        {
            knifeAttack.SetKnifeEquipped(true);
        }
            

        if (itemData.item_type == itemType.Gun1 && gunShoot != null)
        {
            gunShoot.SetHasGun(true);
        }
            
        equipped = true;
        gunShoot.SetHasGun(true);
        slotFull = true;

        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        rb.isKinematic = true;
        coll.isTrigger = true;

        //gunScript.enabled = true;


    }
    
    //dropping the weapon 
    private void Drop()
    {
        if (addedToInventory && inventoryBar != null && itemData != null)
        {
            inventoryBar.RemoveItem(itemData);
        }
            

        if (itemData.item_type == itemType.Knife && knifeAttack != null)
        {
            knifeAttack.SetKnifeEquipped(false);
        }
            

        if (itemData.item_type == itemType.Gun1 && gunShoot != null)
        {
            gunShoot.SetHasGun(false);
        }
           

        addedToInventory = false;
        equipped = false;
        gunShoot.SetHasGun(false);
        slotFull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.linearVelocity = player.GetComponent<Rigidbody>().linearVelocity;

        //
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwarForce, ForceMode.Impulse);

        //this adds random range for throwing and spin 
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        ///gunScript.enabled = false;
    }

    private void OnGUI()
    {
        if (player != null && !equipped && Vector3.Distance(player.position, transform.position) <= pickUpRange && !slotFull)
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 100, 250, 40), "Press E to pick up gun");
        }
    }
}
