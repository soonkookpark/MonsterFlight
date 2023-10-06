using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum moveType
    {
        None = -1,
        LtoR,
        RtoL,
        StairLtoR,
        StairRtoL,
        ZigZag,
        Down
    };
    public float moveTime;
    public float duration;
    //public float moveSpeed;
    private Vector3 firstPos;
    private Vector3 midPos;
    private Vector3 lastPos;
    private Vector2 nextPos;
    //private Vector2 direction;
    private int moveNum;

    private void Start()
    {
        moveNum = Random.Range(0, (int)moveType.Down+1);
    }
    private void FixedUpdate()
    {
        if (firstPos.x==0)
        switch (0)//moveNum)
        {
            case 0:
                firstPos = new Vector2(-10, 0);
                midPos = (firstPos + lastPos)/2;
                lastPos = new Vector2(10, 5);
                duration = 5;//Random.Range(5, 15);
                break;
            //case 1:
            //    break;
            //case 2:
            //    break; 
            //case 3:
            //    break;
            //case 4:
            //    break;
            //case 5:
            //    break;


        }
        //if (moveTime < duration)
        {
            Move(firstPos, midPos, lastPos, duration);
        }
    }
    private void Update()
    {
        moveTime += Time.deltaTime;
    }
    private void Move(Vector3 fP, Vector3 mP, Vector3 lP, float time)
    {
        float t = moveTime / time;
        if(t>1)
        {
            Destroy(gameObject);
        }
        Vector2 pos = transform.position;
        nextPos = CalculateBezierPoint(fP, mP, lP, t);
        transform.position = nextPos;

        //direction = (nextPos - pos).normalized;

    }
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
    //private void 
}
