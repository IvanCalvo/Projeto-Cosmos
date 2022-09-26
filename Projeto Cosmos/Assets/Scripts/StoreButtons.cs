using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoreButtons : MonoBehaviour
{
    public PlayerStats player;
    public ShipMovement ship;

    public void buyShield()
    {
        if(player.money >= 50 && !player.hasShield)
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
            ship.max_boost_value *= 2;
        }
    }
}
