using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 아이템 클래스
 */
public class Item
{
    public E_ITEMS m_Item;                     // 아이템
    public Sprite m_ItemSprite;                // 아이템 텍스처


    public Item(E_ITEMS p_item,
                Sprite p_itemSprite)
    {
        m_Item = p_item;
        m_ItemSprite = p_itemSprite;
    }
}