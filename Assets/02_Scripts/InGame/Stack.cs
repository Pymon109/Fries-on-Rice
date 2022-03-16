using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] int m_maxDynamicFriedCount;
    public int maxDynamicFriedCount { set { m_maxDynamicFriedCount = value; } }

    [SerializeField] int m_minCountForGold;
    [SerializeField] int m_conditionForGold;
    [SerializeField] int m_provisionGold;

    int m_fryCountForGold = 0;

    [SerializeField] List<FryMovement> m_dynamicFries = new List<FryMovement>();
    [SerializeField] List<FryMovement> m_fixedFries = new List<FryMovement>();

    [SerializeField] Queue<FriesData.S_CompensationStackTuple> m_compensationStackCountQueue = new Queue<FriesData.S_CompensationStackTuple>();
    [SerializeField] FriesData.S_CompensationStackTuple m_currentCompensationStackTuple;

    public int GetStackCount() { return m_dynamicFries.Count + m_fixedFries.Count; }

    [SerializeField] Boundary m_boundary;

    FryMovement m_endFry;

    [SerializeField] float m_fFallenForce;

    [SerializeField] AspectRatioControl m_ratioCon;

    public void StackFry(FryMovement fry) 
    {
        m_fryCountForGold++;

        if(m_dynamicFries.Count > 0)
        Debug.Log((m_dynamicFries[m_dynamicFries.Count - 1].transform.position.x -
            fry.transform.position.x) / m_ratioCon.fNewWidthRatio);

        if (m_dynamicFries.Count >= m_maxDynamicFriedCount)
        {
            m_dynamicFries[0].FixFry();
            IngameManager.instance.SetBoundaryHeight(m_dynamicFries[0].transform.position.y - 3);
            m_fixedFries.Add(m_dynamicFries[0]);
            m_dynamicFries.RemoveAt(0);
        }
        m_dynamicFries.Add(fry);

        int stackCount = GetStackCount();
        if (stackCount >= m_minCountForGold)
        {
            if (m_fryCountForGold >= m_conditionForGold)
            {
                if(EffectManager.instance)
                    EffectManager.instance.CreateGoldIMGEffect(IngameManager.instance.guiManager.transform ,fry.transform.position, IngameManager.instance.guiManager.gui_topBar.transform ,m_provisionGold);
                fry.GetComponent<FrySpriteColorEffect>().SwitchOn();
                IngameManager.instance.ingameGold.AddGold(m_provisionGold);
                m_fryCountForGold = 0;
            }
        }

        if (!m_currentCompensationStackTuple.IsNotAssigned()
            && stackCount + 1 >= m_currentCompensationStackTuple.stackCount)
        {
            PlayerData.instance.SetFryHoldings(m_currentCompensationStackTuple.fryID);
            //Debug.Log(m_currentCompensationStackTuple.fryID + "π¯ ∆¢±Ë »πµÊ Ω∫≈√ : " + m_currentCompensationStackTuple.stackCount);
            if (m_compensationStackCountQueue.Count > 0)
                m_currentCompensationStackTuple = m_compensationStackCountQueue.Dequeue();
            else
                m_currentCompensationStackTuple.Deallocate();
        }
        m_endFry = fry;
        IngameManager.instance.guiManager.gui_topBar.SetTopBarStackCount(GetStackCount());
    }

    /////////////////////////////////////////////////////////////////// «»Ω∫µ» ∆¢±ËµÈ «Æ±‚
    public void ReleaseFixedFry()
    {
        for(int i = 0; i < m_fixedFries.Count; i++)
        {
            Rigidbody2D rb = m_fixedFries[i].GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(Vector2.up * m_fFallenForce);
        }
    }

    /////////////////////////////////////////////////////////////////// ∆¢±Ë «•¡§ ∫Ø»≠
    float m_preAngle = 0f;
    public void DetectAngle()
    {
        if(m_endFry)
        {
            Vector3 vStand = m_dynamicFries[0].transform.position;
            Vector3 vNewStack = m_endFry.transform.position;
            Vector3 v = vStand - vNewStack;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

            float distance = m_preAngle - angle;
            Debug.Log("Angle : " + distance);

            m_preAngle = angle;

        }
    }

    public void UpdateFyFaceFail()
    {
        Rigidbody2D rb;
        for (int i = 0; i< m_dynamicFries.Count; i++)
        {
            m_dynamicFries[i].FixFryFaceFall();

            rb = m_dynamicFries[i].GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
        }
        for(int i = 0; i < m_fixedFries.Count; i++)
        {
            m_fixedFries[i].FixFryFaceFall();
        }
    }

    /////////////////////////////////////////////////////////////////// ∞‘¿” ø¿πˆ ƒ´∏ﬁ∂Û ø¨√‚ Y position
    public float GetTheBottomFryPosY()
    {
        if(m_dynamicFries.Count > 0)
            return m_dynamicFries[0].transform.position.y;
        return 0;
    }

    /////////////////////////////////////////////////////////////////// Ω∫≈√ √ ±‚»≠
    public void InitStack()
    {
        m_dynamicFries.Clear();
        m_fixedFries.Clear();

        m_compensationStackCountQueue.Clear();
        List<FriesData.S_CompensationStackTuple> compensationStackCounts;
        if (DataManager.instance)
        {
            compensationStackCounts = DataManager.instance.friesData.GetCompensationStackCountAndFryID();
        }
        else
            compensationStackCounts = new List<FriesData.S_CompensationStackTuple>();
        for (int i = 0; i < compensationStackCounts.Count; i++)
            m_compensationStackCountQueue.Enqueue(compensationStackCounts[i]);
        if(m_compensationStackCountQueue.Count > 0)
            m_currentCompensationStackTuple = m_compensationStackCountQueue.Dequeue();
        else
            m_currentCompensationStackTuple.Deallocate();
        m_endFry = null;

        IngameManager.instance.guiManager.gui_topBar.SetTopBarStackCount(GetStackCount());
    }
}
