using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
 * 제작대
 */

public class CreaftingTable : Singleton_Mono<CreaftingTable> // 싱글톤 적용
{
    protected CreaftingTable() { }

    private int m_SlotCount = 9; // 슬롯 개수

    private List<object> m_ItemList = new List<object>();                // 아이템 리스트
    private List<object[,]> m_ItemRecipeList = new List<object[,]>();    // 아이템 레시피

    
    private object[,] m_CreaftingTable;        // 제작대에 있는 아이템 확인용
    
    private int m_Row = 3; 
    private int m_Col = 3; 
    
    void Start()
    {
        SlotGenerator.GetInstance.SetSlot(m_SlotCount, this.transform);

        m_CreaftingTable = new object[m_Row, m_Col]; // 메모리 할당

        SetItemRecipe(); // 레시피 세팅
    }


    // 장비, 소비 아이템 제작 레시피 설정
    void SetItemRecipe()
    {
        m_ItemList.Add(ItemManager.GetInstance.GetItem<ConsumeItem>(("Stick"),E_ITEMTYPE.Consumption));           // 막대기 추가
        m_ItemList.Add(ItemManager.GetInstance.GetItem<EquipItem>(("WoodenAxe"),E_ITEMTYPE.Equipment));           // 나무 도끼 추가
        m_ItemList.Add(ItemManager.GetInstance.GetItem<EquipItem>(("FishingRod"), E_ITEMTYPE.Equipment));         // 낚싯대 추가
        m_ItemList.Add(ItemManager.GetInstance.GetItem<EquipItem>(("CarrotOnAStick"), E_ITEMTYPE.Equipment));     // 당근 낚싯대 추가

        // 아이템 이름 가져오는 용도
        string tempstr = "";
        E_EQUIPITEMS tempEquipEnum = E_EQUIPITEMS.None;
        E_CONSUMEITEMS tempConsumeEnum = E_CONSUMEITEMS.None;

        for (int i=0; i<m_ItemList.Count; i++)
        {
            if (m_ItemList[i].GetType() == typeof(EquipItem)) // 장비
            {
                tempstr = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(m_ItemList[i], tempEquipEnum).ToString(); // i번째 아이템 이름 가져오기 
            }
            if (m_ItemList[i].GetType() == typeof(ConsumeItem)) // 소비
            {
                tempstr = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(m_ItemList[i], tempConsumeEnum).ToString(); // i번째 아이템 이름 가져오기 
            }
            

            object[,] recipe = new object[m_Row,m_Col];
            // 막대기
            if (tempstr == "Stick") 
            {
                
                recipe[0, 0] = null;                 recipe[0, 1] = null;                      recipe[0, 2] = null;
                recipe[1, 0] = null;                 recipe[1, 1] = E_CONSUMEITEMS.Wood;       recipe[1, 2] = null;
                recipe[2, 0] = null;                 recipe[2, 1] = E_CONSUMEITEMS.Wood;       recipe[2, 2] = null;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // 나무도끼
            if (tempstr == "WoodenAxe") 
            {
                recipe[0, 0] = E_CONSUMEITEMS.Wood;  recipe[0, 1] = E_CONSUMEITEMS.Wood;       recipe[0, 2] = null;
                recipe[1, 0] = E_CONSUMEITEMS.Wood;  recipe[1, 1] = E_CONSUMEITEMS.Stick;      recipe[1, 2] = null;
                recipe[2, 0] = null;                 recipe[2, 1] = E_CONSUMEITEMS.Stick;      recipe[2, 2] = null;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // 낚싯대
            if (tempstr == "FishingRod") 
            {
                recipe[0, 0] = null;                 recipe[0, 1] = null;                      recipe[0, 2] = E_CONSUMEITEMS.Stick;
                recipe[1, 0] = null;                 recipe[1, 1] = E_CONSUMEITEMS.Stick;      recipe[1, 2] = E_CONSUMEITEMS.String;
                recipe[2, 0] = E_CONSUMEITEMS.Stick; recipe[2, 1] = null;                      recipe[2, 2] = E_CONSUMEITEMS.String;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // 당근 낚싯대
            if (tempstr == "CarrotOnAStick") 
            {
                recipe[0, 0] = null;                recipe[0, 1] = null;                       recipe[0, 2] = null;
                recipe[1, 0] = null;                recipe[1, 1] = E_EQUIPITEMS.FishingRod;    recipe[1, 2] = null;
                recipe[2, 0] = null;                recipe[2, 1] = null;                       recipe[2, 2] = E_CONSUMEITEMS.Carrot;
                m_ItemRecipeList.Add(recipe);
                continue;
            }
        }  
    }

    // 아이템 제작 후 테이블 초기화 용도
    public void TableReset()
    {
        // 테이블 채울 값
        E_EQUIPITEMS tempEquipEnum = E_EQUIPITEMS.None;
        E_CONSUMEITEMS tempConsumeEnum = E_CONSUMEITEMS.None;
        object[] tempArr = new object[transform.childCount]; // 1차원 배열로 가져와서 2차원 배열로 옮길것

        for (int i = 0; i < transform.childCount; i++)
        {
            object tempObj = transform.GetChild(i).GetComponent<Slot>().m_ItemInfo; // 제작대 슬롯 별로 접근
            
            // null 이면 저장하고 continue
            if (tempObj == null)
            {
                tempArr[i] = tempObj; // 제작대 n번째 슬롯 아이템
                continue;
            }
            // 장비 아이템
            else if (tempObj.GetType() == typeof(EquipItem))
            {
                tempObj = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(tempObj, tempEquipEnum);   // 해당 슬롯의 아이템값 저장
            }
            // 소비 아이템
            else
            {
                tempObj = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(tempObj, tempConsumeEnum); // 해당 슬롯의 아이템값 저장
            }

            Text tempText = transform.GetChild(i).GetComponentInChildren<Text>(); // 텍스트 접근을 위한 임시변수       
            tempText.text = (int.Parse(tempText.text) - 1).ToString();            // 아이템 개수 -1

            // 개수가 하나도 남지 않으면 해당 슬롯 초기화
            if (int.Parse(tempText.text) == 0)
            {
                Image tempImage = transform.GetChild(i).GetChild(0).GetComponent<Image>();  // 이미지 접근을 위한 임시변수

                tempObj = null;                                                             // 슬롯 정보 초기화
                tempImage.enabled = false;                                                  // 이미지 비활성화
                tempText.enabled = false;                                                   // 텍스트 비활성화
            }

            tempArr[i] = tempObj; // 제작대 n번째 슬롯 아이템
        }

        // 1차원 -> 2차원 옮기기
        for (int y = 0; y < m_Row; y++)
        {
            for (int x = 0; x < m_Col; x++)
            {
                m_CreaftingTable[y, x] = tempArr[(y * m_Row) + x];
            }
        }
    }

    // 테이블 정보 갱신
    public void TableInfoRenewal()
    {
        object[] tempArr = new object[transform.childCount]; // 1차원 배열로 가져와서 2차원 배열로 옮길것

        // 배열 채우는 용도
        E_EQUIPITEMS tempEquipEnum = E_EQUIPITEMS.None; 
        E_CONSUMEITEMS tempConsumeEnum = E_CONSUMEITEMS.None;

        for (int i = 0; i < transform.childCount; i++)
        {
            object tempObj = transform.GetChild(i).GetComponent<Slot>().m_ItemInfo; // 제작대 슬롯 별로 접근

            // null 이면 저장하고 continue
            if (tempObj == null)
            {
                tempArr[i] = tempObj; // 제작대 n번째 슬롯 아이템
                continue;
            }
            // 장비 아이템
            else if (tempObj.GetType() == typeof(EquipItem))
            {
                tempObj = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(tempObj, tempEquipEnum);   // 해당 슬롯의 아이템값 저장
            }
            // 소비 아이템
            else
            {
                tempObj = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(tempObj, tempConsumeEnum); // 해당 슬롯의 아이템값 저장
            }

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
    public object CompareTableToRecipe() 
    {
        bool endflag = false;
        int index = 0;
        foreach (var recipe in m_ItemRecipeList)
        {
            // 하나라도 다른게 있으면 종료
            for (int y = 0; y < m_Row; y++)
            {
                for (int x = 0; x < m_Col; x++)
                {
                    if (recipe[y, x] == null && m_CreaftingTable[y, x] != null) 
                    {
                        // 레시피는 빈칸인데 테이블에 값이 있는경우
                        ++index;
                        endflag = true;
                        break;
                    }
                    else if(recipe[y,x] != null && m_CreaftingTable[y,x] == null)
                    {
                        // 레시피는 빈칸이 아닌데 테이블에 값이 없는경우
                        ++index;
                        endflag = true;
                        break;
                    }

                    // 레시피와 테이블 둘다 빈칸이 아닐때 비교
                    if (recipe[y, x] != null && m_CreaftingTable[y, x] != null)
                    {
                        // 레시피와 다르면 플래그 켜서 종료
                        if (!recipe[y, x].Equals(m_CreaftingTable[y, x]))
                        {
                            ++index;
                            endflag = true;
                            break;
                        }
                    }
                }
                if (endflag)
                    break;
            }

            // 레시피와 일치
            if(!endflag)
            {
                return m_ItemList[index]; // 제작 아이템 리턴
            }

            endflag = false; // 플래그 초기화
        }
        return null;
    }

    void Update()
    {
        
    }
}
