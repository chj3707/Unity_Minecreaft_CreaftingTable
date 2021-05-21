using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton_Mono<ItemManager> // �̱��� ����
{
    private List<EquipmentItem> m_EquipmentItemList = new List<EquipmentItem>(); // �����۵� ������ ����Ʈ
    private List<ConsumerItem> m_ConsumerItemList = new List<ConsumerItem>(); 

    // ����Ʈ�� ������ ����
    private void Awake()
    {
        // ������ ����� �ӽ� ����
        string tempString;
        E_EQUIPMENT_ITEMS tempEquipEnum;
        
        for (int i = 0; i < (int)E_EQUIPMENT_ITEMS.Max; i++) // ��� ������ �߰�
        {
            tempEquipEnum = (E_EQUIPMENT_ITEMS)i;
            tempString = tempEquipEnum.ToString();
            m_EquipmentItemList.Add(new EquipmentItem(i + 1,                                            // ������ �ڵ�
                                                     (E_EQUIPMENT_ITEMS)i,                              // ������
                                                      E_ITEMTYPE.Equipment,                             // ������ Ÿ��
                                                      Resources.Load<Sprite>($"Item/{tempString}")));   // ������ �ؽ�ó
        }

        E_CONSUMER_ITEMS tempConsumeEnum;
        for (int i = 0; i < (int)E_CONSUMER_ITEMS.Max; i++) // �Һ� ������ �߰�
        {
            tempConsumeEnum = (E_CONSUMER_ITEMS)i;
            tempString = tempConsumeEnum.ToString();
            m_ConsumerItemList.Add(new ConsumerItem(i + 1,                                            // ������ �ڵ�
                                                   (E_CONSUMER_ITEMS)i,                               // ������
                                                    E_ITEMTYPE.Consumption,                           // ������ Ÿ��
                                                    Resources.Load<Sprite>($"Item/{tempString}")));   // ������ �ؽ�ó
        }
    }

    /*
     * ����Ʈ���� �Ű������� ���޹��� ������ ���� ������ ã�Ƽ� ��ȯ ���ִ� �Լ�
     */
    public T GetItem<T>(string p_item, E_ITEMTYPE p_itemType) where T : class
    {
        T retItem; // ��ȯ�� ������

        switch(p_itemType) // �Ű������� ���� ������ Ÿ������ �з�
        {
                // ��� ������
            case E_ITEMTYPE.Equipment:
                E_EQUIPMENT_ITEMS tempEquipEnum = (E_EQUIPMENT_ITEMS)Enum.Parse(typeof(E_EQUIPMENT_ITEMS), p_item); // string -> enum

                for (int i = 0; i < m_EquipmentItemList.Count; i++)
                {
                    if(m_EquipmentItemList[i].m_Item == tempEquipEnum) // ���� ������ ã��
                    {
                        retItem = m_EquipmentItemList[i] as T;         // �� ��ȯ
                        return retItem;
                    }
                }
                break;

                // �Һ� ������
            case E_ITEMTYPE.Consumption:
                E_CONSUMER_ITEMS tempConsumeEnum = (E_CONSUMER_ITEMS)Enum.Parse(typeof(E_CONSUMER_ITEMS), p_item);  // string -> enum

                for (int i = 0; i < m_ConsumerItemList.Count; i++)
                {
                    if (m_ConsumerItemList[i].m_Item == tempConsumeEnum) // ���� ������ ã��
                    {
                        retItem = m_ConsumerItemList[i] as T;            // �� ��ȯ
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
