using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAudioEvents
{
    UI_RobotActive,
    UI_Button,
    Station_Assembly,
    Station_MaterialOpen,
    Station_SmelterAdd,
    Station_Printer,
    Station_WeaponFab,
    Station_Soldering,
    Station_BodyFab,
    PlayerPickUp,
    PlayerDropItem,
    PlayerError,
    Music=20
}
//I'm sorry but I'm hella tired 
public class AudioHandler : MonoBehaviour
{
    private static AudioHandler _instance;
    public AudioHandler Instance
    {
        get => _instance;
        set => _instance = value;
    }

    public bool Player2Joined;

    public List<AudioInfo> ListOfClips = new List<AudioInfo>();
    private AudioSource _audioSource;
    public AudioSource AssmblySource;
    public AudioSource PrinterSource;
    public AudioSource WeaponFabSource;
    public AudioSource SolderingSource;
    public AudioSource BodyFabSource;
    public AudioSource MusicSource;
    
    private void Awake()
    {
        if(!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DataManager.ToTheCloud(DataKeys.AUDIO,this);
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }

    public void Joined()
    {
        Player2Joined = true;
    }
    
    //Returns true if audio event is looping
    public bool PlayAudio(EAudioEvents newEvent)
    {
        for(int i = 0; i < ListOfClips.Count; i++)
        {
            if(ListOfClips[i].EventID == newEvent)
            {
                if(ListOfClips[i].isLooping)
                {
                    PlayLoopSource(newEvent);
                    return true;
                }
                _audioSource.PlayOneShot(ListOfClips[i].AudioClip);
                return false;
            }
        }
        Debug.LogWarning($"Event {newEvent} is missing audio clip");
        return false;
    }

    private void PlayLoopSource(EAudioEvents newEvent)
    {
        switch(newEvent)
        {
            case EAudioEvents.Station_Assembly:
                AssmblySource.Play();
                break;
            case EAudioEvents.Station_Soldering:
                SolderingSource.Play();
                break;
            case EAudioEvents.Station_Printer:
                PrinterSource.Play();
                break;
            case EAudioEvents.Station_BodyFab:
                BodyFabSource.Play();
                break;
            case EAudioEvents.Station_WeaponFab:
                WeaponFabSource.Play();
                break;
            case EAudioEvents.Music:
                MusicSource.Play();
                break;
        }
    }

    public void StopLoopSource(EAudioEvents eventToStop)
    {
        switch(eventToStop)
        {
            case EAudioEvents.Station_Assembly:
                AssmblySource.Stop();
                break;
            case EAudioEvents.Station_Soldering:
                SolderingSource.Stop();
                break;
            case EAudioEvents.Station_Printer:
                PrinterSource.Stop();
                break;
            case EAudioEvents.Station_BodyFab:
                BodyFabSource.Stop();
                break;
            case EAudioEvents.Station_WeaponFab:
                WeaponFabSource.Stop();
                break;
            case EAudioEvents.Music:
                MusicSource.Stop();
                break;
        }
    }
}

[Serializable]
public class AudioInfo
{
    public EAudioEvents EventID;
    public bool isLooping;
    public AudioClip AudioClip;
}
