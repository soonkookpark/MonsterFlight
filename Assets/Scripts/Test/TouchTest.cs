using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TouchTest : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        var message = string.Empty;


        //Debug.Log(Input.touchCount);
        foreach(Touch touch in Input.touches)
        {
            message += "Touch ID: " + touch.fingerId;
            message += "\tPhase" + touch.phase;
            message += "Position: " + touch.position;
            message += "\tDelta Pos: " + touch.deltaPosition;
            message += "\tDelta Time: " + touch.deltaTime + "\n";
                //Debug.Log(touch.fingerId);
        }
        Debug.Log("");
    }
}
