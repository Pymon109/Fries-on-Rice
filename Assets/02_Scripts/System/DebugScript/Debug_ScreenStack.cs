using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_ScreenStack : MonoBehaviour
{
    [SerializeField] GameObject m_txtTemplete;

    Stack<GameObject> m_stack = new Stack<GameObject>();

    public void AddScrrenLog(ScreenState screen)
    {
        GameObject newObj =  Instantiate(m_txtTemplete, transform);
        Text newText = newObj.GetComponent<Text>();
        newText.text = screen.ToString();
        m_stack.Push(newObj);
    }

    public void PopText()
    {
        GameObject popObj = m_stack.Pop();
        Destroy(popObj);
    }
}
