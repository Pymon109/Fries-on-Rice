using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_FryEncyclopediaScrollView : MonoBehaviour
{
    [SerializeField] List<FryEncyclopediaItem> m_listItems;
    [SerializeField] RectTransform m_rectGridLayoutGroup;
    [SerializeField] GameObject prefabFryItem;

    [SerializeField] GUI_PopUp m_guiPopUp;
    public GUI_PopUp popUp { get { return m_guiPopUp; } }

    public void SetDisplay()
    {
        List<FryData> friesDatas = DataManager.instance.friesData.AllFriesDatas;

        for (int i = 0; i < friesDatas.Count; i++)
        {
            GameObject newItem = Instantiate(prefabFryItem, m_rectGridLayoutGroup);
            FryEncyclopediaItem newitem_fryStoreItem = newItem.GetComponent<FryEncyclopediaItem>();
            newitem_fryStoreItem.SetItem(friesDatas[i]);
            m_listItems.Add(newitem_fryStoreItem);
        }

        ResizeContent(m_listItems.Count);
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < m_listItems.Count; i++)
            m_listItems[i].UpdateScreen();
    }

    public void ResizeContent(int itemCount)
    {
        Vector2 vRectSize = m_rectGridLayoutGroup.sizeDelta;
        Vector2 vCellSize = m_rectGridLayoutGroup.GetComponent<GridLayoutGroup>().cellSize;
        Vector2 vCpacing = m_rectGridLayoutGroup.GetComponent<GridLayoutGroup>().spacing;

        /*int nRowCount = (int)(vRectSize.x / vCellSize.x);
        int nColCount = itemCount / nRowCount;
        if (itemCount % nRowCount > 0) nColCount++;*/

        //vRectSize.y = (vCellSize.x + vCpacing.y) * nColCount;

        vRectSize.y = (vCellSize.y + vCpacing.y) * itemCount;
        m_rectGridLayoutGroup.sizeDelta = vRectSize;

        
    }

    private void Start()
    {
        SetDisplay();
    }
}
