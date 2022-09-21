using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoreButtons : MonoBehaviour
{
    public PlayerStats player;

    public void buyShield()
    {
        if(player.money >= 50 && !player.hasShield)
        {
            player.buyShield();
            player.money -= 50;
        }
    }
}
