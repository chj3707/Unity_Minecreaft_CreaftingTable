using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int m_ItemCode;
    public E_ITEMTYPE m_ItemType;
    public Sprite m_ItemSprite;

    public Item(int p_itemCode,
                E_ITEMTYPE p_itemType,
                Sprite p_itemSprite)
    {
        m_ItemCode = p_itemCode;
        m_ItemType = p_itemType;
        m_ItemSprite = p_itemSprite;
    }
}
