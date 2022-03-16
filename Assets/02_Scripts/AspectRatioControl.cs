using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioControl : MonoBehaviour
{
    RectTransform m_rectTrs;

    float m_fStandard = 9.0f / 16.0f;

    float m_fWidth;
    float m_fHeight;

    public float fNewWidthRatio = 1;
    public float fNewHeightRatio = 1;

    [SerializeField] bool m_bIngame;

    [SerializeField] List<RectTransform> m_rects;

    private void Start()
    {
        m_fWidth = Screen.width;
        m_fHeight = Screen.height;

        Debug.Log(string.Format("w : {0} / h : {1}", m_fWidth, m_fHeight));
        Debug.Log(string.Format("w:h = {0}:1", m_fWidth / m_fHeight));
        Debug.Log(string.Format("standard w:h = {0}:1", m_fStandard));

        fNewWidthRatio = (m_fWidth / m_fHeight) / m_fStandard;

        if(!m_bIngame)
        {
            if (fNewWidthRatio > 1.1f)
                fNewWidthRatio = 1.1f;
            else if (fNewWidthRatio < 0.88f)
                fNewWidthRatio = 0.88f;
        }

        Debug.Log(string.Format("fNewWidthRatio = {0}", fNewWidthRatio));

        fNewHeightRatio = (m_fHeight / m_fWidth) / (1 / m_fStandard);

        Debug.Log(string.Format("fNewHeightRatio = {0}", fNewHeightRatio));

        Vector3 vNewScale = Vector3.one * fNewWidthRatio;
        vNewScale.z = 1f;
        for (int i = 0; i < m_rects.Count; i++)
            m_rects[i].localScale = vNewScale;
    }
}
