using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    static IngameManager m_unique;
    public static IngameManager instance { get { return m_unique; } }

    [SerializeField] InputManager m_inputManager;
    [SerializeField] EffectManager m_effectManager;
    public EffectManager effectManager { get { return m_effectManager; } }

    public struct S_ingameInfo
    {
        int m_iPlate;
        float m_fPingPongMaxSize;
        float m_fPingPongSpeed;
        float m_fFryColiderSize;
        int m_iDynamicFryCount;
        float m_fDelayTimeForResult;

        public S_ingameInfo(int plate, float pingpongMaxSize, float pingpongSpeed, float fryColiderSize, int dynamicFryCount, float fDelayTimeForResult)
        {
            m_iPlate = plate;
            m_fPingPongMaxSize = pingpongMaxSize;
            m_fPingPongSpeed = pingpongSpeed;
            m_fFryColiderSize = fryColiderSize;
            m_iDynamicFryCount = dynamicFryCount;
            m_fDelayTimeForResult = fDelayTimeForResult;
        }
        public int iPlate { get { return m_iPlate; } }
        public float fPingPongMaxSize { get { return m_fPingPongMaxSize; } }
        public float fPingPongSpeed { get { return m_fPingPongSpeed; } }
        public float fFryColiderSize { get { return m_fFryColiderSize; } }
        public int iDynamicFryCount { get { return m_iDynamicFryCount; } }
        public float fDelayTimeForResult { get { return m_fDelayTimeForResult; } }
    }
    S_ingameInfo m_ingameInfo;
    public S_ingameInfo ingmaeInfo { get { return m_ingameInfo; } }

    Spawner m_spawner;
    public Spawner spawner { get { return m_spawner; } }
    GUIManager m_gui;
    Chopsticks m_chopstics;
    public Chopsticks chopsticks { get { return m_chopstics; } }
    public GUIManager guiManager { get { return m_gui; } }
    StackCamera m_camera;
    public StackCamera stackCamera { get { return m_camera; } }

    Stack m_stack;
    public Stack stack { get { return m_stack; } }
    [SerializeField] Transform m_trsPlate;

    [SerializeField] Transform m_trsBoundary;
    Vector3 m_v3InitBoundaryPos;
    public void SetBoundaryHeight(float newHeight)
    {
        Vector3 newPos = m_trsBoundary.position;
        newPos.y = newHeight;
        m_trsBoundary.position = newPos;
        for(int i = 0; i < m_trsBoundary.childCount; i++)
            m_trsBoundary.GetChild(i).GetComponent<Boundary>().SetBoundaryHeight();
    }

    public enum E_GAMEMODE
    {
        NORMAL = 0,
        HARD,
        EDITOR
    }
    [SerializeField] E_GAMEMODE m_eGameMode;
    public E_GAMEMODE gameMode { get { return m_eGameMode; } set { m_eGameMode = value; } }

    IngameGold m_ingameGold;
    public IngameGold ingameGold { get { return m_ingameGold; } }

    int m_iOrderLayer = 0;
    public int iOderLayer { get { return m_iOrderLayer; } set { m_iOrderLayer = value; } }

    /////////////////////////////////////////////////////////////////// 인게임 상태 유한상태머신
    public enum E_INGAMESTATE
    {
        INIT = 0,
        INPROGRESS,
        GAMEOVER,
        STOP
    }
    public E_INGAMESTATE m_eGameState;

    void UpdateState()
    {
        switch(m_eGameState)
        {
            case E_INGAMESTATE.INIT:
                SetState(E_INGAMESTATE.INPROGRESS);
                break;
            case E_INGAMESTATE.INPROGRESS:
                break;
            case E_INGAMESTATE.GAMEOVER:
                Time.fixedDeltaTime = 0.02f * 0.1f;
                break;
            case E_INGAMESTATE.STOP:
                break;
        }
    }
    public void SetState(E_INGAMESTATE command)
    {
        switch (command)
        {
            case E_INGAMESTATE.INIT:
                m_inputManager.bDropBlock = false;
                Time.timeScale = 1.0f;
                InitGame();
                break;
            case E_INGAMESTATE.INPROGRESS:
                m_inputManager.bDropBlock = false;
                Time.timeScale = 1.0f;
                break;
            case E_INGAMESTATE.GAMEOVER:
                if(m_eGameState != E_INGAMESTATE.GAMEOVER)
                {
                    PlayerData.instance.AddGold(m_ingameGold.earnedGold);
                    PlayerData.instance.SetBestRecord(gameMode, m_stack.GetStackCount());
                    m_inputManager.bDropBlock = true;
                    m_spawner.bSpawnBlock = true;

                    m_camera.fNewCameraPositionY = m_stack.GetTheBottomFryPosY();
                    m_camera.bGameOver = true;

                    //m_stack.ReleaseFixedFry();
                    m_stack.UpdateFyFaceFail();

                    Time.timeScale = 0.1f;
                    //딜레이 주는 구문 필요
                    //StartCoroutine(WaitForResultUI());
                    guiManager.ShowResult(true);
                    
                }
                break;
            case E_INGAMESTATE.STOP:
                m_inputManager.bDropBlock = true;
                Time.timeScale = 0f;
                break;
        }
        m_eGameState = command;
    }
    /////////////////////////////////////////////////////////////////// 종료창 노출을 위한 딜레이
