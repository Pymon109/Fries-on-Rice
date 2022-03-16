using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    static EffectManager unique;
    public static EffectManager instance { get { return unique; } }

    [SerializeField] List<GameObject> m_effectPrefabs;

    [SerializeField] Transform m_trsIngameGoldUI;

    public enum E_EFFECT_TYPE
    {
        GOLD = 0,
        DEBRIS,
        GOLD_IMG,
        CLICK
    }
    public void CreateEffect(E_EFFECT_TYPE type, Vector3 vPos)
    {
       GameObject newEffect =  Instantiate(m_effectPrefabs[(int)type], vPos, m_effectPrefabs[(int)type].transform.rotation);
       Destroy(newEffect, 1f);
    }

    public void CreateClickEffect(Transform trs)
    {
        GameObject newEffect = Instantiate(m_effectPrefabs[(int)E_EFFECT_TYPE.CLICK], trs.localPosition, m_effectPrefabs[(int)E_EFFECT_TYPE.CLICK].transform.rotation, trs);
        Destroy(newEffect, 1f);
    }

    public void CreateGoldEffect(int amount)
    {
        GameObject newObj = Instantiate(m_effectPrefabs[(int)E_EFFECT_TYPE.GOLD], transform);
        Effect_text.E_TEXT_EFFECT_TYPE type = Effect_text.E_TEXT_EFFECT_TYPE.GAIN;
        string front = "+";
        if (amount < 0)
        {
            front = "";
            type = Effect_text.E_TEXT_EFFECT_TYPE.SPEND;
        }
       
        newObj.GetComponent<Effect_text>().SetText(front + amount.ToString(), type);
        SoundManager.instance.SFXAudioSourcePlay(SoundManager.E_SFX_TYPE.GET_GOLD);
    }

    public void CreateGoldIMGEffect(Transform parent, Vector3 vPos, Transform ingameGoldUI ,int amount)
    {
        GameObject newObj = Instantiate(m_effectPrefabs[(int)E_EFFECT_TYPE.GOLD_IMG], parent);
        Effect_gold effect_Gold = newObj.GetComponent<Effect_gold>();
        effect_Gold.trsIngameGoldUI = ingameGoldUI;
        effect_Gold.iAmount = amount;
        //newObj.GetComponent<Effect_gold>().trsIngameGoldUI = ingameGoldUIPos;
        //SoundManager.instance.SFXAudioSourcePlay(SoundManager.E_SFX_TYPE.GET_GOLD);
    }

    private void Awake()
    {
        if (unique == null)
        {
            unique = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("이팩트 매니저 복수 존재");
            Destroy(gameObject);
        }
    }
}
