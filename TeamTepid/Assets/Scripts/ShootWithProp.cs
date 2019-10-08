using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWithProp : MonoBehaviour
{
    
    public enum GunType { PISTOL = 3, SHOTGUN = 4, ASSAULT_RIFLE = 5}
    public GunType gunType;
    public float bulletSpeed = 0;
    public GameObject bulletPrefab;
    public float shootCooldown = 0;
    public bool canShoot = true;
    private bool stopShooting = false;
    public float muzzleFlashTime = 0.1f;
    public float autoShotCooldown = 0;

    private Vector2 shootDirection = Vector2.up;
    public Vector2 shootDirectionDefault = Vector2.right;

    // Start is called before the first frame update

    public void setShootDirection(Vector2 dir)
    {
        shootDirection = dir;
    }

    public void startShoot()
    {
        //Debug.Log(shootDirection);
        if (canShoot  /*&& shootDirection != Vector2.zero*/)
        {
            switch (gunType)
            {
                case GunType.PISTOL:
                    createBullet(shootDirection);
                    break;
                case GunType.SHOTGUN:
                    createBullet(shootDirection + Vector2.Perpendicular(shootDirection), 1);
                    createBullet(shootDirection, 2);
                    createBullet(shootDirection - Vector2.Perpendicular(shootDirection),3);
                    break;
                case GunType.ASSAULT_RIFLE:
                    StartCoroutine(autoFire());
                    break;
                default:
                    return;
            }
        }
    }

    private void createBullet(Vector2 direction, int spawnPosNum = 1)
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        GameObject bulletSpawn = transform.Find(gunType == GunType.SHOTGUN ? "BulletSpawn" + spawnPosNum.ToString() : "BulletSpawn").gameObject;
        newBullet.transform.position = bulletSpawn.transform.position;
        if((gunType == GunType.SHOTGUN && spawnPosNum == 2) || gunType != GunType.SHOTGUN)
        {
            StartCoroutine(FlashMuzzle(bulletSpawn));
        }
        newBullet.GetComponent<BulletCollision>().ignoreCollisionTag = GetComponent<PropInteraction>().pickedupObjTag;
        newBullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
    }

    public void setStopShooting(bool var)
    {
        stopShooting = var;
    }

    IEnumerator autoFire()
    {
        while (!stopShooting)
        {
            Debug.Log("PEW");
            createBullet(shootDirection);
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
