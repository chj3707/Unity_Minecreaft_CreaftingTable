using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton_Mono<ItemManager> // 싱글톤 적용
{
    private List<Item> m_ItemList = new List<Item>(); // 아이템들 저장할 리스트

    // 리스트에 아이템 세팅
    private void Awake()
    {
        // 아이템 저장용 임시 변수
        string tempString;
        E_ITEMS tempEnum;
        
        for (int i = 0; i < (int)E_ITEMS.Max; i++) // 장비 아이템 추가
        {
            tempEnum = (E_ITEMS)i;
            tempString = tempEnum.ToString();

            m_ItemList.Add(new Item((E_ITEMS)i,                                        // 아이템 이름
                                     Resources.Load<Sprite>($"Item/{tempString}")));   // 아이템 텍스처
        }
    }

    /*
     * 리스트에서 매개변수로 전달받은 정보와 같은 아이템 찾아서 반환 해주는 함수
     */
    public T GetItem<T>(string p_item) where T : class
    {
        T retItem; // 반환할 아이템

        E_ITEMS tempEnum = (E_ITEMS)Enum.Parse(typeof(E_ITEMS), p_item); // string -> enum

        for (int i = 0; i < m_ItemList.Count; i++)
        {
            if (m_ItemList[i].m_Item == tempEnum)     // 같은 아이템 찾기
            {
                retItem = m_ItemList[i] as T;         // 형 변환
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
