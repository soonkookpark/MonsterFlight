using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private static PlayerShoot instance;
    public static PlayerShoot Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerShoot>();

                if (instance == null)
                {
                    Debug.LogError("UIManager instance is not found.");
                }
            }

            return instance;
        }
    }

    public bool UnlockAIShot = false;
    public bool UnlockChargeShot = false;
    //public float startingGuage = 0;
    protected virtual void OnEnable()
    {

    }

    public virtual void CountUp() {}
    // Update is called once per frame
}
