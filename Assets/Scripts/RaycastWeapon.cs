using UnityEngine;
using System.Collections;

public class RaycastWeapon : MonoBehaviour
{
    // public static RaycastWeapon Instance { get; private set; }
    public float range = 100f;
    public Camera fpsCamera;
    public Light muzzleFlashLight;
    public float flashTime = 0.03f;
    [SerializeField] private AudioSource weaponAudioSource;
    public AudioClip shootClip;
    public AudioClip emptyClip;

    // private void Awake()
    // {
    //     Instance = this;
    // }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Inventory.Instance.ammoCount > 0)
        {
            Shoot();
        }
        else if (Input.GetMouseButtonDown(0) && Inventory.Instance.ammoCount <= 0)
        {
            NoAmmoAudio();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;

        FireAudio();
        Inventory.Instance.use("ammo", 1);
        if (muzzleFlashLight != null)
        {
            StartCoroutine(MuzzleFlash());
        }

        if (Physics.Raycast(
            fpsCamera.transform.position,
            fpsCamera.transform.forward,
            out hit,
            range,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore))
        {
            // Debug.Log("hit: " + hit.collider.name);

            if (hit.collider.TryGetComponent<Option>(out Option option))
            {
                option.RaycastHitOption();
            }
        }
    }

    private IEnumerator MuzzleFlash()
    {
        // Debug.Log("Muzzle flash!");
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(flashTime);
        muzzleFlashLight.enabled = false;
    }

    public void FireAudio()
    {
        weaponAudioSource.PlayOneShot(shootClip);
    }

    public void NoAmmoAudio()
    {
        weaponAudioSource.PlayOneShot(emptyClip);
    }
}