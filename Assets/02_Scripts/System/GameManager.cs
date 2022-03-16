using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    ///////////////////////////////////////////////////////////////////////////////�ΰ��� ���Խ� ���̵� ���� �Ѱ��ֱ�
    IngameManager.E_GAMEMODE m_eGameMode;
    public IngameManager.E_GAMEMODE eGameMode { get { return m_eGameMode; } set { m_eGameMode = value; } }

    ///////////////////////////////////////////////////////////////////////////////�ΰ��� ���Խ� ������ ��� ���� �Ѱ��ֱ�
    IngameManager.S_ingameInfo m_ingameInfo;
    public IngameManager.S_ingameInfo ingameInfo { get { return m_ingameInfo; } set { m_ingameInfo = value; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Debug.LogWarning("���� �Ŵ��� ���� ����");
            Destroy(gameObject);
        }
        LoadingSceneManager.LoadScene("TitleScene");
    }
}
