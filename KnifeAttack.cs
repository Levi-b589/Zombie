using UnityEngine;
using UnityEngine.InputSystem;

public class KnifeAttack : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float range = 2f;
    [SerializeField] private int damage = 50;

    private bool knifeEquipped;

    //checks if you have the knife and equipped
    public void SetKnifeEquipped(bool equipped)
    {
        knifeEquipped = equipped;
    }

    //Allows the knife to be count as a hit
    public void OnFire(InputValue value)
    {
        if (!value.isPressed || !knifeEquipped)
        {
            return;
        }

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, range, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            zombie_ai zombie = hit.collider.GetComponentInParent<zombie_ai>();

            if (zombie != null)
            {
                zombie.Hit(damage);
            }
                
        }
    }
}