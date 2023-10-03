using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float scrollSpeed = 5f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);   
    }
}
