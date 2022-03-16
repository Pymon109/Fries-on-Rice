using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> m_gFryPrefabs;
    [SerializeField] Transform m_trsStack;
    [SerializeField] AspectRatioControl m_ratioCon;
    float m_fFryMoveSpeed = 5.0f;
    public float fFryMoveSpeed { get { return m_fFryMoveSpeed; } set { m_fFryMoveSpeed = value; } }
    float m_fFryPingPongMaxPos;
    public float fFryPingPongMaxPos { set { m_fFryPingPongMaxPos = value; } }

    FryMovement m_Fry;
    public FryMovement Fry { get { return m_Fry; } }

    bool m_bSpawnBlock = false;
    public bool bSpawnBlock
    {
        get { return m_bSpawnBlock; }
        set
        {
            if (value)
            {
                if(m_Fry)
                    Destroy(m_Fry.gameObject);
            }
                
            m_bSpawnBlock = value;
        }
    }

    public void SpawnFry()
    {
        if (!m_bSpawnBlock)
        {
            GameObject obj_prefab = Resources.Load<GameObject>("Prefabs/Fries/" + m_gFryPrefabs[Random.RandomRange(0, m_gFryPrefabs.Count)].name);
            Vector3 pos = transform.position;
            pos.z = m_trsStack.transform.position.z;
            GameObject obj_new = Instantiate(obj_prefab, pos, obj_prefab.transform.rotation, m_trsStack);
            m_Fry = obj_new.GetComponent<FryMovement>();
            m_Fry.fSpeed = m_fFryMoveSpeed;
            m_Fry.SetOrderInLayer(IngameManager.instance.iOderLayer++);

            obj_new.transform.localScale *= m_ratioCon.fNewWidthRatio;
            m_Fry.fMaxPos = m_fFryPingPongMaxPos;
            m_Fry.fMaxPosCoef = m_ratioCon.fNewWidthRatio;

            IngameManager.instance.chopsticks.fryMove = m_Fry;
            IngameManager.instance.chopsticks.SetState(Chopsticks.E_CHOPSTICK_STATE.PINGPONG);
        }
    }

    public void InitSpawner()
    {
        m_bSpawnBlock = false;
        List<int> holdings;
        if (PlayerData.instance)
            holdings = PlayerData.instance.GetFryHoldingList();
        else
        {
            holdings = new List<int>();
            for (int i = 0; i < 20; i++)
                holdings.Add(i + 1);
        }
        foreach(int i in holdings)
        {
            GameObject prefabObj = Resources.Load<GameObject>("Prefabs/Fries/fry_" + i.ToString());
            m_gFryPrefabs.Add(prefabObj);
        }
    }

    private void Start()
    {
        InitSpawner();
    }
}
