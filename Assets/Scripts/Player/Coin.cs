using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    int coinCount = 0;
    public int CoinCount { get { return coinCount; } }

    public void Use(GameObject go)
    {
        GameManager.Instance.IncreaseCoinCount();
        //coinCount++;
        SwordShoot sword = go.GetComponentInChildren<SwordShoot>();
        switch(GameManager.Instance.CoinCount)
        {
            case 1:
                sword.UnlockAIShot = true;
                break;
            case 2:
                sword.UnlockChargeShot = true;
                break;
            default:    
                PlayerSetting player = go.GetComponent<PlayerSetting>();
                player.LifeUp();
                break;
        }
        gameObject.SetActive(false);
    }
    
}
