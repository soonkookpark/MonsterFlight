using UnityEngine;

public class DieEffect : MonoBehaviour
{
    public ParticleSystem dieParticle;

    public void DieEffectOn()
    {
        dieParticle.Play();
        Destroy(gameObject, 1.5f);
    }

}
