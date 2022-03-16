using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioControl_ingame : MonoBehaviour
{
    RectTransform m_rectTrs;

    float m_fWidth = Screen.width;
    float m_fHeight = Screen.height;

    private void Awake()
    {
        m_rectTrs = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Debug.Log(string.Format("w : {0} / h : {1}", m_fWidth, m_fHeight));
        Debug.Log(string.Format("standard w : w = {0}:1", m_fWidth / 1080));

        float fNewWidthRatio = m_fWidth / 1080;
        m_rectTrs.localScale = Vector3.one * fNewWidthRatio;
    }
}
