using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageData : MonoBehaviour
{
    public enum E_LANGUAGE
    {
        KOR = 0,
        ENG,
        MAX_COUNT
    }
    public struct S_Language
    {
        string m_sKor;
        string m_sEng;

        public string kor { get { return m_sKor; } }
        public string eng { get { return m_sEng; } }
        public S_Language(string kor, string eng)
        {
            m_sKor = kor;
            m_sEng = eng;
        }
    }
    Dictionary<string, S_Language> m_dicLanguages = new Dictionary<string, S_Language>();
    public S_Language GetStrings(string id) { return m_dicLanguages[id]; }

    private void Start()
    {
        List<Dictionary<string, object>> data = FileManager.instance.CVSRead("language data");

        for (int i = 0; i < data.Count; i++)
        {
            m_dicLanguages.Add(data[i]["id"].ToString(), new S_Language(data[i]["kor"].ToString(), data[i]["eng"].ToString()));
        }
    }
}
