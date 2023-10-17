using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChargeShot : MonoBehaviour
{
    public static ChargeShot instance;
    Rigidbody2D chargeShot;
    public float speed = 5f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject,2.5f);
        }

        chargeShot = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ChargeShotMove();
    }

    private void ChargeShotMove()
    {
        chargeShot.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet"))
        {
            //Debug.Log("�� �ε�����");
            collision.gameObject.SetActive(false);
        }

        if(collision.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }


}
