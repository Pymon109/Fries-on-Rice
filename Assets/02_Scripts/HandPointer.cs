using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandPointer : MonoBehaviour
{
    [SerializeField] RectTransform m_trsCursor;
    [SerializeField] GameObject m_effect;

    [SerializeField] float fX;
    [SerializeField] float fY;

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
        Init_Cursor();
    }
    private void Update()
    {
        Update_MousePosition();
    }

    private void Init_Cursor()
    {
        Cursor.visible = false;
        //m_trsCursor.pivot = Vector2.up;

        if (m_trsCursor.GetComponent<Graphic>())
            m_trsCursor.GetComponent<Graphic>().raycastTarget = false;
    }

    //CodeFinder 코드파인더
    //From https://codefinder.janndk.com/ 
    private void Update_MousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos -= new Vector3(fX, fY, -100f);

        m_trsCursor.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click effect");
            if (!m_bEffectContinue)
                StartCoroutine(Click());
        }
    }
}
