using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossFire : MonoBehaviour
{
    public ParticleSystem fire1;
    public ParticleSystem fire2;
    public ParticleSystem fire3;
    public ParticleSystem fire4;
    public ParticleSystem fire5;
    public ParticleSystem fire6;
    public void UnderSeventy()
    {
        fire1.Play();
        fire2.Play();
    }
    public void UnderFourty()
    {
        fire3.Play();
        fire4.Play();
    }
    public void UnderTwenty()
    {
        fire5.Play();
        fire6.Play();
    }
    public void Ondie()
    {
        Destroy(gameObject);
    }

}
