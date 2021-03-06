using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/*
 * 소비 아이템 생성기
 */

public class ItemGenerator : Singleton_Mono<ItemGenerator> // 싱글톤 적용
{
    private object m_ItemInfo = null;   // 아이템

    public int m_MaxItemCount = 10;     // 아이템 가질 수 있는 최대 개수

    // 슬롯에 아이템 세팅해주는 함수 (좌측 상단부터 정렬해가며 생성됨)
    void SetSlotIntoItem(E_CONSUMEITEMS p_item)
    {
        Slot tempSlot;         // n번째 슬롯
        Image tempImage;       // n번째 슬롯의 Image
        Text tempText;         // n번째 슬롯의 Text
        int currItemCount;     // n번째 슬롯의 현재 아이템개수(Text 파싱)
        int tempVal;           // n번째 슬롯 최대개수 - 가지고 있는 아이템개수(부족한 개수)
        int getItemCount = 15; // 버튼 클릭으로 가져올 아이템 개수
        E_CONSUMEITEMS tempItem = E_CONSUMEITEMS.None;

        for (int i = 0; i < Inventroy.GetInstance.m_SlotCount; i++)
        {
            tempSlot = Inventroy.GetInstance.transform.GetChild(i).GetComponent<Slot>(); // n번째 슬롯 상태 확인용
            
            if (tempSlot.m_SlotState == E_SLOTSTATE.Full) // 아이템이 있으면
            {
                tempItem = Core.GetFiledInfoToReflectionValueType<E_CONSUMEITEMS>(tempSlot.m_ItemInfo, tempItem); // 슬롯의 아이템 정보 저장
                if (tempItem != p_item) // 다른 아이템이면 다음 슬롯으로 넘어감
                {
                    continue;
                }

                tempText = tempSlot.transform.GetComponentInChildren<Text>(); // 아이템이 있는 슬롯의 하위 Text컴포넌트에 접근
                currItemCount = int.Parse(tempText.text); // 아이템 개수 할당
                
                if(currItemCount >= m_MaxItemCount) // 최대 개수 이면 다음 슬롯으로 넘어감
                {
                    continue;
                }

                // 최대 개수가 아닐 때
                tempVal = m_MaxItemCount - currItemCount; // 최대개수 보다 부족한 개수
                
                if (tempVal >= getItemCount) // 부족한 개수가 가져올 아이템 개수보다 크면
                {
                    currItemCount += getItemCount; // 전부 다주고 다음 슬롯으로 넘어감
                    tempText.text = currItemCount.ToString(); // 변경 내용 저장
                    break;
                }

                // 부족한 개수가 가져올 아이템 개수보다 작으면
                getItemCount -= tempVal;
                currItemCount += tempVal;       // 개수 빼서 채워줌
                tempText.text = currItemCount.ToString(); // 변경 내용 저장
            }

            // 아이템이 없는 슬롯 일때의 처리
            else
            {
                if (getItemCount > 0)
                {
                    tempSlot.m_ItemInfo = m_ItemInfo;           // 슬롯에 아이템 정보 할당
                    tempSlot.m_SlotState = E_SLOTSTATE.Full;    // 아이템 있는 상태로 변경
                    tempImage = tempSlot.transform.GetChild(0).GetComponent<Image>(); // 아이템이 없는 슬롯의 하위 오브젝트 Image 컴포넌트에 접근
                    tempText = tempImage.transform.GetChild(0).GetComponent<Text>(); // 슬롯의 하위 오브젝트(이미지)의 하위 오브젝트 Text컴포넌트에 접근

                    currItemCount = int.Parse(tempText.text); // 현재 아이템 개수
                    tempVal = m_MaxItemCount - currItemCount; // 최대개수 보다 부족한 개수

                    // 부족한 개수가 가져올 아이템 개수보다 작으면 채워주고 다음 슬롯으로 넘어감
                    if (tempVal < getItemCount)
                    {
                        currItemCount += tempVal;       // 부족한 개수 채워주기
                        getItemCount -= tempVal;        // 채워준 개수만큼 빼기
                        tempImage.sprite = Core.GetFiledInfoToReflectionReferenceType<Sprite>(tempSlot.m_ItemInfo, tempImage.sprite); // Sprite 저장
                        tempText.text = currItemCount.ToString(); // 변경 내용 저장
                        tempSlot.UpdateSlotUI(); // 슬롯 UI 업데이트
                        continue;
                    }

                    tempImage.sprite = Core.GetFiledInfoToReflectionReferenceType<Sprite>(tempSlot.m_ItemInfo, tempImage.sprite); // Sprite 저장
                    tempText.text = getItemCount.ToString(); // 개수 저장
                    tempSlot.UpdateSlotUI(); // 슬롯 UI 업데이트
                    break;
                }
            }
        }
    }

    // 소비 아이템 정보 가져오기
    void GetConsumptionItem(string p_name, E_ITEMTYPE p_type)
    {
        if (EventManager.GetInstance.m_isDragging)
        {
            return;
        }
        m_ItemInfo = ItemManager.GetInstance.GetItem<ConsumeItem>(p_name, p_type); // 아이템 정보 빼오기

        E_CONSUMEITEMS tempEnum = E_CONSUMEITEMS.None;
        tempEnum = Core.GetFiledInfoToReflectionValueType(m_ItemInfo, tempEnum); // object에 저장된 아이템 정보 빼오기

        SetSlotIntoItem(tempEnum); // 슬롯에 아이템 세팅
    }

    // 나무 버튼
    public void _On_GetWoodBtn()
    {
        GetConsumptionItem("Wood", E_ITEMTYPE.Consumption);
    }

    // 실 버튼
    public void _On_GetStringBtn()
    {
        GetConsumptionItem("String", E_ITEMTYPE.Consumption);
    }

    // 당근 버튼
    public void _On_GetCarrotBtn()
    {
        GetConsumptionItem("Carrot", E_ITEMTYPE.Consumption);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
