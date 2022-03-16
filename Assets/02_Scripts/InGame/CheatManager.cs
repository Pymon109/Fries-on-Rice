using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    [SerializeField] int _nX;
    [SerializeField] int _nY;

    public void cheat_GetAllFires()
    {
        List<FryData> fries = DataManager.instance.friesData.AllFriesDatas;
        for (int i = 0; i < fries.Count; i++)
        {
            if (!PlayerData.instance.GetFryHoldings(fries[i].iFryID))
                PlayerData.instance.SetFryHoldings(fries[i].iFryID);
        }
    }

    private void OnGUI()
    {
        int nWidth = 200;
        int nHeight = 40;

        int nX = _nX;
        int nY = _nY;

        /*if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "start normal"))
        {
            IngameManager.instance.gameMode = IngameManager.E_GAMEMODE.NORMAL;
            IngameManager.instance.InitGame();
        }
        if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "start hard"))
        {
            IngameManager.instance.gameMode = IngameManager.E_GAMEMODE.HARD;
            IngameManager.instance.InitGame();
        }*/

/*        if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "get all fries."))
        {
            List<FryData> fries = DataManager.instance.friesData.AllFriesDatas;
            for (int i = 0; i < fries.Count; i++)
            {
                if (!PlayerData.instance.GetFryHoldings(fries[i].iFryID))
                    PlayerData.instance.SetFryHoldings(fries[i].iFryID);
            }
        }*/

/*        if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "test scene"))
        {
            LoadingSceneManager.LoadScene("Test");
        }

        if (GUI.Button(new Rect(nWidth * nX, nHeight * nY++, nWidth, nHeight), "title scene"))
        {
            LoadingSceneManager.LoadScene("TitleScene");
        }*/

    }
}
