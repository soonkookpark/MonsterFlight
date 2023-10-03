using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    private float height;

    private void Awake()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        height = boxCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -height)
        {
            Reposition();
        }
    }
    private void Reposition()
    {
        var offset = new Vector2(0f, height * 2f);
        transform.position = (Vector2)transform.position + offset;
    }
}
