using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] GraphicRaycaster m_gr;
    [SerializeField] List<GUI_UISwitch> m_gGUIs;
    [SerializeField] Spawner m_spawner;

    bool m_bDropBlock = false;
    public bool bDropBlock { set { m_bDropBlock = value; } }

    public void DetectTouch()
    {
        if (Input.touchCount > 0)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.phase == TouchPhase.Began)
                {
                    var ped = new PointerEventData(null);
                    ped.position = touch.position;
                    List<RaycastResult> results = new List<RaycastResult>();
                    m_gr.Raycast(ped, results);

                    if (results.Count <= 0)
                    {
                        // gui 아닌 경우
                        if (m_spawner && !m_bDropBlock)
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                            RaycastHit hit;
                            Vector3 position = new Vector3();

                            m_spawner.Fry.SetState(FryMovement.FRY_STATE.DROPPING);
                        }
                    }
                    else
                    {
                        //Debug.Log("gui 감지");
                        for (int k = 0; k < results.Count; k++)
                            if (results[i].gameObject.GetComponent<Button>())
                            {
                                SoundManager.instance.SFXAudioSourcePlay(SoundManager.E_SFX_TYPE.UI);
                                break;
                            }

                        for (int j = 0; j < m_gGUIs.Count; j++)
                        {
                            //Debug.Log(results[0].gameObject.tag);
                            if (!results[0].gameObject.CompareTag(m_gGUIs[j].gameObject.tag))
                                m_gGUIs[j].ButtonOnHideUI();
                        }
                    }
                    break;
                }
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_gr.Raycast(ped, results);

            if (results.Count <= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Vector3 position = new Vector3();

                // gui 아닌 경우
                if (m_spawner && !m_bDropBlock)
                {
                    FryMovement fry = m_spawner.Fry;
                    if (fry)
                    {
                        fry.SetState(FryMovement.FRY_STATE.DROPPING);
                    }
                }
                //Debug.Log("Click");
            }
            else
            {
                //gui 터치
                //Debug.Log("gui 감지");
                for (int i = 0; i < results.Count; i++)
                    if (results[i].gameObject.GetComponent<Button>())
                    {
                        SoundManager.instance.SFXAudioSourcePlay(SoundManager.E_SFX_TYPE.UI);
                        break;
                    }

                for (int j = 0; j < m_gGUIs.Count; j++)
                {
                    //Debug.Log(results[0].gameObject.tag);
                    if (!results[0].gameObject.CompareTag(m_gGUIs[j].gameObject.tag))
                        m_gGUIs[j].ButtonOnHideUI();
                }
            }
        }
#endif
    }

    private void Awake()
    {
        if(!m_gr)
            m_gr = GetComponent<GraphicRaycaster>();
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            DetectTouch();
        }
    }
}
