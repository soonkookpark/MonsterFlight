using UnityEngine;
using UnityEngine.UI;

public class SwordShoot : PlayerShoot
{
    public Transform[] firePos; // 크기가 4인 배열
    public GameObject projectile;
    public GameObject chargeShot;
    public Slider powerGuage;
    public float shootDelay = 0.1f;
    public float shootTimer;
    public float subWeaponCount = 0;
    public float subweaponAttackEnable = 50;
    public bool chargeShotNow;
    private float ultTime = 30f;
    private float ultTimer;
    
    protected override void OnEnable()
    {
        base.OnEnable();

        powerGuage.gameObject.SetActive(true);
        powerGuage.minValue = 0;
        powerGuage.maxValue = subweaponAttackEnable;
        powerGuage.value = subWeaponCount;
    }

    // Update is called once per frame
    protected void Update()
    {
        shootTimer += Time.deltaTime;
        ultTimer += Time.deltaTime;
        if(shootTimer>shootDelay)
        {
            Shoot();
            shootTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space)&&ultTimer>ultTime)
        {
            Ult();
            ultTimer = 0;
        }

        if(subWeaponCount >= subweaponAttackEnable)
        {
            Debug.Log("1번");
            //ChargeShoot();
            Debug.Log("2번");
            subWeaponCount = 0;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < firePos.Length; i++)
        {
            Instantiate(projectile, firePos[i].position, Quaternion.identity);
        }
    }

    public override void CountUp()
    {
        subWeaponCount++;
        powerGuage.value = subWeaponCount;
        //Debug.Log(subWeaponCount);
    }

    void Ult()
    {
        foreach (GameObject bullet in ObjectManager.instance.enemyAttack)
        {
            if (bullet.activeInHierarchy)
            {
                bullet.SetActive(false);
            }
        }
    }

    void ChargeShoot()
    {
        
        Instantiate(chargeShot,transform.position, Quaternion.identity);
        
    }


}
