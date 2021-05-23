using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ Ŭ����
 */

// ���
public class EquipItem
{
    public E_EQUIPITEMS m_Item;                // ������
    public E_ITEMTYPE m_ItemType;              // ������ Ÿ��
    public Sprite m_ItemSprite;                // ������ �ؽ�ó


    public EquipItem(E_EQUIPITEMS p_item,
                     E_ITEMTYPE p_itemType,
                     Sprite p_itemSprite)
    {
        m_Item = p_item;
        m_ItemType = p_itemType;
        m_ItemSprite = p_itemSprite;
    }
}

// �Һ�
public class ConsumeItem
{
    public E_CONSUMEITEMS m_Item;              // ������
    public E_ITEMTYPE m_ItemType;              // ������ Ÿ��
    public Sprite m_ItemSprite;                // ������ �ؽ�ó


    public ConsumeItem(E_CONSUMEITEMS p_item,
                       E_ITEMTYPE p_itemType,
                       Sprite p_itemSprite)
    {
        m_Item = p_item;
        m_ItemType = p_itemType;
        m_ItemSprite = p_itemSprite;
    }
}