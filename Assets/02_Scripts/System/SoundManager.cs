using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager unique;
    public static SoundManager instance { get { return unique; } }

    [SerializeField] AudioSource m_BGMAudioSource;
    [SerializeField] AudioSource m_SFXAudioSource;

    public enum E_SFX_TYPE
    {
        UI = 0,
        FRY_STACK,
        FRY_DROPPING,
        GET_GOLD,
        GAMEOVER,
        MAX_COUNT
    }
    [SerializeField] AudioClip[] m_SFXAudioClips = new AudioClip[(int)E_SFX_TYPE.MAX_COUNT];
    [SerializeField] float[] m_fSFXVolume = new float[(int)E_SFX_TYPE.MAX_COUNT];
    public float[] fSFXVolume { set { m_fSFXVolume = value; } }
    [SerializeField] AudioClip[] m_SFXAudioClips_FryDrop = new AudioClip[3];

    public void UpdateBGMVolume(float value)
    {
        m_BGMAudioSource.volume = value;
    }
    public void BGMMuteSwitch() 
    {
        var value = OptionManager.instance.GetCurrentOptionValue(OptionManager.E_OPTION_TYPE.BGM);
        switch(value)
        {
            case OptionManager.E_OPTION_VALUE.ON:
                m_BGMAudioSource.mute = false;
                break;
            case OptionManager.E_OPTION_VALUE.OFF:
                m_BGMAudioSource.mute = true;
                break;
        }
    }
    public void SFXMuteSwitch() 
    {
        var value = OptionManager.instance.GetCurrentOptionValue(OptionManager.E_OPTION_TYPE.SFX);
        switch (value)
        {
            case OptionManager.E_OPTION_VALUE.ON:
                m_SFXAudioSource.mute = false;
                break;
            case OptionManager.E_OPTION_VALUE.OFF:
                m_SFXAudioSource.mute = true;
                break;
        }
    }

    public void SFXAudioSourceClear()
    {
        m_SFXAudioSource.Stop();
    }

    public void SFXAudioSourcePlay(E_SFX_TYPE type)
    {
        AudioClip clip;
        if (type == E_SFX_TYPE.FRY_DROPPING)
        {
            int index = Random.RandomRange(0, 3);
            clip = m_SFXAudioClips_FryDrop[index];
        }
        else
        {
            clip = m_SFXAudioClips[(int)type];
        }
        if (clip == null)
            return;
        m_SFXAudioSource.volume = m_fSFXVolume[(int)type];
        m_SFXAudioSource.PlayOneShot(clip);
    }

    /*public void SFXAudioSourcePlay_fryDropping()
    {
        int index = Random.RandomRange(0, 3);
        AudioClip clip = m_SFXAudioClips_FryDrop[index];
        m_SFXAudioSource.PlayOneShot(clip);
    }
    public void SFXAudioSourcePlay_fryDropping_0()
    {
        AudioClip clip = m_SFXAudioClips_FryDrop[0];
        m_SFXAudioSource.PlayOneShot(clip);
    }
    public void SFXAudioSourcePlay_fryDropping_1()
    {
        AudioClip clip = m_SFXAudioClips_FryDrop[1];
        m_SFXAudioSource.PlayOneShot(clip);
    }
    public void SFXAudioSourcePlay_fryDropping_2()
    {
        AudioClip clip = m_SFXAudioClips_FryDrop[2];
        m_SFXAudioSource.PlayOneShot(clip);
    }
    public void SFXAudioSourcePlay_fryDropping_test()
    {
        AudioClip clip = m_SFXAudioClips[(int)E_SFX_TYPE.FRY_DROPPING];
        m_SFXAudioSource.PlayOneShot(clip);
    }*/

    private void Awake()
    {
        if (instance == null)
        {
            unique = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        BGMMuteSwitch();
        SFXMuteSwitch();
    }
}
