using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    ///////////////////////////////////////////////////////////////////////////////인게임 진입시 난이도 정보 넘겨주기
    IngameManager.E_GAMEMODE m_eGameMode;
    public IngameManager.E_GAMEMODE eGameMode { get { return m_eGameMode; } set { m_eGameMode = value; } }

    ///////////////////////////////////////////////////////////////////////////////인게임 진입시 에디터 모드 정보 넘겨주기
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
            Debug.LogWarning("게임 매니저 복수 존재");
            Destroy(gameObject);
        }
        LoadingSceneManager.LoadScene("TitleScene");
    }
}
