using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrySpriteColorEffect : MonoBehaviour
{
    SpriteRenderer m_spriteRender;
    [SerializeField] Color m_cGold;
    //float m_fColorG = 0.7f;

    [SerializeField] float m_fColorSpeed;
    [SerializeField] float m_fDuration;

    [SerializeField] bool m_bSwitch = false;
    public void SwitchOn() { m_bSwitch = true; }
    public void LerpColor()
    {
        float newR = Mathf.Lerp(m_spriteRender.color.r, Color.white.r, Time.deltaTime * m_fColorSpeed);
        float newG = Mathf.Lerp(m_spriteRender.color.g, Color.white.g, Time.deltaTime * m_fColorSpeed);
        float newB = Mathf.Lerp(m_spriteRender.color.b, Color.white.b, Time.deltaTime * m_fColorSpeed);
        Color newColor = new Color();
        newColor.r = newR;
        newColor.g = newG;
        newColor.b = newB;
        newColor.a = 1f;
        m_spriteRender.color = newColor;
    }

    bool m_bDoingTimer = false;
    IEnumerator Timer()
    {
        m_bDoingTimer = true;
        Color newColor = m_cGold;
        //newColor.g = m_cGold.g;
        m_spriteRender.color = newColor;

        yield return new WaitForSeconds(m_fDuration);

        m_spriteRender.color = Color.white;
        m_bDoingTimer = false;
        m_bSwitch = false;
    }

    private void Awake()
    {
        m_spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(m_bSwitch)
        {
            if (!m_bDoingTimer)
                StartCoroutine(Timer());
            LerpColor();
        }
    }
}
