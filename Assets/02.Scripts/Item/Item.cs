using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��� ������ Ŭ����
 */

public class EquipmentItem
{
    public int m_ItemCode;                     // ������ �ڵ�
    public E_EQUIPMENT_ITEMS m_Item;           // ������
    public E_ITEMTYPE m_ItemType;              // ������ Ÿ��
    public Sprite m_ItemSprite;                // ������ �ؽ�ó

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
 * �Һ� ������ Ŭ����
 */

public class ConsumerItem
{
    public int m_ItemCode;                    // ������ �ڵ�
    public E_CONSUMER_ITEMS m_Item;           // ������
    public E_ITEMTYPE m_ItemType;             // ������ Ÿ��
    public Sprite m_ItemSprite;               // ������ �ؽ�ó

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
