using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorModeManager : MonoBehaviour
{
    [SerializeField] Dropdown m_ddPlate;
    [SerializeField] InputField m_ifPingPongMaxSize;
    [SerializeField] InputField m_ifPingPongSpeed;
    [SerializeField] InputField m_ifFryColiderSize;
    [SerializeField] InputField m_ifDynamicFryCount;
    [SerializeField] InputField m_ifDelay;

    public void DeliverIngameInfo()
    {
        float fPingPongMaxSize = 1f;
        if (m_ifPingPongMaxSize.text != "") fPingPongMaxSize = float.Parse(m_ifPingPongMaxSize.text);
        float fPingPongSpeed = 5f;
        if (m_ifPingPongSpeed.text != "") fPingPongSpeed  = float.Parse(m_ifPingPongSpeed.text);
        float fFryColiderSize = 1f;
        if (m_ifFryColiderSize.text != "") fFryColiderSize  = float.Parse(m_ifFryColiderSize.text);
        float fDelay = 0.75f;
        if (m_ifDelay.text != "") fDelay  = float.Parse(m_ifDelay.text);
        int iDynamicFryCount = 10;
        if (m_ifDynamicFryCount.text != "") iDynamicFryCount = int.Parse(m_ifDynamicFryCount.text);

        IngameManager.S_ingameInfo info = new IngameManager.S_ingameInfo(m_ddPlate.value,
            fPingPongMaxSize, fPingPongSpeed, fFryColiderSize, iDynamicFryCount, fDelay);
        GameManager.instance.ingameInfo = info;
        GameManager.instance.eGameMode = IngameManager.E_GAMEMODE.EDITOR;
        LoadingSceneManager.LoadScene("InGameScene"); 
    }

}
