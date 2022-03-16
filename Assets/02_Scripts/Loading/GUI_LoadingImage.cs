using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_LoadingImage : MonoBehaviour
{
    public bool m_bJump = false;
    private void Update()
    {
        if(m_bJump)
        {
            //사운드 재생
            m_bJump = false;
        }

    }
}
