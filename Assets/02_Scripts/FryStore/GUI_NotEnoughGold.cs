using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_NotEnoughGold : MonoBehaviour
{
    [SerializeField] Image m_img;

    [SerializeField] float m_fDuration;
    [SerializeField] float m_fFadeSpeed;
/*    float m_fLeftTime;
    bool m_bActive = false;*/

    float m_fA = 0;
    Color m_color;
/*    public void SetActive()
    {
        gameObject.SetActive(true);
        m_fLeftTime = m_Duration;
        m_bActive = true;
    }*/

    public enum E_STATE
    {
        SLEEP = 0,
        FADE_IN,
        WAIT,
        FADE_OUT
    }
    E_STATE m_curState = E_STATE.SLEEP;

    void UpdateState()
    {
        switch(m_curState)
        {
            case E_STATE.SLEEP:

                break;
            case E_STATE.FADE_IN:
                FadeIn();
                if (m_fA >= 1)
                    SetState(E_STATE.WAIT);
                break;
            case E_STATE.WAIT:

                break;
            case E_STATE.FADE_OUT:
                FadeOut();
                if (m_fA <= 0)
                    SetState(E_STATE.SLEEP);
                break;
        }
    }

    public void SetState(E_STATE command)
    {
        switch (command)
        {
            case E_STATE.SLEEP:
                gameObject.SetActive(false);
                break;
            case E_STATE.FADE_IN:
                m_color = m_img.color;
                //gameObject.SetActive(true);
                break;
            case E_STATE.WAIT:
                StartCoroutine(ActiveAndWait());
                break;
            case E_STATE.FADE_OUT:
                
                break;
        }
        m_curState = command;
    }

    void FadeIn()
    {
        //m_fA = Mathf.Lerp(m_fA, 1, m_fFadeTime);
        m_fA += Time.deltaTime * m_fFadeSpeed;
        m_color.a = m_fA;
        m_img.color = m_color;
        //Debug.Log("Fade in " + m_fA);
    }

    void FadeOut()
    {
        //m_fA = Mathf.Lerp(m_fA, 0, m_fFadeTime);
        m_fA -= Time.deltaTime * m_fFadeSpeed;
        m_color.a = m_fA;
        m_img.color = m_color;
        //Debug.Log("Fade out " + m_fA);
    }

    IEnumerator ActiveAndWait()
    {
        yield return new WaitForSeconds(m_fDuration);
        SetState(E_STATE.FADE_OUT);
    }

    private void Awake()
    {
        m_img = GetComponent<Image>();
    }

    private void Start()
    {
        //m_fLeftTime = m_Duration;
        //m_color = m_img.color;
    }

    private void Update()
    {
        /*if(m_bActive)
        {
            m_fLeftTime -= Time.deltaTime;
            if(m_fLeftTime <= 0)
            {
                gameObject.SetActive(false);
                m_bActive = false;
            }
        }*/
        UpdateState();
    }
}
