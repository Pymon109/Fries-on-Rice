using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FryData_", menuName = "Scriptable Object/Fry Data", order = int.MaxValue)]
public class FryData : ScriptableObject
{
    public enum E_FRY_COMPENSATION_TYPE
    {
        NONE = 0,
        BUY,
        BUYPACKAGE,
        STACK,
        IAP
    }
    [SerializeField] int m_iFryID;
    public int iFryID { get { return m_iFryID; } }

    [SerializeField] string m_sNameID;
    public string sNameID { get { return m_sNameID; } }

    [SerializeField] string m_sCompensationID;
    public string sCompensationID { get { return m_sCompensationID; } }

    [SerializeField] E_FRY_COMPENSATION_TYPE m_eCompensation;
    public E_FRY_COMPENSATION_TYPE eCompensation {  get { return m_eCompensation; } }
    [SerializeField] int m_iCompensation_StackCount;
    public int iCompensation_StackCount { get { return m_iCompensation_StackCount; } }

    [SerializeField] int m_iPrice;
    public int iPrice {  get { return m_iPrice; } }

    [SerializeField] Sprite m_spriteFryImage;
    public Sprite spriteFryImage { get { return m_spriteFryImage; } }

    [SerializeField] Sprite m_spriteFryImage_smile;
    public Sprite spriteFryImage_smile { get { return m_spriteFryImage_smile; } }
    [SerializeField] Sprite m_spriteFryImage_teasing;
    public Sprite spriteFryImage_teasing { get { return m_spriteFryImage_teasing; } }

    [SerializeField] Sprite m_spriteFryImage_fail;
    public Sprite spriteFryImage_fail { get { return m_spriteFryImage_fail; } }

    [SerializeField] Sprite m_spriteFryImage_black;
    public Sprite spriteFryImage_black { get { return m_spriteFryImage_black; } }

    [SerializeField] Sprite m_spriteFryImage_dish;
    public Sprite spriteFryImage_dish { get { return m_spriteFryImage_dish; } }

    [SerializeField] Sprite m_spriteFryImage_dishBlack;
    public Sprite spriteFryImage_dishBlack { get { return m_spriteFryImage_dishBlack; } }

    [SerializeField] Sprite m_spriteFryCollectionPopUp_kor;
    public Sprite spriteFryCollectionPopUp_kor {  get { return m_spriteFryCollectionPopUp_kor; } }
    [SerializeField] Sprite m_spriteFryCollectionPopUp_eng;
    public Sprite spriteFryCollectionPopUp_eng { get { return m_spriteFryCollectionPopUp_eng; } }
}