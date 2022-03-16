using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FriesData : MonoBehaviour
{
    [SerializeField] List<FryData> m_listFryDatas;
    public List<FryData> AllFriesDatas { get { return m_listFryDatas; } }
    public List<FryData> GetSaleFries()
    {
        List<FryData> listSaleFries = new List<FryData>();
        for (int i = 0; i < m_listFryDatas.Count; i++)
            if (m_listFryDatas[i].eCompensation == FryData.E_FRY_COMPENSATION_TYPE.BUY)
                listSaleFries.Add(m_listFryDatas[i]);

        return listSaleFries;
    }
    public List<FryData> GetIAPFries()
    {
        List<FryData> listIAPFries = new List<FryData>();
        for (int i = 0; i < m_listFryDatas.Count; i++)
            if (m_listFryDatas[i].eCompensation == FryData.E_FRY_COMPENSATION_TYPE.IAP)
                listIAPFries.Add(m_listFryDatas[i]);

        return listIAPFries;
    }

    public List<FryData> GetPackageFries()
    {
        List<FryData> listPackageFries = new List<FryData>();
        for (int i = 0; i < m_listFryDatas.Count; i++)
            if (m_listFryDatas[i].eCompensation == FryData.E_FRY_COMPENSATION_TYPE.BUYPACKAGE)
                listPackageFries.Add(m_listFryDatas[i]);

        return listPackageFries;
    }

    public struct S_CompensationStackTuple
    {
        int m_iStackCount;
        int m_iFryID;

        public S_CompensationStackTuple(int stackCount = -1, int fryID = -1)
        {
            m_iStackCount = stackCount;
            m_iFryID = fryID;
        }
        public int stackCount { get { return m_iStackCount; } }
        public int fryID { get { return m_iFryID; } }
        public bool IsNotAssigned()
        {
            return m_iStackCount < 0 || m_iFryID < 0;
        }
        public void Deallocate()
        {
            m_iStackCount = -1;
            m_iFryID = -1;
        }
    }
    public List<S_CompensationStackTuple> GetCompensationStackCountAndFryID()
    {
        List<S_CompensationStackTuple> tuples = new List<S_CompensationStackTuple>();
        Dictionary<int, int> dictionaryForSort = new Dictionary<int, int>();
        for (int i = 0; i < m_listFryDatas.Count; i++)
        {
            if (m_listFryDatas[i].eCompensation == FryData.E_FRY_COMPENSATION_TYPE.STACK
                && !PlayerData.instance.GetFryHoldings(m_listFryDatas[i].iFryID))
            {
                dictionaryForSort.Add(m_listFryDatas[i].iCompensation_StackCount, m_listFryDatas[i].iFryID);
            }
        }
        var queryAsc = dictionaryForSort.OrderBy(x => x.Key);
        foreach (KeyValuePair<int, int> items in dictionaryForSort)
        {
            tuples.Add(new S_CompensationStackTuple(items.Key, items.Value));
        }
        return tuples;
    }
}
