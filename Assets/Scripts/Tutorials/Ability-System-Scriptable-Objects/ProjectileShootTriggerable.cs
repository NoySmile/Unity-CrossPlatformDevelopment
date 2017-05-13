﻿using UnityEngine;

namespace Tutorials
{
    

public class ProjectileShootTriggerable : MonoBehaviour
{
    public Transform bulletSpawn; // Transform variable to hold the location where we will spawn our projectile

    [HideInInspector] public Rigidbody projectile; // Rigidbody variable to hold a reference to our projectile prefab

    [HideInInspector]
    public float projectileForce = 250f
        ; // Float variable to hold the amount of force which we will apply to launch our projectiles

    public void Launch()
    {
        //Instantiate a copy of our projectile and store it in a new rigidbody variable called clonedBullet
        var clonedBullet = Instantiate(projectile, bulletSpawn.position, transform.rotation);

        //Add force to the instantiated bullet, pushing it forward away from the bulletSpawn location, using projectile force for how hard to push it away
        clonedBullet.AddForce(bulletSpawn.transform.forward * projectileForce);
    }
    }
}