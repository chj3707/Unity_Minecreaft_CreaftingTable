using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 장비 아이템 클래스
 */

public class EquipmentItem
{
    public int m_ItemCode;                     // 아이템 코드
    public E_EQUIPMENT_ITEMS m_Item;           // 아이템
    public E_ITEMTYPE m_ItemType;              // 아이템 타입
    public Sprite m_ItemSprite;                // 아이템 텍스처

    public EquipmentItem(int p_itemCode,
                E_EQUIPMENT_ITEMS p_item,
                E_ITEMTYPE p_itemType,
                Sprite p_itemSprite)
    {
        m_ItemCode = p_itemCode;
        m_Item = p_item;
        m_ItemType = p_itemType;
        m_ItemSprite = p_itemSprite;
    }
}

/*
 * 소비 아이템 클래스
 */

public class ConsumerItem
{
    public int m_ItemCode;                    // 아이템 코드
    public E_CONSUMER_ITEMS m_Item;           // 아이템
    public E_ITEMTYPE m_ItemType;             // 아이템 타입
    public Sprite m_ItemSprite;               // 아이템 텍스처

    public ConsumerItem(int p_itemCode,
                E_CONSUMER_ITEMS p_item,
                E_ITEMTYPE p_itemType,
                Sprite p_itemSprite)
    {
        m_ItemCode = p_itemCode;
        m_Item = p_item;
        m_ItemType = p_itemType;
        m_ItemSprite = p_itemSprite;
    }
}
