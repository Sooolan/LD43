﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pistol : Wepond {
    [SerializeField] private int maxLoadedAmmo = 7;
    [SerializeField] private int extraAmmo = 21;
    [SerializeField] private bool unlimitedAmmo = false;

    private int shotsFired = 0;

    public override int CheckMagasine()
    {
        return loadedAmmo;
    }

    public override int CheckTotalBullets()
    {
        return loadedAmmo + reserveAmmo;
    }

    public override bool ReloadAmmo()
    {
        if (!unlimitedAmmo)
        {
            if (reserveAmmo > 0)
            {
                //play anim and reload gun after x time
                if (reserveAmmo > maxLoadedAmmo)
                {
                    reserveAmmo -= maxLoadedAmmo;
                    loadedAmmo = maxLoadedAmmo;
                    Debug.Log("Reserve Ammo: " + reserveAmmo);
                }
                else
                {
                    loadedAmmo = reserveAmmo;
                    reserveAmmo = 0;
                    Debug.Log("Reserve Ammo: " + reserveAmmo);
                }
                return true;
            }
            else
            {
                //out of ammo
                return false;
            }
            
        }
        return true;
    }

    public override void Attack(Player player, Vector3 spawnPoint)
    {
        if (loadedAmmo > 0)
        {
            loadedAmmo--;
            GameObject b = Instantiate(bulletPrefab, spawnPoint, Quaternion.identity) as GameObject;
            player.CmdSpawnBullet(b);
        }
        else
        {
            if (!ReloadAmmo())
            {
                Debug.Log("ALL OUT OFF AMMO");
            }

        }
    }
    
    void Start () {
        reserveAmmo = extraAmmo;
        loadedAmmo = maxLoadedAmmo;

    }




}