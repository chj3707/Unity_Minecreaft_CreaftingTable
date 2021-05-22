using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton_Mono<ItemManager> // �̱��� ����
{
    private List<Item> m_ItemList = new List<Item>(); // �����۵� ������ ����Ʈ

    // ����Ʈ�� ������ ����
    private void Awake()
    {
        // ������ ����� �ӽ� ����
        string tempString;
        E_ITEMS tempEnum;
        
        for (int i = 0; i < (int)E_ITEMS.Max; i++) // ��� ������ �߰�
        {
            tempEnum = (E_ITEMS)i;
            tempString = tempEnum.ToString();

            m_ItemList.Add(new Item((E_ITEMS)i,                                        // ������ �̸�
                                     Resources.Load<Sprite>($"Item/{tempString}")));   // ������ �ؽ�ó
        }
    }

    /*
     * ����Ʈ���� �Ű������� ���޹��� ������ ���� ������ ã�Ƽ� ��ȯ ���ִ� �Լ�
     */
    public T GetItem<T>(string p_item) where T : class
    {
        T retItem; // ��ȯ�� ������

        E_ITEMS tempEnum = (E_ITEMS)Enum.Parse(typeof(E_ITEMS), p_item); // string -> enum

        for (int i = 0; i < m_ItemList.Count; i++)
        {
            if (m_ItemList[i].m_Item == tempEnum)     // ���� ������ ã��
            {
                retItem = m_ItemList[i] as T;         // �� ��ȯ
                return retItem;
            }
        }

        return null;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
