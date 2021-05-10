using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeHandler : MonoBehaviour
{
    public AudioMixer master;
    public void SetMasterLevel(float sliderValue)
    {
        master.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    public void SetSFXLevel(float sliderValue)
    {
        master.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }
    public void SetMusicLevel(float sliderValue)
    {
        master.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
}
