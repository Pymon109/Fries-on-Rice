using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEditor : MonoBehaviour
{
    [SerializeField] InputField m_ifBGM;
    [SerializeField] InputField m_ifUI;
    [SerializeField] InputField m_ifFryStack;
    [SerializeField] InputField m_ifFryDropping;
    [SerializeField] InputField m_ifGold;
    [SerializeField] InputField m_ifResultUI;

    public void SetSoundManager()
    {
        float fBGMVolum = (m_ifBGM.text == "") ? 0.5f : float.Parse(m_ifBGM.text);
        SoundManager.instance.UpdateBGMVolume(fBGMVolum);

        float[] newVolumes = new float[5];
        newVolumes[0] = (m_ifUI.text == "") ? 1f : float.Parse(m_ifUI.text);
        newVolumes[1] = (m_ifFryStack.text == "") ? 1f : float.Parse(m_ifFryStack.text);
        newVolumes[2] = (m_ifFryDropping.text == "") ? 1f : float.Parse(m_ifFryDropping.text);
        newVolumes[3] = (m_ifGold.text == "") ? 1f : float.Parse(m_ifGold.text);
        newVolumes[4] = (m_ifResultUI.text == "") ? 1f : float.Parse(m_ifResultUI.text);

        SoundManager.instance.fSFXVolume = newVolumes;
    }
}
