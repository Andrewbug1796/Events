using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public UnityEvent<int> OnAmmoChanged = new UnityEvent<int>();
    public UnityEvent OnGunFired = new UnityEvent();

    // references
    [SerializeField] Transform gunBarrelEnd;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator anim;

    // stats
    [SerializeField] int maxAmmo;
    [SerializeField] float timeBetweenShots = 0.1f;

    // private variables
    int ammo;
    float elapsed = 0;

    void Start()
    {
        ammo = maxAmmo;
        OnAmmoChanged.Invoke(ammo);
    }

    void Update()
    {
        elapsed += Time.deltaTime;
    }

    public bool AttemptFire()
    {
        if (ammo <= 0)
        {
            return false;
        }

        if (elapsed < timeBetweenShots)
        {
            return false;
        }

        Debug.Log("Bang");
        Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        OnAmmoChanged.Invoke(ammo);
        OnGunFired.Invoke();

        return true;
    }

    public void AddAmmo(int amount)
    {
        ammo += amount;

        if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }

        OnAmmoChanged.Invoke(ammo);
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
}