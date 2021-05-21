using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton_Mono<ItemManager> // 싱글톤 적용
{
    private List<EquipmentItem> m_EquipmentItemList = new List<EquipmentItem>(); // 아이템들 저장할 리스트
    private List<ConsumerItem> m_ConsumerItemList = new List<ConsumerItem>(); 

    // 리스트에 아이템 세팅
    private void Awake()
    {
        // 아이템 저장용 임시 변수
        string tempString;
        E_EQUIPMENT_ITEMS tempEquipEnum;
        
        for (int i = 0; i < (int)E_EQUIPMENT_ITEMS.Max; i++) // 장비 아이템 추가
        {
            tempEquipEnum = (E_EQUIPMENT_ITEMS)i;
            tempString = tempEquipEnum.ToString();
            m_EquipmentItemList.Add(new EquipmentItem(i + 1,                                            // 아이템 코드
                                                     (E_EQUIPMENT_ITEMS)i,                              // 아이템
                                                      E_ITEMTYPE.Equipment,                             // 아이템 타입
                                                      Resources.Load<Sprite>($"Item/{tempString}")));   // 아이템 텍스처
        }

        E_CONSUMER_ITEMS tempConsumeEnum;
        for (int i = 0; i < (int)E_CONSUMER_ITEMS.Max; i++) // 소비 아이템 추가
        {
            tempConsumeEnum = (E_CONSUMER_ITEMS)i;
            tempString = tempConsumeEnum.ToString();
            m_ConsumerItemList.Add(new ConsumerItem(i + 1,                                            // 아이템 코드
                                                   (E_CONSUMER_ITEMS)i,                               // 아이템
                                                    E_ITEMTYPE.Consumption,                           // 아이템 타입
                                                    Resources.Load<Sprite>($"Item/{tempString}")));   // 아이템 텍스처
        }
    }

    /*
     * 리스트에서 매개변수로 전달받은 정보와 같은 아이템 찾아서 반환 해주는 함수
     */
    public T GetItem<T>(string p_item, E_ITEMTYPE p_itemType) where T : class
    {
        T retItem; // 반환할 아이템

        switch(p_itemType) // 매개변수로 받은 아이템 타입으로 분류
        {
                // 장비 아이템
            case E_ITEMTYPE.Equipment:
                E_EQUIPMENT_ITEMS tempEquipEnum = (E_EQUIPMENT_ITEMS)Enum.Parse(typeof(E_EQUIPMENT_ITEMS), p_item); // string -> enum

                for (int i = 0; i < m_EquipmentItemList.Count; i++)
                {
                    if(m_EquipmentItemList[i].m_Item == tempEquipEnum) // 같은 아이템 찾기
                    {
                        retItem = m_EquipmentItemList[i] as T;         // 형 변환
                        return retItem;
                    }
                }
                break;

                // 소비 아이템
            case E_ITEMTYPE.Consumption:
                E_CONSUMER_ITEMS tempConsumeEnum = (E_CONSUMER_ITEMS)Enum.Parse(typeof(E_CONSUMER_ITEMS), p_item);  // string -> enum

                for (int i = 0; i < m_ConsumerItemList.Count; i++)
                {
                    if (m_ConsumerItemList[i].m_Item == tempConsumeEnum) // 같은 아이템 찾기
                    {
                        retItem = m_ConsumerItemList[i] as T;            // 형 변환
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
