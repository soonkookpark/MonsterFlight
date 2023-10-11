using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
//버려진 스크립트.
public class EnemyMovement : MonoBehaviour
{
    //움직이는 애들 static
    private float moveTime;
    private float duration;


    public CinemachineSmoothPath path;
    public float smoothPos;
    public float speed = 0.5f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

        duration = 5;
        if(transform.CompareTag("BossEnemy"))
        {
            duration = 150;
        }
    }

    private void FixedUpdate()
    {
        smoothPos += speed * Time.deltaTime;
        if (smoothPos > path.MaxPos)
        {
            Destroy(gameObject);
        }
        rb.transform.position = path.EvaluateLocalPosition(smoothPos);
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
    }

}
