using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    static FileManager unique;
    public static FileManager instance { get { return unique; } }

    [SerializeField] Text m_txtLog;

    //////////////////////////////////////////////////////////////////json 파일과 오브젝트 간의 변환
    public string ObjectToJson(object obj)  { return JsonConvert.SerializeObject(obj, Formatting.Indented); }
    T JsonToObject<T>(string jsonData) { return JsonUtility.FromJson<T>(jsonData); }

    //////////////////////////////////////////////////////////////////json 파일 읽기 쓰기
    public void CreateJsonFile(string filePath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(Path.Combine(filePath, fileName + ".json"), FileMode.Create, FileAccess.Write);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
        m_txtLog.text = Path.Combine(filePath, fileName + ".json") + " 쓰기";
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(Path.Combine(loadPath, fileName + ".json"), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        m_txtLog.text = Path.Combine(loadPath, fileName + ".json") + " 읽기";
        return JsonUtility.FromJson<T>(jsonData);
    }

    //////////////////////////////////////////////////////////////////json 파일 존재 체크
    public bool JsonFileExists(string loadPath, string fileName)
    {
        bool result = File.Exists(Path.Combine(loadPath, fileName + ".json"));
        if (result)
            m_txtLog.text = Path.Combine(loadPath, fileName + ".json") + " 존재";
        else
            m_txtLog.text = Path.Combine(loadPath, fileName + ".json") + " 미존재";
        return result;
    }

    //////////////////////////////////////////////////////////////////딕셔너리
    struct S_Language
    {
        string m_sKor;
        string m_sEng;

        public string kor { get { return m_sKor; } }
        public string eng { get { return m_sEng;} }
        public S_Language(string kor, string eng)
        {
            m_sKor = kor;
            m_sEng = eng;
        }
    }

    //////////////////////////////////////////////////////////////////cvs 읽기
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public List<Dictionary<string, object>> CVSRead(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }


    private void Start()
    {
        string sDirPath;

        sDirPath = Application.persistentDataPath + "/Json";
        DirectoryInfo di = new DirectoryInfo(sDirPath);
        if (di.Exists == false)
            di.Create();

    }

    private void Awake()
    {
        if (unique == null)
        {
            unique = this;
        }
        else
        {
            Debug.LogWarning("파일 매니저 복수 존재");
            Destroy(gameObject);
        }
    }
}
