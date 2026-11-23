using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LineRenderer tracer;
    [SerializeField] private float range = 100f;
    [SerializeField] private int damage = 25;
    [SerializeField] private float tracerDuration = 0.05f;

    [SerializeField] private AudioSource shotAudio;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private pickupcontroller gunPickup;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private int startingReserveAmmo = 90;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private AudioSource reloadAudioSource;
    [SerializeField] private AudioClip reloadSound;

    private int ammoInMagazine;
    private int reserveAmmo;
    private bool isReloading;

    private Coroutine hideTracerCoroutine;
    private bool hasGun = false;
    public PlayerHealth playerHealth;


    private void Start()
    {
        ammoInMagazine = magazineSize;
        reserveAmmo = startingReserveAmmo;
        UpdateAmmoCounter();
    }

    public void SetHasGun(bool value)
    {
        hasGun = value;
    }

    //When the gun shoots, for tracer and Ammo Counter
    public void OnFire(InputValue value)
    {
        if (gunPickup == null || !gunPickup.equipped)
        {
            return;
        }
        if (!value.isPressed || isReloading || ammoInMagazine <= 0)
        {
            return;
        }

        ammoInMagazine--;
        UpdateAmmoCounter();

        if (!value.isPressed || gunPickup == null || !gunPickup.equipped)
        {
            return;
        }

        if (shotAudio != null && shotSound != null)
        {
            shotAudio.PlayOneShot(shotSound);
        }

        if (playerHealth != null && playerHealth.IsDead)
        {
            return;
        }
            
        if (!hasGun)
        {
            return;
        }
        if (!value.isPressed)
        {
            return;
        }

        if (playerCamera == null)
        {
            Debug.LogError("Assign the Player Camera in the Inspector.", this);
            return;
        }

        Vector3 start = muzzlePoint.position;
        Vector3 direction = playerCamera.transform.forward;

        bool hitSomething = Physics.Raycast(start, direction, out RaycastHit hit, range);

        Vector3 end = hitSomething? hit.point : start + direction * range;

        ShowTracer(start, end);

        if (hitSomething)
        {
            zombie_ai zombie = hit.collider.GetComponentInParent<zombie_ai>();

            if (zombie != null)
            {
                zombie.Hit(damage);
            }
        }
    }
    //Shows the bullet tracer
    private void ShowTracer(Vector3 start, Vector3 end)
    {
        if (tracer == null)
        {
            Debug.LogWarning("Assign a Line Renderer to Tracer in the Inspector.", this);
            return;
        }

        tracer.positionCount = 2;
        tracer.SetPosition(0, start);
        tracer.SetPosition(1, end);
        tracer.enabled = true;

        if (hideTracerCoroutine != null)
        {
            StopCoroutine(hideTracerCoroutine);
        }

        hideTracerCoroutine = StartCoroutine(HideTracerAfterDelay());
    }

    private IEnumerator HideTracerAfterDelay()
    {
        yield return new WaitForSeconds(tracerDuration);

        if (tracer != null)
        {
            tracer.enabled = false;
        }
    }

    //Allows the user to reload and makes a sound when it reloads
    public void OnReload(InputValue value)
    {
        if (gunPickup == null || !gunPickup.equipped)
        {
            return;
        }
        if (reloadAudioSource != null && reloadSound != null)
        {
            reloadAudioSource.PlayOneShot(reloadSound);
        }
        if (!value.isPressed || isReloading || ammoInMagazine == magazineSize || reserveAmmo <= 0)
        {
            return;
        }

        StartCoroutine(ReloadMagazine());
    }

    private IEnumerator ReloadMagazine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        int bulletsNeeded = magazineSize - ammoInMagazine;
        int bulletsToLoad = Mathf.Min(bulletsNeeded, reserveAmmo);

        ammoInMagazine += bulletsToLoad;
        reserveAmmo -= bulletsToLoad;

        isReloading = false;
        UpdateAmmoCounter();
    }
    //Updates ammo count
    private void UpdateAmmoCounter()
    {
        if (ammoText != null)
        {
            ammoText.text = $"{ammoInMagazine} / {reserveAmmo}";
        }
    }
    //Reserved ammo
    public void AddReserveAmmo(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        reserveAmmo += amount;
        UpdateAmmoCounter();
    }


}