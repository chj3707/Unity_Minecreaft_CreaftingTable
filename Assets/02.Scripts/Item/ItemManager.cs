using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton_Mono<ItemManager> // �̱��� ����
{
    private List<EquipItem> m_EquipItemList = new List<EquipItem>();       // ��� ������ ����Ʈ
    private List<ConsumeItem> m_ConsumeItemList = new List<ConsumeItem>(); // �Һ� ������ ����Ʈ

    // ����Ʈ�� ������ ����
    private void Awake()
    {
        // ������ ����� �ӽ� ����
        string tempString;
        E_EQUIPITEMS tempEnum;
        
        for (int i = 0; i < (int)E_EQUIPITEMS.Max; i++) // ��� ������ �߰�
        {
            tempEnum = (E_EQUIPITEMS)i;
            tempString = tempEnum.ToString();

            m_EquipItemList.Add(new EquipItem((E_EQUIPITEMS)i,                                        // ������ �̸�
                                               E_ITEMTYPE.Equipment,                                  // ������ Ÿ��
                                               Resources.Load<Sprite>(tempString)));                  // ������ �ؽ�ó
        }

        E_CONSUMEITEMS ttempEnum;
        for (int i = 0; i < (int)E_CONSUMEITEMS.Max; i++) // �Һ� ������ �߰�
        {
            ttempEnum = (E_CONSUMEITEMS)i;
            tempString = ttempEnum.ToString();

            m_ConsumeItemList.Add(new ConsumeItem((E_CONSUMEITEMS)i,                                  // ������ �̸�
                                                   E_ITEMTYPE.Consumption,                            // ������ Ÿ��
                                                   Resources.Load<Sprite>(tempString)));              // ������ �ؽ�ó
        }
    }

    /*
     * ����Ʈ���� �Ű������� ���޹��� ������ ���� ������ ã�Ƽ� ��ȯ ���ִ� �Լ�
     */
    public T GetItem<T>(string p_item, E_ITEMTYPE p_type) where T : class
    {
        T retItem; // ��ȯ�� ������
        
        switch(p_type)
        {
            case E_ITEMTYPE.Equipment:
                // �Ѿ�� ������ �̸� Enum.Parse �ؼ� Enum �������� �Ҵ�
                E_EQUIPITEMS tempEquipEnum = (E_EQUIPITEMS)Enum.Parse(typeof(E_EQUIPITEMS), p_item); // string -> enum

                for (int i = 0; i < m_EquipItemList.Count; i++)
                {
                    if (m_EquipItemList[i].m_Item == tempEquipEnum)     // ����Ʈ���� ���� ������ ã��
                    {
                        retItem = m_EquipItemList[i] as T;              // �� ��ȯ
                        return retItem;
                    }
                }
                break;
            case E_ITEMTYPE.Consumption:
                // �Ѿ�� ������ �̸� Enum.Parse �ؼ� Enum �������� �Ҵ�
                E_CONSUMEITEMS tempConsumeEnum = (E_CONSUMEITEMS)Enum.Parse(typeof(E_CONSUMEITEMS), p_item); // string -> enum

                for (int i = 0; i < m_ConsumeItemList.Count; i++)
                {
                    if (m_ConsumeItemList[i].m_Item == tempConsumeEnum)     // ����Ʈ���� ���� ������ ã��
                    {
                        retItem = m_ConsumeItemList[i] as T;                // �� ��ȯ
                        return retItem;
                    }
                }
                break;
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
