using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] Transform m_trsCamera;
    Vector3 m_vInitPos;
    bool m_bFixPos;
    [SerializeField] float m_fOffsetY;

    public void FixBackGroundPos()
    {
        m_bFixPos = true;
    }

    public void InitBackGround()
    {
        m_bFixPos = false;
        transform.position = m_vInitPos;
    }

    private void Start()
    {
        m_vInitPos = transform.position;
    }

    private void Update()
    {
        if (m_bFixPos)
        {
            Vector3 vNewPos = transform.position;
            vNewPos.y = m_trsCamera.position.y - m_fOffsetY;
            transform.position = vNewPos;
        }
            
    }
}
