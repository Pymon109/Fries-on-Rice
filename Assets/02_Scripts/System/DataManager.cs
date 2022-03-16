using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager unique;
    public static DataManager instance { get { return unique; } }

    //////////////////////////////////////////////////////////////////플레이어 데이터
    [SerializeField] PlayerData m_playerData;
    public PlayerData playerData { get { return m_playerData; } }

    //////////////////////////////////////////////////////////////////튀김 데이터
    [SerializeField] FriesData m_friesData;
    public FriesData friesData { get { return m_friesData; } }

    //////////////////////////////////////////////////////////////////언어 데이터
    [SerializeField] LanguageData m_languageData;
    public LanguageData languageData { get { return m_languageData; } }

    private void Awake()
    {
        if (instance == null)
        {
            unique = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
