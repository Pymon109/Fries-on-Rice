using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_gold : MonoBehaviour
{
    GUI_TopBar m_guiTopBar;
    Transform m_trsIngameGoldUI;
    public Transform trsIngameGoldUI { set { m_trsIngameGoldUI = value; } }
    [SerializeField] float m_fSpeed;
    [SerializeField] float m_fDuration;

    int m_iAmount;
    public int iAmount { set { m_iAmount = value; } }

    IEnumerator CreateTextEffectAndDestroySelf()
    {
        yield return new WaitForSeconds(m_fDuration);
        EffectManager.instance.CreateGoldEffect(m_iAmount);
        m_guiTopBar.SetIngaeGold(IngameManager.instance.ingameGold.earnedGold + PlayerData.instance.playerGold);
        Destroy(gameObject);
    }

    private void Start()
    {
        //Destroy(gameObject, m_fDuration);
        m_guiTopBar = IngameManager.instance.guiManager.gui_topBar;
        StartCoroutine(CreateTextEffectAndDestroySelf());
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, m_trsIngameGoldUI.position, Time.deltaTime * m_fSpeed);
    }
}
