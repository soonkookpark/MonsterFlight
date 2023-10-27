using Google.Play.Common;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    //���ӿ�����Ʈ�� ����Ʈ ����
    //public GameObject dieEffect;
    [Header("ü�� ����")]
    [SerializeField] private int startingHealth = 3;
    private int playerhealth;
    public GameObject hitEffect;
    private float invincibleTimeAdd;
    private float invincibleTime = 0.5f;
    //float waitTime = 2f;
    //float elapsedTime = 0f;
    //private void Awake()
    //{
    //    GameManager.Instance.UpdateLife(playerhealth);
    //}
    private void OnEnable()
    {
        playerhealth = startingHealth;
    }
    private void Update()
    {
        invincibleTimeAdd += Time.deltaTime;
        //elapsedTime += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(playerhealth);
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet") || collision.CompareTag("ItemEnemy") || collision.CompareTag("BossEnemy"))
        {
            //GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
            //effect.getcomponent<dieEffect>().DieEffectOn();

            //�÷��̾� 

            if (invincibleTimeAdd >= invincibleTime)
            {
                playerhealth--;
                GameManager.Instance.UpdateLife(playerhealth);
                invincibleTimeAdd = 0f;
            }
            //Debug.Log(playerhealth);
            if (playerhealth <= 0 )
            {
                gameObject.SetActive(false);
                GameManager.Instance.OnPlayerDead();
            }
            else
            {
                StartCoroutine(PlayerHitEffect());
            }
        }
        var item = collision.GetComponent<IItem>();

        if (item != null)
        {
            item.Use(gameObject);
        }
    }
    public void LifeUp()
    {
        if(playerhealth<5)
            playerhealth++;
        GameManager.Instance.UpdateLife(playerhealth);
    }

    IEnumerator PlayerHitEffect()
    {
        //PlayerInput.instance.gameObject.SetActive(false);
        //gameObject.SetActive(false);
        hitEffect. SetActive(true);
        //// ��ٸ� �ð� (��: 2��)


        //Vector3 startPosition = new Vector3(0f, 0f, 0f);
        //Vector3 targetPosition = new Vector3(0f, 2f, 0f); // ȭ�� �߾� �ϴ� ���� �ö� ��ġ

        //while (elapsedTime < waitTime)
        //{
        //    transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / waitTime);

        //    yield return null;
        //}

        //transform.position = targetPosition;

        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);
        //gameObject.SetActive(true);
        //PlayerInput.instance.gameObject.SetActive(false);
    }
}
