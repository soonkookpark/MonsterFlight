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
    int score;
    private GameObject bossCheck;
    private Color[] originalColors; // 원래 색을 저장할 배열
    private MeshRenderer[] meshRenderers;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Start()
    {
        // 게임 오브젝트 내의 모든 MeshRenderer 구성 요소를 가져옴
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        // 모든 MeshRenderer에 대해 루프를 돌며 원래 색을 저장하고 변경
        originalColors = new Color[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalColors[i] = meshRenderers[i].material.color; // 원래 색을 저장
        }
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
    

    private void LoadData()
    {
        maxHP = EnemyManager.instance.GetMonsterData(monsterID).Mon_HP;
        score = EnemyManager.instance.GetMonsterData(monsterID).Mon_Score;
        //Debug.Log(maxHP);
        if (EnemyManager.instance==null)
        {
            Debug.Log("여기드름.");
        }
        enemyHP = maxHP;
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

        
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = Color.white; // 변경된 색을 적용
        }
        ReturnColor();
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

    private void ReturnColor()
    {
        if (getDamaged >= 0.5f&&originalColors != null)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material.color = originalColors[i];
            }
            getDamaged = 0f;
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
        Destroy(gameObject);
    }
}
