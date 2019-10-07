using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWithProp : MonoBehaviour
{
    public int ammoCount = 0;
    
    public enum GunType { PISTOL = 3, SHOTGUN = 4, ASSAULT_RIFLE = 5}
    public GunType gunType;
    public float bulletSpeed = 0;
    public GameObject bulletPrefab;
    public float shootCooldown = 0;
    public bool canShoot = true;
    private bool stopShooting = false;
    public float muzzleFlashTime = 0.1f;
    public float autoShotCooldown = 0;
    // Start is called before the first frame update

    public void startShoot(Vector2 direction)
    {
        if (canShoot && ammoCount > 0)
        {
            switch (gunType)
            {
                case GunType.PISTOL:
                    createBullet(direction);
                    --ammoCount;
                    break;
                case GunType.SHOTGUN:
                    createBullet(direction + Vector2.Perpendicular(direction), 1);
                    createBullet(direction, 2);
                    createBullet(direction - Vector2.Perpendicular(direction),3);
                    --ammoCount;
                    break;
                case GunType.ASSAULT_RIFLE:
                    StartCoroutine(autoFire(direction));
                    break;
                default:
                    return;
            }
        }
    }

    private void createBullet(Vector2 direction, int spawnPosNum = 1)
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        GameObject bulletSpawn = GameObject.Find(gunType == GunType.SHOTGUN ? "BulletSpawn" + spawnPosNum.ToString() : "BulletSpawn");
        newBullet.transform.position = bulletSpawn.transform.position;
        if((gunType == GunType.SHOTGUN && spawnPosNum == 2) || gunType != GunType.SHOTGUN)
        {
            StartCoroutine(FlashMuzzle(bulletSpawn));
        }

        newBullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
    }

    public void setStopShooting(bool var)
    {
        stopShooting = var;
    }

    IEnumerator autoFire(Vector2 direction)
    {
        while (!stopShooting)
        {
            Debug.Log("PEW");
            createBullet(direction);
            --ammoCount;
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(autoShotCooldown);        
        }

        stopShooting = false;
        yield return null;
    }

    IEnumerator FlashMuzzle(GameObject bulletSpawn)
    {
        bulletSpawn.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(muzzleFlashTime);
        bulletSpawn.GetComponent<SpriteRenderer>().enabled = false;

    }
}
