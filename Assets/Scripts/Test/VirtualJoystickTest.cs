using UnityEngine;

public class VirtualJoystickTest : MonoBehaviour
{
    public VirtualJoystick2 joystick;
    //중간에 생성되면 태그로 찾아라

    void Update()
    {
        Debug.Log($"{joystick.Value}");
    }
}