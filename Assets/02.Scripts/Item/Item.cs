using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ Ŭ����
 */
public class Item
{
    public E_ITEMS m_Item;                     // ������
    public Sprite m_ItemSprite;                // ������ �ؽ�ó


    public Item(E_ITEMS p_item,
                Sprite p_itemSprite)
    {
        m_Item = p_item;
        m_ItemSprite = p_itemSprite;
    }
}