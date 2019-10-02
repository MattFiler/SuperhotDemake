using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWithProp : MonoBehaviour
{
    public int ammoCount = 0;
    
    public enum GunType { PISTOL, SHOTGUN, ASSAULT_RIFLE}
    public GunType gunType;
    public float bulletSpeed = 0;
    public GameObject bulletPrefab;
    public float shootCooldown = 0;
    public bool canShoot = true;

    // Start is called before the first frame update

    public void startShoot(Vector2 direction)
    {
        if (canShoot && ammoCount > 0)
        {
            switch (gunType)
            {
                case GunType.PISTOL:
                    createBullet(direction);
                    break;
                case GunType.SHOTGUN:
                    createBullet(direction + Vector2.Perpendicular(direction), 1);
                    createBullet(direction, 2);
                    createBullet(direction - Vector2.Perpendicular(direction),3);
                    break;
                case GunType.ASSAULT_RIFLE:
                    break;
                default:
                    return;
            }
            --ammoCount;
        }
    }

    private void createBullet(Vector2 direction, int spawnPosNum = 1)
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = GameObject.Find(gunType == GunType.SHOTGUN? "BulletSpawn" + spawnPosNum.ToString() : "BulletSpawn").transform.position;
        newBullet.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;
    }
}
