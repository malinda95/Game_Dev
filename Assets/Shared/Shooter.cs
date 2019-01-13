using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] float rateOfFire;
    [SerializeField] Transform hand;
    [SerializeField] Projectile projectile;
    [SerializeField] AudioController audioReload;
    [SerializeField] AudioController audioFire;
    // [HideInInspector]
    Transform muzzel;

    public WeaponReloader reloader;

    float nextFireAllowed;
    public bool canFire;

    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    void Awake()
    {
        muzzel = transform.Find("Model/Muzzle");
        reloader = GetComponent<WeaponReloader>();

    }

    public void Reload(){
        if (reloader == null)
            return;
        reloader.Reload();
        audioReload.play();
    }

    public virtual void Fire()
    {
     
        canFire = false;

        if (Time.time < nextFireAllowed)
            return;

        if (reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundsRemainingInClip == 0)
                return;
            reloader.TakeFromClip(1);
        }

        nextFireAllowed = Time.time + rateOfFire;

        //instantiate the projectile;
        Instantiate(projectile, muzzel.position, muzzel.rotation);
        audioFire.play();
        canFire = true;
    }
}
