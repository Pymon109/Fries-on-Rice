using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_TopBar : MonoBehaviour
{
    [SerializeField] Text m_txtStackCount;

    public void SetTopBarStackCount(int count)
    {
        m_txtStackCount.text = count.ToString();
    }

    [SerializeField] Text m_txtIngameGold;
    public void SetIngaeGold(int gold)
    {
        m_txtIngameGold.text = gold.ToString();
    }

    public void SetActiveIngameGoldUI(bool command)
    {
        gameObject.SetActive(command);
        m_txtStackCount.gameObject.SetActive(command);
    }
}
