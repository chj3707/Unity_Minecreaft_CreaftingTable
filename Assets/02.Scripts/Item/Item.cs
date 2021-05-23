using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 아이템 클래스
 */

// 장비
public class EquipItem
{
    public E_EQUIPITEMS m_Item;                // 아이템
    public E_ITEMTYPE m_ItemType;              // 아이템 타입
    public Sprite m_ItemSprite;                // 아이템 텍스처


    public EquipItem(E_EQUIPITEMS p_item,
                     E_ITEMTYPE p_itemType,
                     Sprite p_itemSprite)
    {
        m_Item = p_item;
        m_ItemType = p_itemType;
        m_ItemSprite = p_itemSprite;
    }
}

// 소비
public class ConsumeItem
{
    public E_CONSUMEITEMS m_Item;              // 아이템
    public E_ITEMTYPE m_ItemType;              // 아이템 타입
    public Sprite m_ItemSprite;                // 아이템 텍스처


    public ConsumeItem(E_CONSUMEITEMS p_item,
                       E_ITEMTYPE p_itemType,
                       Sprite p_itemSprite)
    {
        m_Item = p_item;
        m_ItemType = p_itemType;
        m_ItemSprite = p_itemSprite;
    }
}