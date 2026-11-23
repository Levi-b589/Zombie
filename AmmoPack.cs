using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [SerializeField] private int ammoPerVisit = 30;
    private bool readyToCollect = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!readyToCollect)
        {
            return;
        }
            

        GunShoot gun = other.GetComponentInParent<GunShoot>();

        if (gun == null)
        {
            return;
        }
        

        gun.AddReserveAmmo(ammoPerVisit);
        readyToCollect = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<GunShoot>() != null)
        {
            readyToCollect = true;
        }
    }
}