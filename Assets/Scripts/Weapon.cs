using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
//     public static Weapon Instance { get; private set; }
//     public GameObject bulletPrefab;
//     public Transform bullerSpawnPoint;
//     public float bulletSpeed = 10f;
//     public float bulletLifetime = 3f;
//     // Update is called once per frame
//     public Inventory inventory; // Reference to the Inventory script
    
//     private void Awake()
//     {
//         Instance = this;
//     }
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Mouse0))
//         {
//             Shoot();
//         }
//     }

//     private void Shoot()
//     {
//         if (inventory.getCount("ammo") <= 0)
//         {
//             Debug.Log("No ammo left!");
//             return;
//         }
//         GameObject bullet = Instantiate(bulletPrefab, bullerSpawnPoint.position, Quaternion.identity);
//         bullet.GetComponent<Rigidbody>().AddForce(bullerSpawnPoint.forward.normalized * bulletSpeed, ForceMode.Impulse);
//         StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifetime));
//         inventory.use("ammo", 1);
//     }

//     private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         Destroy(bullet);
//     }
}
