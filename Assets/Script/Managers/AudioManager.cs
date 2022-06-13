using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer mainMixer;

    [SerializeField] private Slider _slider;

    [Header("Images")]
    [SerializeField] private Image _audioLowImg;
    [SerializeField] private Image _audioMidImg;
    [SerializeField] private Image _audioHighImg;

    public const string VOLUME_ID = "MasterVolume";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(VOLUME_ID))
        {
            PlayerPrefs.SetFloat(VOLUME_ID, 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void SetVolume(float value)
    {
        mainMixer.SetFloat(VOLUME_ID, Mathf.Log10(value) * 20);
        UpdateImage(value);
    }

    private void LoadVolume()
    {
        _slider.value = PlayerPrefs.GetFloat(VOLUME_ID);
        SetVolume(_slider.value);
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(VOLUME_ID, _slider.value);
    }

    private void UpdateImage(float value)
    {
        if (value < 0.33)
        {
            _audioLowImg.gameObject.SetActive(true);
            _audioMidImg.gameObject.SetActive(false);
            _audioHighImg.gameObject.SetActive(false);
        }
        else if (value < 0.66)
        {
            _audioLowImg.gameObject.SetActive(false);
            _audioMidImg.gameObject.SetActive(true);
            _audioHighImg.gameObject.SetActive(false);
        }
        else
        {
            _audioLowImg.gameObject.SetActive(false);
            _audioMidImg.gameObject.SetActive(false);
            _audioHighImg.gameObject.SetActive(true);
        }
    }

    #region Saving functions
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveVolume();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveVolume();
    }

    private void OnApplicationQuit()
    {
        SaveVolume();
    }

    private void OnDestroy()
    {
        SaveVolume();
    }
    #endregion
}
