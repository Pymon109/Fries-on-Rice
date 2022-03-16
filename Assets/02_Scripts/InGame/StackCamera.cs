using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackCamera : MonoBehaviour
{

    [SerializeField] float m_fSpeed;
    [SerializeField] float m_fCameraOffsetY;
    [SerializeField] float m_fGameOverSpeed;

    [SerializeField] CameraTracker m_backIMG;
    [SerializeField] float m_fEnfPos;

    [SerializeField] AspectRatioControl m_rectRatioCon;

    bool m_bMoveCamera;
    bool m_bGameOver = false;
    public bool bGameOver { set { m_bGameOver = value; } }
    public bool bMoveCamera { set { m_bMoveCamera = value; } }
    float m_fNewCameraPositionY;
    public float fNewCameraPositionY { set { m_fNewCameraPositionY = value; } }

    Vector3 m_initPosition;

    public void CameraInit()
    {
        transform.position = m_initPosition;
        m_bMoveCamera = false;
        m_bGameOver = false;
        m_fNewCameraPositionY = 0;
        m_backIMG.InitBackGround();
    }

    private void Awake()
    {
        m_initPosition = transform.position;
    }

    private void Update()
    {
        if(m_bGameOver)
        {
            if(transform.position.y >=1)
            {
                transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(transform.position.y, m_fNewCameraPositionY, m_fGameOverSpeed * Time.deltaTime),
                transform.position.z);
            }
        }
        else if(m_bMoveCamera)
        {
            transform.position = new Vector3(transform.position.x, 
                Mathf.Lerp(transform.position.y, m_fNewCameraPositionY + (m_fCameraOffsetY * m_rectRatioCon.fNewHeightRatio), m_fSpeed * Time.deltaTime), 
                transform.position.z);

            if(transform.position.y >= m_fNewCameraPositionY + (m_fCameraOffsetY * m_rectRatioCon.fNewHeightRatio))
            {
                m_bMoveCamera = false;
            }
        }
        if (transform.position.y >= m_fEnfPos)
        {
            m_backIMG.FixBackGroundPos();
        }
    }
}
