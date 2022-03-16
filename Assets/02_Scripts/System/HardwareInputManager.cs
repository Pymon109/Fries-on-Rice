using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardwareInputManager : MonoBehaviour
{
    static HardwareInputManager unique;
    public static HardwareInputManager instance { get { return unique; } }

    [SerializeField] ScreenState m_appQuit;
    [SerializeField] ScreenState m_title;

    public Stack<ScreenState> m_stateStack = new Stack<ScreenState>();

    [SerializeField] Debug_ScreenStack m_debug;

    public void EscapeCurrentState()
    {
        //Debug.Log("Escape Current State");
        ScreenState screenState = m_stateStack.Pop();
        screenState.DoBackProcess();
        DebugPrint();
        m_debug.PopText();
    }

    public void PushStateStack(ScreenState state)
    {
        //Debug.Log("Push State");
        m_stateStack.Push(state);
        DebugPrint();
        m_debug.AddScrrenLog(state);
    }

    public void JustPopStateStack()
    {
        //Debug.Log("Just Pop State");
        m_stateStack.Pop();
        DebugPrint();
        m_debug.PopText();
    }

    void DebugPrint()
    {
        string text = "";
        ScreenState[] arr = m_stateStack.ToArray();
        for (int i = 0; i < arr.Length; i++)
            text += string.Format("[{0}] {1} /", i.ToString(), arr[i]);
        Debug.Log(text);
    }

    private void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                EscapeCurrentState();
            }
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EscapeCurrentState();
        }
#endif
    }

    private void Awake()
    {
        if (unique == null)
        {
            unique = this;
        }
        else
        {
            Debug.LogWarning("하드웨어 인풋 매니저 복수 존재");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        m_stateStack.Push(m_appQuit);
        m_debug.AddScrrenLog(m_appQuit);
        m_stateStack.Push(m_title);
        m_debug.AddScrrenLog(m_title);
    }
}
