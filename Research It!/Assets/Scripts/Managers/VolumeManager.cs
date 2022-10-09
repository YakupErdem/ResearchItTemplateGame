using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public void SetVolume(float v)
    {
        SaveSystem.SetFloat("Volume", v);
    }

    public void SetVolumeBySlider(Slider s)
    {
        SaveSystem.SetFloat("Volume", s.value);
        foreach (var audioSrcVolumeChanger in FindObjectsOfType<AudioSrcVolumeChanger>())
        {
            audioSrcVolumeChanger.SetVolume();
        }
    }
}
