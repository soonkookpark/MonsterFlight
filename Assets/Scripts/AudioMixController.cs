using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] public AudioMixer m_AudioMixer;
    [SerializeField] public Slider m_MusicMasterSlider;
    [SerializeField] public Slider m_MusicBGMSlider;
    [SerializeField] public Slider m_MusicEffectSlider;

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
        GameManager.Instance.SaveSoundSettings("Master", volume);
    }

    public void SetBGMVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 40);
        GameManager.Instance.SaveSoundSettings("BGM", volume);
    }

    public void SetEffectVolume(float volume)
    {
        m_AudioMixer.SetFloat("Effect", Mathf.Log10(volume) * 40);
        GameManager.Instance.SaveSoundSettings("Effect", volume);
    }

    public void SaveSoundSettings(string key, float value)
    {
        var saveFileName = "sound_settings.json";
        var saveData = new SaveDataV1();

        switch (key)
        {
            case "Master":
                saveData.MasterVolume = value;
                break;
            case "BGM":
                saveData.BgmVolume = value;
                break;
            case "Effect":
                saveData.EffectVolume = value;
                break;
        }

        SaveLoadSystem.Save(saveData, saveFileName);

        Debug.Log(key + " sound setting saved: " + value);
    }
}

