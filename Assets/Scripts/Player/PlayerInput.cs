using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public Vector3 CurrentPos { get; private set; }
    public bool IsMove { get; private set; }
    public Vector3 FirstPos { get; private set; }
    public Vector3 MovePos;
    public Vector3 MoveDirection { get; private set; } // 이동 방향
    public float speed = 2;
    private float maxSpeed = 1.7f;
    public float MoveDistance { get; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        
        IsMove = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")&&!IsMove)
        {
            IsMove = true;
            FirstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetButtonUp("Fire1")&& IsMove)
        {
            IsMove = false;

            CurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveDistance = Vector3.Distance(FirstPos, CurrentPos);

            FirstPos = Vector3.zero;
            CurrentPos = Vector3.zero;
        }


        if (IsMove)
        {
            //Debug.Log(Input.mousePosition);
            CurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MovePos.x = (CurrentPos.x - FirstPos.x);
            MovePos.y = (CurrentPos.y - FirstPos.y);
            if (MovePos.x > 1 || MovePos.x < -1|| MovePos.y > 1 || MovePos.y < -1)
                MovePos = (CurrentPos - FirstPos).normalized;   
            //MovePos = CurrentPos;
            
            //MovePos.Normalize();
            //Debug.Log(MovePos.y);
        }
    }
}
