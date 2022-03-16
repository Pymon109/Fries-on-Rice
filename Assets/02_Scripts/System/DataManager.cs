using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager unique;
    public static DataManager instance { get { return unique; } }

    //////////////////////////////////////////////////////////////////�÷��̾� ������
    [SerializeField] PlayerData m_playerData;
    public PlayerData playerData { get { return m_playerData; } }

    //////////////////////////////////////////////////////////////////Ƣ�� ������
    [SerializeField] FriesData m_friesData;
    public FriesData friesData { get { return m_friesData; } }

    //////////////////////////////////////////////////////////////////��� ������
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
