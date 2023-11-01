using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //public ParticleSystem hitParticle;
    
    public float enemyHP;
    private float maxHP;

    public CinemachineSmoothPath path;
    public float smoothPos;
    private float speed;
    public Rigidbody2D rb;
    private bool isDead = false;
    //public ParticleSystem explosionParticle;
    public GameObject DieEffectPrefab;
    public GameObject FireEffectPrefab;
    private bool firstFire = false;
    private bool secondFire = false;
    private bool lastFire = false;
    private float getDamaged;
    private string monsterID;
    //Monster Status
    private string ID;
    private int Hp;
    private int levelPerHp;
    int score;
    private GameObject bossCheck;
    //parentHitEffect.OnHit();
    private ChangeColor changeColor;
    public GameObject[] itemPrefab;
    GameObject dropItem;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        changeColor = GetComponent<ChangeColor>();
        if (changeColor == null)
        {
            Debug.LogError("No ChangeColor component found on this game object.");
        }
    }
    void Start()
    {
    }

    public void InitializePath(CinemachineSmoothPath newPath, Vector3 newStartPosition)
    {
        path = newPath;
        rb.transform.position = newStartPosition;
        smoothPos = 0f;
    }

    public void SetMonsterID(string id)
    {
        monsterID = id;
        LoadData();
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void SetHp(float newSpeed)
    {
        maxHP += ((newSpeed-1)* levelPerHp);
        enemyHP = maxHP;
    }


    private void LoadData()
    {
        maxHP = EnemyManager.instance.GetMonsterData(monsterID).Mon_HP;
        levelPerHp = EnemyManager.instance.GetMonsterData(monsterID).Stage_Hpup;
        score = EnemyManager.instance.GetMonsterData(monsterID).Mon_Score;
        //Debug.Log(maxHP);
        if (EnemyManager.instance==null)
        {
            Debug.Log("여기드름.");
        }
        
        //Debug.Log(enemyHP);
    }
    private void FixedUpdate()
    {
        getDamaged += Time.fixedDeltaTime;
        smoothPos += speed * Time.deltaTime;
        
        if (smoothPos > path.MaxPos)
        {
            //Debug.Log(transform.position);
            Destroy(gameObject);
            //SpawnManager.enemyCounter--;
        }
        rb.transform.position = path.EvaluateLocalPosition(smoothPos);

    }
    
    public void TakeDamage(int damage)
    {
        Mathf.Clamp(enemyHP -= damage, 0, maxHP);
        //if (enemyHP / maxHP < 0.7 && !firstFire && rb.CompareTag("BossEnemy"))
        //    FireSetting1();
        //if (enemyHP / maxHP < 0.4 && !secondFire && rb.CompareTag("BossEnemy"))
        //    FireSetting2();
        //if (enemyHP / maxHP < 0.2 && !lastFire && rb.CompareTag("BossEnemy"))
        //    FireSetting3();

        if (changeColor != null)
            changeColor.OnHit();

        if (enemyHP <= 0)
        {

            //explosionParticle.Stop();
            //explosionParticle.Play();
            if (!isDead)
            {
                //Ondie()
                isDead = true;
                OnDie(); 
            }
        }
    }





    //public void FireSetting1()
    //{
    //    firstFire = true;
    //    GameObject go = Instantiate(FireEffectPrefab, transform.position, Quaternion.identity);
    //    go.GetComponent<SecondBossFire>().UnderSeventy();
    //    //go.GetComponent<SecondBossFire>().UnderSeventy();
    //}
    //public void FireSetting2()
    //{
    //    secondFire = true;
    //    GameObject go = Instantiate(FireEffectPrefab, transform.position, Quaternion.identity);
    //    go.GetComponent<SecondBossFire>().UnderFourty();
    //    //go.GetComponent<SecondBossFire>().UnderFourty();
    //}
    //public void FireSetting3()
    //{
    //    lastFire = true;
    //    GameObject go = Instantiate(FireEffectPrefab, transform.position, Quaternion.identity);
    //    go.GetComponent<SecondBossFire>().UnderTwenty();
    //    //go.GetComponent<SecondBossFire>().UnderTwenty();
    //}

    public void OnDie()
    {
        GameObject go = Instantiate(DieEffectPrefab, transform.position, Quaternion.identity);
        go.GetComponent<DieEffect>().DieEffectOn();
        //explosionParticle.Play();
        //Debug.Log("Hi");
        GameManager.Instance.AddScore(score);
        if (CompareTag("ItemEnemy"))
            DropItem();
        Destroy(gameObject);
    }
    private void DropItem()
    {
        
        if(GameManager.Instance.CoinCount<2)
        {
            dropItem = Instantiate(itemPrefab[0], transform.position, Quaternion.identity);
        }
        else
        {
            dropItem = Instantiate(itemPrefab[1], transform.position, Quaternion.identity);
        }
        dropItem.SetActive(true);
    }
}
