using Pattern.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volume = 0.3f;

    protected override void Awake()
    {
        base.Awake();

        this._volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, 1f);

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        if (this._audioSource == null)
        {
            this._audioSource = GetComponent<AudioSource>();
        }
    }

    public void ChangeVolume()
    {
        this._volume += 0.1f;

        if (this._volume > 1f)
        {
            this._volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, this._volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return this._volume;
    }


}
