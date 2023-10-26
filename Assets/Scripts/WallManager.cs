using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject[] walls  ;
    float wide;
    void Awake()
    {
        //Left Wall
        walls[0].gameObject.transform.SetPositionAndRotation(new Vector2(Camera.main.orthographicSize,Screen.width/2),new Quaternion(0,0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        wide = Screen.width / 2;
        //Debug.Log(Screen.width / 2); //360

        //Debug.Log(Camera.main.ScreenToWorldPoint());
    }
}
