using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRigid;
    public float moveSpeed = 5f;
    public float xPosClampMin = -2.3f;
    public float xPosClampMax = 2.3f;
    public float yPosFix = -3.5f;

    public void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        if(PlayerInput.instance.IsMove)
        {
            Vector3 movePos = Vector3.Lerp(playerRigid.position, PlayerInput.instance.CurrentPos, moveSpeed * Time.deltaTime);
            movePos.x = Mathf.Clamp(movePos.x, xPosClampMin, xPosClampMax);
            movePos.y = playerRigid.position.y;
            playerRigid.MovePosition(movePos);
        }
    }
}
