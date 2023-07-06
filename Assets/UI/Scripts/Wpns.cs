using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wpns : MonoBehaviour
{
    Animator wpnHUDanim;
    PlayerStats pStats;
    int reloadLInd;
    int baseLINd;
    void Start()
    {
        wpnHUDanim = GetComponent<Animator>();
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        reloadLInd = wpnHUDanim.GetLayerIndex("ReloadingWand");
        baseLINd = wpnHUDanim.GetLayerIndex("Base Layer");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
            wpnHUDanim.SetTrigger("ChangeWpn");


        if (pStats.fireBallsCount >= 3)
        { 
            wpnHUDanim.SetLayerWeight(reloadLInd, 1);
            wpnHUDanim.SetLayerWeight(baseLINd, 0);
        }
        else
        {
            wpnHUDanim.SetLayerWeight(reloadLInd, 0);
            wpnHUDanim.SetLayerWeight(baseLINd, 1);
        }
    }
}
