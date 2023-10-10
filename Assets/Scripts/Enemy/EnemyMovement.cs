using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MoveType
    {
        None = -1,
        LtoR,
        RtoL,
        StairLtoR,
        StairRtoL,
        ZigZag,
        Down
    };
    private float moveTime;
    private float duration;
    //public float moveSpeed;
    private Vector3 firstPos;
    private Vector3 midPos;
    private Vector3 lastPos;
    private Vector2 nextPos;
    //private Vector2 direction;
    private int moveNum;
    private int stairCount;
    public float stepHeight { get; private set; } // 계단 높이
    public float stepWidth { get; private set; }  // 계단 너비
    private float rotationSpeed = 20;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        moveNum = 3;// Random.Range(0, 2);
        duration = 5;//Random.Range(10, 11);
        stairCount = Random.Range(1, 4);
        if(transform.CompareTag("BossEnemy"))
        {
            moveNum = (int)MoveType.Down;
            duration = 150;
        }
        switch (moveNum)
        {
            case 0:
                firstPos = new Vector2(-10, 5);
                midPos = (firstPos + lastPos) / 2;
                lastPos = new Vector2(10, 10);
                break;
            case 1:
                firstPos = new Vector2(10, 5);
                midPos = (firstPos + lastPos) / 2;
                lastPos = new Vector2(-10, 10);
                break;
            case 2:
                firstPos = new Vector2(-10, 0);
                lastPos = new Vector2(10, 5);
                stepHeight = (firstPos.y - lastPos.y) / stairCount;
                stepWidth = (firstPos.x - lastPos.x) / stairCount;
                break;
            case 3://오른쪽위에서 시계방향으로 돌아 날아감.
                firstPos = new Vector2(8, 6);
                midPos = new Vector2(-5,-5);
                lastPos = new Vector2(6, 8);
                
                duration = 5;
                
                break;
            case 4:

                break;
            //case 5:
            //    break;
            case 5:
                var randXPos = Random.Range(-4, 4);
                var randYPos = Random.Range(12, 15);
                firstPos = new Vector2(randXPos, randYPos);
                lastPos = new Vector2(randXPos, randYPos - 50);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (moveNum < 4||transform.CompareTag("BossEnemy"))
        {
            //Debug.Log("일직선");
            Move(firstPos, midPos, lastPos, duration);
        }
        //else if(moveNum < 4)
        //{
        //    Debug.Log("계단식");
        //    MoveStair(firstPos, lastPos, stairCount, duration);
        //}
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
    }

    private void Move(Vector3 fP, Vector3 mP, Vector3 lP, float time)
    {
        Debug.Log(mP);
        float t = moveTime / time;
        if(t>1)
        {
             Destroy(gameObject);
        }
        if (mP != new Vector3(0, 0, 0))
        {
            nextPos = CalculateBezierPoint(fP, mP, lP, t);
        }
        else
        {
            nextPos = CalculateMoveStraight(fP, lP, t);
        }
        float rotateAmount = Time.deltaTime * rotationSpeed;
        transform.Rotate(Vector3.forward, rotateAmount);
        transform.position = nextPos;
    }

    private Vector2 CalculateMoveStraight(Vector2 start, Vector2 end, float t)
    {
        Vector2 p = (end - start) * t;
        return p;
    }

    //private void MoveStair(Vector3 fP, Vector3 lP, int stair, float time)
    //{
    //    CalculateStairPosition
    //}

    private Vector2 CalculateBezierPoint(Vector2 start, Vector2 passingPos, Vector2 end, float t)
    {
        float u = 1.0f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector2 p = uuu * start;
        p += 3 * uu * t * passingPos;
        p += 3 * u * tt * end;
        p += ttt * end;
        return p;
    }

    //private Vector3 CalculateStairPosition(Vector3 start, Vector3 end, int step, float time)
    //{

    //    return Vector3 
    //}
}
