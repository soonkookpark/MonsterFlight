using Cinemachine;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //public ParticleSystem hitParticle;
    
    public float enemyHP;
    private float maxHP;

    public CinemachineSmoothPath path;
    public float smoothPos;
    public float speed = 0.5f;
    public Rigidbody2D rb;
    private bool isDead = false;
    //public ParticleSystem explosionParticle;
    public GameObject DieEffectPrefab;

    private string monsterID;
    //Monster Status
    private string ID;
    private int Hp;
        

    private void Awake()
    {
        //explosionParticle.Stop();
        rb = GetComponent<Rigidbody2D>();
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
    private void Start()
    {
        //LoadData();
        //enemyHP = maxHP;
        Debug.Log(transform.position);
    }
    private void LoadData()
    {
        maxHP = EnemyManager.instance.GetMonsterData(monsterID).Mon_HP;
        Debug.Log(maxHP);
        if (EnemyManager.instance==null)
        {
            Debug.Log("여기드름.");
        }
        enemyHP = maxHP;
        //Debug.Log(enemyHP);
    }
    private void FixedUpdate()
    {
        smoothPos += speed * Time.deltaTime;
        
        if (smoothPos > path.MaxPos)
        {
            Debug.Log(transform.position);
            Destroy(gameObject);
            //SpawnManager.enemyCounter--;
        }
        rb.transform.position = path.EvaluateLocalPosition(smoothPos);
    }
    
    public void TakeDamage(int damage)
    {
        Mathf.Clamp(enemyHP -= damage, 0, maxHP);
        
        if (enemyHP <= 0)
        {
            //explosionParticle.Stop();
            //explosionParticle.Play();
            if (!isDead)
            {
                isDead = true;
                OnDie(); 
            }
        }
    }

    public void OnDie()
    {
        GameObject go = Instantiate(DieEffectPrefab, transform.position, Quaternion.identity);
        go.GetComponent<DieEffect>().DieEffectOn();
        //explosionParticle.Play();
        Debug.Log("Hi");
        GameManager.Instance.AddScore(100000);
        Destroy(gameObject);
    }
}
