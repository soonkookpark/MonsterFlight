using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public Vector3 CurrentPos { get; private set; }
    public bool IsMove { get; private set; }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            IsMove = true;

        if (Input.GetButtonUp("Fire1"))
            IsMove = false;

        if (Input.GetButton("Fire1"))
        {
            //Debug.Log(Input.mousePosition);
            CurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
    }
}
