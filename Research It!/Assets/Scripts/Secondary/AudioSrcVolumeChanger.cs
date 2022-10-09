using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSrcVolumeChanger : MonoBehaviour
{
    private AudioSource _audioSrc;

    private void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSrc.volume = SaveSystem.GetFloat("Volume");
    }

    public void SetVolume()
    {
        _audioSrc.volume = SaveSystem.GetFloat("Volume");
    }
}
