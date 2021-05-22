using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 * 제작대
 */

public class CreaftingTable : Singleton_Mono<CreaftingTable> // 싱글톤 적용
{
    protected CreaftingTable() { }

    private int m_SlotCount = 9; // 슬롯 개수

    private List<object> m_ItemList = new List<object>();                // 아이템 리스트
    private List<E_ITEMS[,]> m_ItemRecipeList = new List<E_ITEMS[,]>();  // 아이템 레시피

    
    private E_ITEMS[,] m_CreaftingTable;        // 제작대에 있는 아이템 확인용
    
    private int m_Row = 3; 
    private int m_Col = 3; 
    
    void Start()
    {
        SlotGenerator.GetInstance.SetSlot(m_SlotCount, this.transform);

        m_CreaftingTable = new E_ITEMS[m_Row, m_Col]; // 메모리 할당

        SetItemRecipe(); // 레시피 세팅
    }


    // 장비, 소비 아이템 제작 레시피 설정
    void SetItemRecipe()
    {
        m_ItemList.Add(ItemManager.GetInstance.GetItem<Item>("Stick"));              // 막대기 추가
        m_ItemList.Add(ItemManager.GetInstance.GetItem<Item>("WoodenAxe"));          // 나무 도끼 추가
        m_ItemList.Add(ItemManager.GetInstance.GetItem<Item>("FishingRod"));         // 낚싯대 추가
        m_ItemList.Add(ItemManager.GetInstance.GetItem<Item>("CarrotOnAStick"));     // 당근 낚싯대 추가

        // 아이템 이름 가져오는 용도
        E_ITEMS tempItem = E_ITEMS.None;
        string tempstr = "";
        
        for (int i=0; i<m_ItemList.Count; i++)
        {
            tempstr = ItemGenerator.GetInstance.
                GetFiledInfoToReflectionString(m_ItemList[i], tempItem); // i번째 아이템 이름 가져오기 

            E_ITEMS[,] recipe = new E_ITEMS[m_Row,m_Col];
            // 막대기
            if (tempstr == "Stick") 
            {
                
                recipe[0, 0] = E_ITEMS.None;  recipe[0, 1] = E_ITEMS.None;       recipe[0, 2] = E_ITEMS.None;
                recipe[1, 0] = E_ITEMS.None;  recipe[1, 1] = E_ITEMS.Wood;       recipe[1, 2] = E_ITEMS.None;
                recipe[2, 0] = E_ITEMS.None;  recipe[2, 1] = E_ITEMS.Wood;       recipe[2, 2] = E_ITEMS.None;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // 나무도끼
            if (tempstr == "WoodenAxe") 
            {
                recipe[0, 0] = E_ITEMS.Wood;  recipe[0, 1] = E_ITEMS.Wood;       recipe[0, 2] = E_ITEMS.None;
                recipe[1, 0] = E_ITEMS.Wood;  recipe[1, 1] = E_ITEMS.Stick;      recipe[1, 2] = E_ITEMS.None;
                recipe[2, 0] = E_ITEMS.None;  recipe[2, 1] = E_ITEMS.Stick;      recipe[2, 2] = E_ITEMS.None;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // 낚싯대
            if (tempstr == "FishingRod") 
            {
                recipe[0, 0] = E_ITEMS.None;  recipe[0, 1] = E_ITEMS.None;       recipe[0, 2] = E_ITEMS.Stick;
                recipe[1, 0] = E_ITEMS.None;  recipe[1, 1] = E_ITEMS.Stick;      recipe[1, 2] = E_ITEMS.String;
                recipe[2, 0] = E_ITEMS.Stick; recipe[2, 1] = E_ITEMS.None;       recipe[2, 2] = E_ITEMS.String;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // 당근 낚싯대
            if (tempstr == "CarrotOnAStick") 
            {
                recipe[0, 0] = E_ITEMS.None;  recipe[0, 1] = E_ITEMS.None;       recipe[0, 2] = E_ITEMS.None;
                recipe[1, 0] = E_ITEMS.None;  recipe[1, 1] = E_ITEMS.FishingRod; recipe[1, 2] = E_ITEMS.None;
                recipe[2, 0] = E_ITEMS.None;  recipe[2, 1] = E_ITEMS.None;       recipe[2, 2] = E_ITEMS.Carrot;
                m_ItemRecipeList.Add(recipe);
                continue;
            }
        }  
    }

    // 테이블 정보 갱신
    public void TableInfoRenewal()
    {
        E_ITEMS[] tempArr = new E_ITEMS[transform.childCount]; // 1차원 배열로 가져와서 2차원 배열로 옮길것
        for (int i = 0; i < transform.childCount; i++)
        {
            E_ITEMS tempObj = transform.GetChild(i).GetComponent<Slot>().m_ItemInfo.m_Item; // 제작대 슬롯 별로 접근
            
            tempArr[i] = tempObj; // 제작대 n번째 슬롯 아이템
        }

        // 1차원 -> 2차원 옮기기
        for (int y = 0; y < m_Row; y++)
        {
            for (int x = 0; x < m_Col; x++)
            {
                m_CreaftingTable[y, x] = tempArr[(y * m_Row) + x];
                //Debug.Log(m_CreaftingTable[y, x]);
            }
        }

        FinishedItem.GetInstance.FinishedCheak(); // 완성 체크
    }

    // 제작대와 등록해둔 레시피 비교(제작대에 아이템 올릴때마다)
    public Item CompareTableToRecipe()
    {
        bool endflag = false;
        int index = 0;
        foreach (var item in m_ItemRecipeList)
        {
            // 하나라도 다른게 있으면 종료
            for (int y = 0; y < m_Row; y++)
            {
                for (int x = 0; x < m_Col; x++)
                {
                    if (item[y, x] != m_CreaftingTable[y, x])
                    {
                        ++index;
                        endflag = true;
                        break;
                    }
                }
                if (endflag)
                    break;
            }

            // 레시피와 일치
            if(!endflag)
            {
                return m_ItemList[index] as Item; // 제작 아이템 리턴
            }
               
        }
        return null;
    }

    void Update()
    {
        
    }
}
