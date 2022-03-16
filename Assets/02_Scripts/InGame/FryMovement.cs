using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryMovement : MonoBehaviour
{
    [SerializeField] FryData m_fryData;
    //Sprite m_sprite;
    SpriteRenderer m_spriteRender;
    public void SetOrderInLayer(int value) { m_spriteRender.sortingOrder = value; }

    [SerializeField] private float mSpeed = 5.0f;
    public float fSpeed { set { mSpeed = value; } }
    [SerializeField] private float m_fMaxPos = 1f;
    public float  fMaxPos { set { m_fMaxPos = value; } }
    float m_fMaxPosCoef = 1f;
    public float fMaxPosCoef { set { m_fMaxPosCoef = value; } }

    private Vector3 curPos;

    Rigidbody2D m_rb2D;
    CapsuleCollider2D m_col2D;

    Spawner m_spawner;
    StackCamera m_camera;
    Stack m_stack;

    [SerializeField] int m_fryID;
    public int fryID { get { return m_fryID; } }

    /////////////////////////////////////////////////////////////////// Æ¢±è »óÅÂ °ü¸® À¯ÇÑ»óÅÂ ¸Ó½Å
    public enum FRY_STATE
    {
        WAIT = 0,
        DROPPING,
        STACKED,
        FIXED,
        FALL
    }
    [SerializeField]
    FRY_STATE mCurFryState = FRY_STATE.WAIT;
    public bool IsFixed() { return mCurFryState == FRY_STATE.FIXED; }

    void UpdateState()
    {
        switch (mCurFryState)
        {
            case FRY_STATE.WAIT:
                FryMove();
                break;
            case FRY_STATE.DROPPING:
                if (DetectStacked())
                    SetState(FRY_STATE.STACKED);
                break;
            case FRY_STATE.STACKED:
                // ½ºÅÃ Èçµé¸² °¨Áö
                DetectRotation();
                break;
            case FRY_STATE.FIXED:
                break;
            case FRY_STATE.FALL:
                break;
        }
    }

    public void SetState(FRY_STATE command)
    {
        switch (command)
        {
            case FRY_STATE.WAIT:
                break;
            case FRY_STATE.DROPPING:
                m_rb2D.isKinematic = false;
                m_rb2D.mass = 5f;
                IngameManager.instance.chopsticks.SetState(Chopsticks.E_CHOPSTICK_STATE.DROP);
                if(SoundManager.instance)
                    SoundManager.instance.SFXAudioSourcePlay(SoundManager.E_SFX_TYPE.FRY_DROPPING);
                break;
            case FRY_STATE.STACKED:
                m_stack.StackFry(this);
                //m_spriteRender.sortingOrder = IngameManager.instance.iOderLayer++;
                if (SoundManager.instance)
                    SoundManager.instance.SFXAudioSourcePlay(SoundManager.E_SFX_TYPE.FRY_STACK);
                if (EffectManager.instance)
                    EffectManager.instance.CreateEffect(EffectManager.E_EFFECT_TYPE.DEBRIS, transform.position);
                m_spawner.SpawnFry();
                if (transform.position.y + 1 >= m_camera.transform.position.y)
                {
                    m_camera.fNewCameraPositionY = transform.position.y;
                    m_camera.bMoveCamera = true;
                }
                break;
            case FRY_STATE.FIXED:
                break;
            case FRY_STATE.FALL:
                continueFace = true;
                UpdateFryFace(E_FRY_FACE_TYPE.FALL);
                IngameManager.instance.SetState(IngameManager.E_INGAMESTATE.GAMEOVER);
                break;
        }
        mCurFryState = command;
    }

    /////////////////////////////////////////////////////////////////// Æ¢±è ÇÎÆþ µ¿ÀÛ
    void FryMove()
    {
        Vector3 tempV = curPos;
        tempV.x += m_fMaxPos * m_fMaxPosCoef * Mathf.Sin(Time.time * mSpeed);
        tempV.y = m_spawner.transform.position.y;
        transform.position = tempV;
    }

    /////////////////////////////////////////////////////////////////// ÇÃ·§Æû, Æ¢±è, ¹Ù´Ú¿¡ ´ê¾Ò´ÂÁö ÆÇÁ¤
    bool DetectStacked()
    {
        int nLayer;
        Collider2D[] colliders;

        nLayer = 1 << LayerMask.NameToLayer("Plate");
        colliders = Physics2D.OverlapCapsuleAll(transform.position, m_col2D.size * 1.2f, m_col2D.direction, 0.0f, nLayer);
        if (colliders.Length > 0)
            return true;

        colliders = null;
        nLayer = 1 << LayerMask.NameToLayer("Fry");
        colliders = Physics2D.OverlapCapsuleAll(transform.position, m_col2D.size * 1.2f, m_col2D.direction, 0.0f, nLayer);
        if (colliders.Length > 1)
            return true;

        return false;
    }

    /////////////////////////////////////////////////////////////////// Æ¢±è °­Ã¼ °íÁ¤
    public void FixFry()
    {
        m_rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        SetState(FRY_STATE.FIXED);
    }

    /////////////////////////////////////////////////////////////////// Æ¢±è Ç¥Á¤ º¯°æ
    public enum E_FRY_FACE_TYPE
    {
        NORMAL = 0,
        SMILE,
        TEASING,
        FALL
    }
    public void UpdateFryFace(E_FRY_FACE_TYPE face)
    {
        switch(face)
        {
            case E_FRY_FACE_TYPE.NORMAL:
                if(m_fryData.spriteFryImage)
                    m_spriteRender.sprite = m_fryData.spriteFryImage;
                //m_spriteRender.color = Color.white;
                break;
            case E_FRY_FACE_TYPE.SMILE:
                if(m_fryData.spriteFryImage_smile)
                    m_spriteRender.sprite = m_fryData.spriteFryImage_smile;
                //m_spriteRender.color = Color.green;
                break;
            case E_FRY_FACE_TYPE.TEASING:
                if(m_fryData.spriteFryImage_teasing)
                    m_spriteRender.sprite = m_fryData.spriteFryImage_teasing;
                //m_spriteRender.color = Color.yellow;
                break;
            case E_FRY_FACE_TYPE.FALL:
                if (m_fryData.spriteFryImage_fail)
                    m_spriteRender.sprite = m_fryData.spriteFryImage_fail;
                //m_spriteRender.color = Color.red;
                break;
        }
    }

    bool m_bIsFixedFall = false;
    public void FixFryFaceFall()
    {
        continueFace = true;
        m_bIsFixedFall = true;
        UpdateFryFace(E_FRY_FACE_TYPE.FALL);
    }

    /////////////////////////////////////////////////////////////////// Æ¢±è È¸ÀüÀ²
    public void DetectRotation()
    {
        //Debug.Log(zAngle);
        if (!continueFace)
        {
            float zAngle = Mathf.Abs(transform.rotation.eulerAngles.z - 180);
            if (zAngle >= 179)
                UpdateFryFace(E_FRY_FACE_TYPE.SMILE);
            else if(zAngle >= 170)
                StartCoroutine(ContinueFace(E_FRY_FACE_TYPE.TEASING));
            else
                StartCoroutine(ContinueFace(E_FRY_FACE_TYPE.FALL));
        }
    }

    bool continueFace = false;
    IEnumerator ContinueFace(E_FRY_FACE_TYPE face)
    {
        if(!continueFace)
        {
            continueFace = true;
            UpdateFryFace(face);
            yield return new WaitForSeconds(0.5f);
            if(m_bIsFixedFall)
            {
                UpdateFryFace(E_FRY_FACE_TYPE.FALL);
            }
            else
            {
                continueFace = false;
                UpdateFryFace(E_FRY_FACE_TYPE.NORMAL);
            }
        }
    }

    private void Awake()
    {
        m_rb2D = GetComponent<Rigidbody2D>();
        m_col2D = GetComponent<CapsuleCollider2D>();
        m_stack = transform.parent.GetComponent<Stack>();
        m_spriteRender = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        curPos = transform.position;
        m_spawner = IngameManager.instance.spawner;
        m_camera = IngameManager.instance.stackCamera;

        //mSpeed = IngameManager.instance.ingmaeInfo.fPingPongSpeed;
        m_fMaxPos = IngameManager.instance.ingmaeInfo.fPingPongMaxSize;
        GetComponent<CapsuleCollider2D>().size *= new Vector2(IngameManager.instance.ingmaeInfo.fFryColiderSize, 1);
        m_spriteRender.sprite = m_fryData.spriteFryImage;

        m_rb2D.mass = 5f;
    }

    void Update()
    {
        UpdateState();
    }
}
