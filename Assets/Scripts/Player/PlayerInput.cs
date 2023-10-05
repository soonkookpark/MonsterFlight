using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public Vector3 CurrentPos { get; private set; }
    public bool IsMove { get; private set; }
    public Vector3 FirstPos { get; private set; }
    public Vector3 MovePos {  get; private set; }
    public Vector3 MoveDirection { get; private set; } // 이동 방향

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
            FirstPos = Vector3.zero;
            CurrentPos = Vector3.zero;
        }


        if (Input.GetButton("Fire1"))
        {
            //Debug.Log(Input.mousePosition);
            CurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MovePos = (CurrentPos - FirstPos).normalized;
            //MovePos.Normalize();
        }
    }
}
