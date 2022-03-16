using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;
    public GameObject optionUIs;
    Dictionary<string, E_OPTION_VALUE> m_optionDatas;
    public E_OPTION_VALUE GetCurrentOptionValue(E_OPTION_TYPE type) { return m_optionDatas[GetKey(type)]; }

    public enum E_OPTION_VALUE
    {
        ON = 0,
        OFF,
        KOR,
        ENG
    }
    public int GetValue(E_OPTION_VALUE value) { return (int)value; }
    public enum E_OPTION_TYPE
    {
        BGM = 0,
        SFX,
        LANGAUGE,
        MAX_COUNT
    }
    [SerializeField] string[] m_sPlayerPrefsKes = new string[(int)E_OPTION_TYPE.MAX_COUNT];
    string GetKey(E_OPTION_TYPE type) { return m_sPlayerPrefsKes[(int)type]; }

    public bool CompareOption(E_OPTION_TYPE type, E_OPTION_VALUE value) { return m_optionDatas[GetKey(type)] == value; }


    private void OptionLoad()
    {
        m_optionDatas = new Dictionary<string, E_OPTION_VALUE>();
        LoadOption(E_OPTION_TYPE.BGM);
        LoadOption(E_OPTION_TYPE.SFX);
        LoadOption(E_OPTION_TYPE.LANGAUGE);
    }
    private void LoadOption(E_OPTION_TYPE type)
    {
        string key = GetKey(type);
        if (!PlayerPrefs.HasKey(key))
        {
            E_OPTION_VALUE value;
            if (type == E_OPTION_TYPE.LANGAUGE)
                value = E_OPTION_VALUE.ENG;
            else
                value = E_OPTION_VALUE.ON;
            m_optionDatas.Add(key, value);
            SetOption(type, value);
        }
        else m_optionDatas.Add(key, (E_OPTION_VALUE)PlayerPrefs.GetInt(key));
    }

    public void SetOption(E_OPTION_TYPE type, E_OPTION_VALUE value)
    {
        string key = GetKey(type);

        m_optionDatas[key] = value;
        PlayerPrefs.SetInt(GetKey(type), GetValue(value));
    }

    public void OptionOpen()
    {
        optionUIs.gameObject.SetActive(true);
        optionUIs.GetComponent<OptionUI>().UpdateOptionUI();
        HardwareInputManager.instance.PushStateStack(optionUIs.GetComponent<ScreenState>());
    }
    public void OptionClose()
    {
        optionUIs.gameObject.SetActive(false);
        HardwareInputManager.instance.EscapeCurrentState();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("사운드 매니저 복수 존재");
            Destroy(gameObject);
        }
        OptionLoad();
    }
}
