using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string masterVolumeParameter = "MasterVolume";
    public string musicVolumeParameter = "MusicVolume";
    public string soundEffectVolumeParameter = "SoundEffectVolume";

    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundEffectVolumeKey = "SoundEffectVolume";

    private void Start()
    {
        // При запуске игры восстанавливаем сохраненные значения громкости
        float savedMasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, 0f);
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0f);
        float savedSoundEffectVolume = PlayerPrefs.GetFloat(SoundEffectVolumeKey, 0f);
        SetMasterVolume(savedMasterVolume);
        SetMusicVolume(savedMusicVolume);
        SetSoundEffectVolume(savedSoundEffectVolume);
    }

    // Функция для управления мастер-громкостью
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(masterVolumeParameter, volume);

        // Сохраняем текущую громкость мастер-громкости
        PlayerPrefs.SetFloat(MasterVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // Функция для управления громкостью фоновой музыки
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(musicVolumeParameter, volume);

        // Сохраняем текущую громкость громкости фоновой музыки
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    // Функция управления громкостью звукового эффекта
    public void SetSoundEffectVolume(float volume)
    {
        audioMixer.SetFloat(soundEffectVolumeParameter, volume);

        // Сохраняем текущую громкость громкости звукового эффекта
        PlayerPrefs.SetFloat(SoundEffectVolumeKey, volume);
        PlayerPrefs.Save();
    }
}