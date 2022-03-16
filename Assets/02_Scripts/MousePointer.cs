using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField] Camera m_camera;
    public RectTransform targetRectTr;

    [SerializeField] float fCal;
    [SerializeField] float fX;
    [SerializeField] float fY;

    //[SerializeField] Transform m_trsEffectPos;
    [SerializeField] GameObject m_effect;

    bool m_bEffectContinue = false;

    IEnumerator Click()
    {
        m_bEffectContinue = true;
        m_effect.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_effect.SetActive(false);
        m_bEffectContinue = false;
    }

    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        //transform.position = Input.mousePosition + new Vector3(fX, fY * -1, 0);
        //transform.position = Input.mousePosition.normalized;

        Vector2 mousePos = Input.mousePosition;
        //mousePos = m_camera.ScreenToViewportPoint(mousePos) * fCal;
        mousePos = m_camera.ScreenToViewportPoint(mousePos);
        mousePos -= new Vector2(fX, fY);

        transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click effect");
            if (!m_bEffectContinue)
                StartCoroutine(Click());
            //EffectManager.instance.CreateEffect(EffectManager.E_EFFECT_TYPE.CLICK, m_trsEffectPos.position);
            //EffectManager.instance.CreateClickEffect(m_trsEffectPos);
        }
    }
}
