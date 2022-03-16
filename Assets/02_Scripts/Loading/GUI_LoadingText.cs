using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_LoadingText : MonoBehaviour
{
    [SerializeField] float m_fDuration;
    Langauge m_lnaguage;
    string[] m_sComments = { ""," .", " ..", " ..." };

    bool m_bFlag = true;
    int m_iIndex = 0;

    IEnumerator ChangeText()
    {
        //m_bFlag = false;
        while(true)
        {
            m_lnaguage.backtWord = m_sComments[m_iIndex];
            m_iIndex = (m_iIndex + 1) % 4;
            yield return new WaitForSeconds(m_fDuration);
        }
        //m_bFlag = true;
    }

    private void Awake()
    {
        m_lnaguage = GetComponent<Langauge>();
    }

    private void Start()
    {
        StartCoroutine(ChangeText());
    }

    /*    private void Update()
    {
        if (m_bFlag)
            StartCoroutine(ChangeText());
    }*/
}
