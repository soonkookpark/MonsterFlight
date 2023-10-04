using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRigid;

    public float moveSpeed = 5f;
    public float xPosClampMin;
    public float xPosClampMax;
    public float yPosClampMin;
    public float yPosClampMax;

    public void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        yPosClampMin = -Camera.main.orthographicSize * 0.9f;
        yPosClampMax = Camera.main.orthographicSize * 0.9f;
        xPosClampMin = yPosClampMin / 16 * 10;
        xPosClampMax = yPosClampMax / 16 * 10;
    }

    private void FixedUpdate()
    {
        
        if(PlayerInput.instance.IsMove)
        {
            Vector3 movePos = Vector3.Lerp(playerRigid.position, PlayerInput.instance.CurrentPos, moveSpeed * Time.deltaTime);
            movePos.x = Mathf.Clamp(movePos.x, xPosClampMin, xPosClampMax);
            movePos.y = Mathf.Clamp(movePos.y, yPosClampMin, yPosClampMax);
            playerRigid.MovePosition(movePos);
            //Debug.Log(Screen.width);
            Debug.Log(xPosClampMin);
            Debug.Log(xPosClampMax);
        }
    }
}
