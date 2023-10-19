using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicMasterSlider;
    [SerializeField] private Slider m_MusicBGMSlider;
    [SerializeField] private Slider m_MusicEffectSlider;

    private void Awake()
    {
        m_MusicMasterSlider.onValueChanged.AddListener(SetMasterVolume);
        m_MusicBGMSlider.onValueChanged.AddListener(SetBGMVolume);
        m_MusicEffectSlider.onValueChanged.AddListener(SetEffectVolume);
    }

    public void SetMasterVolume(float volume)
    {
        m_AudioMixer.SetFloat("Master", Mathf.Log10(volume) * 40);
        Debug.Log(volume);
    }

    public void SetBGMVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 40);
    }

    public void SetEffectVolume(float volume)
    {
        m_AudioMixer.SetFloat("Effect", Mathf.Log10(volume) * 40);
    }
}

