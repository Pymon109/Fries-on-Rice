using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect_text : MonoBehaviour
{
    [SerializeField] Text m_text;
    [SerializeField] float m_fDuration;

    [SerializeField] float m_fAlphSpeed;
    [SerializeField] float m_fMoveSpeed;

    [SerializeField] float m_fMaxPos;

    Vector3 m_vNewPos;

    [SerializeField] List<Color> m_colors;
    public enum E_TEXT_EFFECT_TYPE
    {
        GAIN = 0,
        SPEND
    }

    public void SetText(string text, E_TEXT_EFFECT_TYPE type)
    {
        m_text.text = text;
        m_text.color = m_colors[(int)type];
    }

    private void Start()
    {
        Destroy(gameObject, m_fDuration);
        m_vNewPos = transform.position;
        m_vNewPos.y += m_fMaxPos;
    }
    private void Update()
    {
        Color color = m_text.color;
        float fA = color.a;
        fA = Mathf.Lerp(fA, 0, m_fAlphSpeed * Time.deltaTime);
        color.a = fA;
        m_text.color = color;

        transform.position = Vector3.Lerp(transform.position, m_vNewPos, m_fMoveSpeed * Time.deltaTime);

        //Debug.Log(transform.position);
    }

    
}
