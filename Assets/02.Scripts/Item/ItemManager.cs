using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton_Mono<ItemManager> // 싱글톤 적용
{
    private List<EquipItem> m_EquipItemList = new List<EquipItem>();       // 장비 아이템 리스트
    private List<ConsumeItem> m_ConsumeItemList = new List<ConsumeItem>(); // 소비 아이템 리스트

    // 리스트에 아이템 세팅
    private void Awake()
    {
        // 아이템 저장용 임시 변수
        string tempString;
        E_EQUIPITEMS tempEnum;
        
        for (int i = 0; i < (int)E_EQUIPITEMS.Max; i++) // 장비 아이템 추가
        {
            tempEnum = (E_EQUIPITEMS)i;
            tempString = tempEnum.ToString();

            m_EquipItemList.Add(new EquipItem((E_EQUIPITEMS)i,                                        // 아이템 이름
                                               E_ITEMTYPE.Equipment,                                  // 아이템 타입
                                               Resources.Load<Sprite>(tempString)));                  // 아이템 텍스처
        }

        E_CONSUMEITEMS ttempEnum;
        for (int i = 0; i < (int)E_CONSUMEITEMS.Max; i++) // 소비 아이템 추가
        {
            ttempEnum = (E_CONSUMEITEMS)i;
            tempString = ttempEnum.ToString();

            m_ConsumeItemList.Add(new ConsumeItem((E_CONSUMEITEMS)i,                                  // 아이템 이름
                                                   E_ITEMTYPE.Consumption,                            // 아이템 타입
                                                   Resources.Load<Sprite>(tempString)));              // 아이템 텍스처
        }
    }

    /*
     * 리스트에서 매개변수로 전달받은 정보와 같은 아이템 찾아서 반환 해주는 함수
     */
    public T GetItem<T>(string p_item, E_ITEMTYPE p_type) where T : class
    {
        T retItem; // 반환할 아이템
        
        switch(p_type)
        {
            case E_ITEMTYPE.Equipment:
                // 넘어온 아이템 이름 Enum.Parse 해서 Enum 형식으로 할당
                E_EQUIPITEMS tempEquipEnum = (E_EQUIPITEMS)Enum.Parse(typeof(E_EQUIPITEMS), p_item); // string -> enum

                for (int i = 0; i < m_EquipItemList.Count; i++)
                {
                    if (m_EquipItemList[i].m_Item == tempEquipEnum)     // 리스트에서 같은 아이템 찾기
                    {
                        retItem = m_EquipItemList[i] as T;              // 형 변환
                        return retItem;
                    }
                }
                break;
            case E_ITEMTYPE.Consumption:
                // 넘어온 아이템 이름 Enum.Parse 해서 Enum 형식으로 할당
                E_CONSUMEITEMS tempConsumeEnum = (E_CONSUMEITEMS)Enum.Parse(typeof(E_CONSUMEITEMS), p_item); // string -> enum

                for (int i = 0; i < m_ConsumeItemList.Count; i++)
                {
                    if (m_ConsumeItemList[i].m_Item == tempConsumeEnum)     // 리스트에서 같은 아이템 찾기
                    {
                        retItem = m_ConsumeItemList[i] as T;                // 형 변환
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
