using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameGold : MonoBehaviour
{
    int m_earnedGold = 0;
    public int earnedGold { get { return m_earnedGold; } }
    public void AddGold(int amount) 
    { 
        m_earnedGold += amount;
    }
    public void InitGold() 
    { 
        m_earnedGold = 0;
        IngameManager.instance.guiManager.gui_topBar.SetIngaeGold(m_earnedGold + PlayerData.instance.playerGold);
    }
}
