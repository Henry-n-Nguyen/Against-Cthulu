using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class AudioManager : Singleton<AudioManager>
{

    [Header("Audio Sources")]
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("BGMs")]
    [SerializeField] private AudioClip bgm;

    [Header("SFXs")]
    [SerializeField] private List<AudioClip> SFXs;

    private UserData data;

    private void Start()
    {
        data = UserDataManager.Ins.userData;

        SetBGMVolume();
        SetSFXVolume();

        BGMSource.clip = bgm;
        BGMSource.Play();
    }

    public void SetBGMVolume()
    {
        BGMSource.volume = data.BGMVolume;
    }

    public void SetSFXVolume()
    {
        SFXSource.volume = data.SFXVolume;
    }

    public void PlaySFX(SFX index) 
    {
        if ((int) index < SFXs.Count) SFXSource.PlayOneShot(SFXs[(int)index]);
    }
}
