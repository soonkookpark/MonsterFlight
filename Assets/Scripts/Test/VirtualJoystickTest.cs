using UnityEngine;

public class VirtualJoystickTest : MonoBehaviour
{
    public VirtualJoystick2 joystick;
    //�߰��� �����Ǹ� �±׷� ã�ƶ�

    void Update()
    {
        Debug.Log($"{joystick.Value}");
    }
}