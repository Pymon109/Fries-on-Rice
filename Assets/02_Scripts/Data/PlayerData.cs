using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    private PlayerInfo m_playerInfo;

    //////////////////////////////////////////////////////////////////플레이 카운트
    [SerializeField] int m_maxPlayCount;
    int m_playCount = 0;
    public bool IsPlayCountReachMaxValue()
    {
        if(m_playCount >= m_maxPlayCount)
        {
            m_playCount = 0;
            return true;
        }
        else
        {
            ++m_playCount;
            return false;
        }
    }

    //////////////////////////////////////////////////////////////////파일 출력
    string m_sFileName = "playerinfo";
    string m_sFilePath;
    void WriteData()
    {
        string jsonData = FileManager.instance.ObjectToJson(m_playerInfo);
        FileManager.instance.CreateJsonFile(m_sFilePath, m_sFileName, jsonData);
    }

    //////////////////////////////////////////////////////////////////재화 골드
    public void AddGold(int value) 
    {
        m_playerInfo.gold += value;
        Debug.Log("[File/DB] required to update gold");
        WriteData();
    }
    public bool SpendGold(int value)
    {
        if (m_playerInfo.gold < value)
            return false;
        else
        {
            m_playerInfo.gold -= value;
            SoundManager.instance.SFXAudioSourcePlay(SoundManager.E_SFX_TYPE.GET_GOLD);
            EffectManager.instance.CreateGoldEffect(value * -1);
            Debug.Log("[File/DB] required to update gold");
            WriteData();
            return true;
        }
    }
    public int playerGold { get { return m_playerInfo.gold; } }

    //////////////////////////////////////////////////////////////////광고 제거
    public bool isAdRemoved
    {
        get { return m_playerInfo.isAdRemoved; }
        set
        {
            m_playerInfo.isAdRemoved = value;
            AdmobManager.instance.ToggleBannerAd(false);
            Debug.Log("[File/DB] required to update isAdRemoved");
            WriteData();
        }
    }

    //////////////////////////////////////////////////////////////////보유중인 튀김
    Dictionary<int, bool> m_FryHoldings = new Dictionary<int, bool>();
    public void SetFryHoldings(int fryID) 
    { 
        if(!m_FryHoldings[fryID])
        {
            m_FryHoldings[fryID] = true;
            m_playerInfo.friesInStock.Add(fryID);
            Debug.Log("[File/DB] required to update fry holdings");
            WriteData();
        }
    }
    public bool GetFryHoldings(int fryID) { return m_FryHoldings[fryID]; }
    public List<int> GetFryHoldingList() { return m_playerInfo.friesInStock; }

    //////////////////////////////////////////////////////////////////최고 기록
    public int GetBestRecord(IngameManager.E_GAMEMODE mode)
    {
        int record = -1;
        switch(mode)
        {
            case IngameManager.E_GAMEMODE.NORMAL:
                record = m_playerInfo.bestRecord_normal;
                break;
            case IngameManager.E_GAMEMODE.HARD:
                record = m_playerInfo.bestRecord_hard;
                break;
        }
        return record;
    }
    public bool SetBestRecord(IngameManager.E_GAMEMODE mode, int record)
    {
        switch (mode)
        {
            case IngameManager.E_GAMEMODE.NORMAL:
                if (m_playerInfo.bestRecord_normal < record)
                {
                    m_playerInfo.bestRecord_normal = record;
                    Debug.Log("[Google] required to update leader board (normal)");
                    GameCenterManager.instance.ReportScore(mode, record);
                    WriteData();
                    return true;
                }
                break;
            case IngameManager.E_GAMEMODE.HARD:
                if (m_playerInfo.bestRecord_hard < record)
                {
                    m_playerInfo.bestRecord_hard = record;
                    Debug.Log("[Google] required to update leader board (hard)");
                    GameCenterManager.instance.ReportScore(mode, record);
                    WriteData();
                    return true;
                }
                break;
        }
        return false;
    }

    //////////////////////////////////////////////////////////////////언어 설정
    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("플레이어 데이터 오브젝트 복수 존재");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_sFilePath = Application.persistentDataPath + "/Json";
        if (FileManager.instance.JsonFileExists(m_sFilePath, m_sFileName))
            m_playerInfo = FileManager.instance.LoadJsonFile<PlayerInfo>(m_sFilePath, m_sFileName);
        else
            m_playerInfo = new PlayerInfo();
        m_playerInfo.Debug_Print();
        //AdmobManager.instance.ToggleBannerAd(!m_playerInfo.isAdRemoved);

        List<FryData> friesDatas = DataManager.instance.friesData.AllFriesDatas;
        for (int i = 0; i < friesDatas.Count; i++)
        {
            if(m_playerInfo.friesInStock.Contains(friesDatas[i].iFryID))
                m_FryHoldings.Add(friesDatas[i].iFryID, true);
            else
                m_FryHoldings.Add(friesDatas[i].iFryID, false);
        }
    }
}

[System.Serializable]
public class PlayerInfo
{
    public int gold;
    public bool isAdRemoved;
    public List<int> friesInStock;
    public int bestRecord_normal;
    public int bestRecord_hard;

    public PlayerInfo()
    {
        gold = 0;
        isAdRemoved = false;
        friesInStock = new List<int>();
        bestRecord_normal = 0;
        bestRecord_hard = 0;

        List<FryData> friesDatas = DataManager.instance.friesData.AllFriesDatas;
        for (int i = 0; i < friesDatas.Count; i++)
        {
            bool bIsHold = false;
            if (friesDatas[i].eCompensation == FryData.E_FRY_COMPENSATION_TYPE.NONE)
                friesInStock.Add(friesDatas[i].iFryID);
        }
    }

    public void Debug_Print()
    {
        Debug.Log(string.Format("gold : {0}, isAdRemoved : {1}", gold, isAdRemoved));

        string s = "";
        for (int i = 0; i <friesInStock.Count; i++)
        {
            s += friesInStock[i].ToString() + ", ";
        }
        Debug.Log("friesInStock : " + s);
    }
}