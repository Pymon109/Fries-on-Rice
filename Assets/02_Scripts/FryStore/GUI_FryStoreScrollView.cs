using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_FryStoreScrollView : MonoBehaviour
{
    [SerializeField] RectTransform m_rectGridLayoutGroup;
    [SerializeField] GameObject prefabFryItem;
    [SerializeField] DragSnapper m_dragSnapper;
    [SerializeField] DragSnapper m_guiScrollBarCon;

    [SerializeField] GUI_FryStore m_guiFryStore;
    public GUI_FryStore guiFryStore { get { return m_guiFryStore; } set { m_guiFryStore = value; } }

    List<FryStoreItem> m_listItems = new List<FryStoreItem>();

    public void SetDisplay()
    {
        //List<FryData> friesDatas = DataManager.instance.friesData.GetSaleFries();
        List<FryData> friesDatas = DataManager.instance.friesData.AllFriesDatas;

        for (int i = 0; i < friesDatas.Count; i++)
        {
            GameObject newItem = Instantiate(prefabFryItem, m_rectGridLayoutGroup);
            FryStoreItem newitem_fryStoreItem = newItem.GetComponent<FryStoreItem>();
            newitem_fryStoreItem.SetItem(friesDatas[i]);
            newitem_fryStoreItem.guiPopUp = m_guiFryStore.guiPopUp;
            m_listItems.Add(newitem_fryStoreItem);
        }

            ResizeContent(m_listItems.Count);
        m_dragSnapper.itemCount = m_listItems.Count;
        m_guiScrollBarCon.itemCount = m_listItems.Count;
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < m_listItems.Count; i++)
            m_listItems[i].UpdateItem();
    }

    public void ResizeContent(int itemCount)
    {
        Vector2 vRectSize = m_rectGridLayoutGroup.sizeDelta;
        Vector2 vCellSize = m_rectGridLayoutGroup.GetComponent<GridLayoutGroup>().cellSize;

        vRectSize.x = vCellSize.x * itemCount;
        m_rectGridLayoutGroup.sizeDelta = vRectSize;
    }

    private void Start()
    {
        SetDisplay();
    }
}
