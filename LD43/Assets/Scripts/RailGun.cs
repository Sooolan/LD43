﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : Wepond {
    [SerializeField] private int maxLoadedAmmo = 5;

    [SerializeField] private float shootSpeed = 2f;
    
    private Timer timer;

    public override void Attack(Player player, Vector3 spawnPos, Vector3 direction)
    {

        Animator anim = GetComponent<Animator>();
        if (timer.Time < 0)
        {
            Debug.Log(loadedAmmo);
            if (loadedAmmo > 0)
            {
                if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                {
                    anim.SetTrigger("Fire");
                    RaycastHit hit;
                    if (Physics.Raycast(player.playerCamera.transform.position, player.playerCamera.transform.forward, out hit, 150.0f))
                    {
                        player.CmdSpawnRail(1, spawnPos, hit.point);
                    }
                    else
                    {
                        player.CmdSpawnRail(1, spawnPos, spawnPos + direction * 150f);

                    }
                    timer.reset();
                    loadedAmmo--;
                }
            }
        }
    }

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
        return true;
    }

    public override void Reset()
    {
        loadedAmmo = maxLoadedAmmo;
        timer = new Timer(shootSpeed);
        timer.Time = -1;
    }


    // Use this for initialization
    void Start () {
        loadedAmmo = maxLoadedAmmo;
        timer = new Timer(shootSpeed);
        timer.Time = -1;
        
    }

    // Update is called once per frame
    void Update() {
        if (timer.Time > 0)
        {
            timer.tick(Time.deltaTime);
        }
    
	}
}
