using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    public GameObject dieEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
            //effect.getcomponent<dieEffect>().DieEffectOn();
            GameManager.Instance.OnPlayerDead();
            Destroy(gameObject);
        }
    }
}