/*    [SerializeField] float m_fDelayTimeForResult;
    IEnumerator WaitForResultUI()
    {
        yield return new WaitForSeconds(m_fDelayTimeForResult);
        guiManager.ShowResult(true);
    }*/

    /////////////////////////////////////////////////////////////////// 인게임 초기화
    public void InitGame()
    {
        if(PlayerData.instance)
        {
            if (PlayerData.instance.IsPlayCountReachMaxValue())
                AdmobManager.instance.ShowFrontAd();
        }

        if (GameManager.instance)
            m_eGameMode = GameManager.instance.eGameMode;
        else
            m_eGameMode = E_GAMEMODE.NORMAL;

        //float fFryMoveSpeed = 0;

        switch (m_eGameMode)
        {
            case E_GAMEMODE.NORMAL:
                m_ingameInfo = new S_ingameInfo((int)m_eGameMode,1.0f, 5.0f, 1.0f, 10, 0.75f);
                //fFryMoveSpeed = 5.0f;
                break;
            case E_GAMEMODE.HARD:
                m_ingameInfo = new S_ingameInfo((int)m_eGameMode, 1.0f, 7.0f, 1.0f, 10, 0.75f);
               //fFryMoveSpeed = 7.0f;
                break;
            case E_GAMEMODE.EDITOR:
                m_ingameInfo = GameManager.instance.ingameInfo;
                m_eGameMode = (E_GAMEMODE)m_ingameInfo.iPlate;
                break;
        }

        //골드
        m_ingameGold.InitGold();

        //밥그릇
        for(int i = 0; i < m_trsPlate.childCount; i++)
        {
            if (i == (int)m_eGameMode)
                m_trsPlate.GetChild(i).gameObject.SetActive(true);
            else
                m_trsPlate.GetChild(i).gameObject.SetActive(false);
        }

        //스포너
        m_spawner.fFryMoveSpeed = m_ingameInfo.fPingPongSpeed;
        m_spawner.fFryPingPongMaxPos = m_ingameInfo.fPingPongMaxSize;
        m_spawner.bSpawnBlock = false;

        //스택 초기화
        for (int i = 0; i < m_stack.transform.childCount; i++)
            Destroy(m_stack.transform.GetChild(i).gameObject);
        m_stack.InitStack();
        m_stack.maxDynamicFriedCount = ingmaeInfo.iDynamicFryCount;

        //카메라
        m_camera.CameraInit();

        //바운더리
        m_trsBoundary.position = m_v3InitBoundaryPos;
        SetBoundaryHeight(m_trsBoundary.position.y);

        m_iOrderLayer = 0;

        //GUI
        m_gui.fDelayTimeForResult = m_ingameInfo.fDelayTimeForResult * 0.1f;
        m_gui.ShowResult(false);
        m_spawner.SpawnFry();
        //m_fDelayTimeForResult = m_ingameInfo.fDelayTimeForResult * 0.1f;
        
    }

    /////////////////////////////////////////////////////////////////// 게임 정지
    public void GameStop()
    {
        SetState(E_INGAMESTATE.STOP);
    }
    public void GameContinue()
    {
        SetState(E_INGAMESTATE.INPROGRESS);
    }

    [SerializeField] ScreenState m_screeanStateIngame;
    [SerializeField] ScreenState m_screenStateIngameStop;

    private void Awake()
    {
        m_unique = this;
        m_ingameGold = GetComponent<IngameGold>();
        m_spawner = FindObjectOfType<Spawner>();
        m_gui = FindObjectOfType<GUIManager>();
        m_camera = FindObjectOfType<StackCamera>();
        m_stack = FindObjectOfType<Stack>();
        m_chopstics = FindObjectOfType<Chopsticks>();
    }

    private void Start()
    {
        //InitGame();
        m_v3InitBoundaryPos = m_trsBoundary.position;
        SetState(E_INGAMESTATE.INIT);

        HardwareInputManager.instance.PushStateStack(m_screenStateIngameStop);
        HardwareInputManager.instance.PushStateStack(m_screeanStateIngame);
    }

    private void Update()
    {
        UpdateState();
    }
}
