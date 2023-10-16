using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //public ChangeColor changeColor;
    public Color hitColor = Color.white;
    public float duration = 0.2f;
    public float minEmission = 1.0f;
    public float maxEmission = 3.0f;

    private Renderer[] childRenderers;
    private Color[] originalColors;
    private float[] emissions;
    private float timer = 0.0f;
    private bool isHit = false;

    private void Start()
    {
        // 모든 자식 렌더러 수집
        childRenderers = GetComponentsInChildren<Renderer>();

        // 원래 색상 및 Emission 값을 기록
        originalColors = new Color[childRenderers.Length];
        emissions = new float[childRenderers.Length];

        for (int i = 0; i < childRenderers.Length; i++)
        {
            originalColors[i] = childRenderers[i].material.GetColor("_EmissionColor");
            emissions[i] = 0.0f;
        }
    }

    private void Update()
    {
        if (isHit)
        {
            for (int i = 0; i < childRenderers.Length; i++)
            {
                emissions[i] = Mathf.Lerp(minEmission, maxEmission, Mathf.PingPong(timer / duration, 1));
                childRenderers[i].material.SetColor("_EmissionColor", hitColor * emissions[i]);
            }
            timer += Time.deltaTime;

            if (timer >= duration)
            {
                for (int i = 0; i < childRenderers.Length; i++)
                {
                    childRenderers[i].material.SetColor("_EmissionColor", originalColors[i]);
                }
                isHit = false;
            }
        }
    }

    public void OnHit()
    {
        timer = 0.0f;
        isHit = true;
    }
}
