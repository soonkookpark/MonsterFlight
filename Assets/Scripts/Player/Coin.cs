using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    int coinCount = 0;

    public void Use(GameObject go)
    {
        coinCount++;
        switch(coinCount)
        {
            case 1:
                PlayerShoot.Instance.UnlockAIShot = true;
                break;
            case 2:
                PlayerShoot.Instance.UnlockChargeShot = true;
                break;
            default:
                PlayerSetting player = go.GetComponent<PlayerSetting>();
                player.LifeUp();
                break;
        }
        Destroy(gameObject);
    }
}
