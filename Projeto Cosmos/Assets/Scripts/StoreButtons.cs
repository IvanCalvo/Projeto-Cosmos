using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoreButtons : MonoBehaviour
{
    public PlayerStats player;
    //public ShipMovement ship;

    public void buyShield()
    {
        if(player.money >= 50 && PlayerPrefs.GetInt("HasShield") == 0)
        {
            player.buyShield();
            player.money -= 50;
        }
    }

    public void buyBoost()
    {
        if(player.money >= 50)
        {
            player.money -= 50;
            PlayerPrefs.SetInt("maxBoostValue", PlayerPrefs.GetInt("maxBoostValue") * 2);
            PlayerPrefs.SetInt("BoostRefuelVelocity", PlayerPrefs.GetInt("BoostRefuelVelocity") + 1);
        }
    }

    public void BuyBetterDrops()
    {
        if (player.money >= 200)
        {
            player.money -= 200;
            PlayerPrefs.SetInt("DropMultiplier", PlayerPrefs.GetInt("DropMultiplier") + 1);
        }
    }
    
    public void BuyMissiles()
    {
        if (player.money >= 300)
        {
            player.money -= 300;
            PlayerPrefs.SetInt("HasMissileGun", 1);
            if(PlayerPrefs.GetInt("HasMissileGun") == 1)
            {
                ArmaRay gunScript = GameObject.FindGameObjectWithTag("Gun").GetComponent<ArmaRay>();
                gunScript.extraAmmo += 30;
            }
        }
    }
}
