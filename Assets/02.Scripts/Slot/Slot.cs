using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,
    IPointerClickHandler   // 포인터 누르고 뗄 때 호출
{
    public Item m_ItemInfo = null;          // 아이템 정보
    private Image m_SlotItemImage = null;   // 아이템 이미지
    private Text m_SlotItemCount = null;    // 아이템 개수

    public E_SLOTSTATE m_SlotState;         // 슬롯 상태

    private void Awake()
    {
        /* 슬롯 하위 오브젝트 Image,Text 컴포넌트에 접근 */
        m_SlotItemImage = transform.GetChild(0).GetComponent<Image>();
        m_SlotItemCount = m_SlotItemImage.transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {
        m_SlotState = E_SLOTSTATE.Empty;
        m_ItemInfo = new Item(E_ITEMS.None, null);

        UpdateSlotUI();
    }

    // 포인터 누르고 뗄 때 호출 (좌,우 클릭 구분해서 구현)
    // https://docs.unity3d.com/kr/530/ScriptReference/EventSystems.PointerEventData.html PointerEventData
    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager tempManager = EventManager.GetInstance;
        Text tempText;
        
        // 아이템을 들고 있는 경우
        if (tempManager.m_isDragging)
        {
            switch(m_SlotState) 
            {
                // 아이템 없는 슬롯(드랍)
                case E_SLOTSTATE.Empty:
                    switch (eventData.pointerId)
                    {
                        // 마우스 좌클릭 (전체)
                        case -1:
                            ItemDrop(tempManager, eventData);
                            break;

                        // 마우스 우클릭 (1개씩)
                        case -2:
                            ItemDrop(tempManager, eventData);
                            break;
                    }
                    
                    break;

                // 아이템 있는 슬롯(아이템 겹치기, 스왑) 
                case E_SLOTSTATE.Full:
                    int currItemCount = int.Parse(m_SlotItemCount.text); // 현재 슬롯에 있는 아이템 개수
                    
                    if (currItemCount == ItemGenerator.GetInstance.m_MaxItemCount || 
                        this.m_ItemInfo != tempManager.m_DraggingItemInfo) // 슬롯 아이템 개수가 가득 찬 경우(스왑) 이거나 서로 다른 아이템 일 경우
                    {
                        if (eventData.pointerId == -2) // 우클릭 무시
                            return;
                        tempManager.m_isDragging = false;            // 드래그 해제

                        // 드래그 시작 슬롯에 현재 슬롯 정보 할당
                        tempManager.m_BeginDraggingSlotInfo.m_ItemInfo = this.m_ItemInfo;                           // 아이템 정보
                        tempManager.m_BeginDraggingSlotInfo.m_SlotItemImage.sprite = this.m_SlotItemImage.sprite;   // 아이템 이미지
                        tempManager.m_BeginDraggingSlotInfo.m_SlotItemCount.text = this.m_SlotItemCount.text;       // 아이템 개수
                        tempManager.m_BeginDraggingSlotInfo.m_SlotState = E_SLOTSTATE.Full;                         // 슬롯 상태 변경
                        tempManager.m_BeginDraggingSlotInfo.UpdateSlotUI();                                         // UI 업데이트

                        // 현재 슬롯에 드래그 되어 있는 정보 할당
                        m_ItemInfo = tempManager.m_DraggingItemInfo;                                                // 아이템 정보 가져오기
                        m_SlotItemImage.sprite = tempManager.m_DraggingItem.GetComponent<Image>().sprite;           // 들고 있던 아이템 이미지에 접근
                        m_SlotItemCount.text = tempManager.m_DraggingItem.GetComponentInChildren<Text>().text;      // 들고 있던 아이템 텍스트에 접근
                        UpdateSlotUI();                                                                             // UI 업데이트

                        tempManager.m_DraggingItem.SetActive(false); // 드래그 아이템 오브젝트 비활성화
                    }
                    else    // 슬롯 아이템 개수가 가득 차지 않은 경우(아이템 겹치기) :: 같은 아이템
                    {
                        switch(eventData.pointerId)
                        {
                            // 좌클릭 (통째로 합침)
                            case -1:
                                tempText = tempManager.m_DraggingItem.GetComponentInChildren<Text>();                   // 드래그 중인 아이템 텍스트에 접근
                                int itemCount = int.Parse(this.m_SlotItemCount.text);                                   // 현재 아이템 개수
                                int dragItemCount = int.Parse(tempText.text);                                           // 드래그 중인 아이템 개수

                                // 가지고 있던 아이템 + 드래그로 가져온 아이템이 최대 개수를 넘으면
                                if (itemCount + dragItemCount > ItemGenerator.GetInstance.m_MaxItemCount)
                                {
                                    int totalCount = itemCount + dragItemCount;
                                    m_SlotItemCount.text = ItemGenerator.GetInstance.m_MaxItemCount.ToString();         // 최대 개수로 설정
                                    tempText.text = (totalCount - ItemGenerator.GetInstance.m_MaxItemCount).ToString(); // 드래그 중인 아이템 개수 총개수-최대개수
                                    UpdateSlotUI();                                                                     // UI 업데이트
                                    return;
                                }

                                // 최대 개수보다 작으면
                                m_SlotItemCount.text = (itemCount + dragItemCount).ToString();   // 아이템 겹치기
                                tempManager.m_isDragging = false;                                // 드래그 해제
                                tempManager.m_DraggingItem.SetActive(false);                     // 드래그 아이템 오브젝트 비활성화
                                UpdateSlotUI();                                                  // UI 업데이트
                                break;

                            // 우클릭 (하나씩 합침)
                            case -2:
                                ItemDrop(tempManager, eventData);

                                break;

                        }
                        
                    }
                    break;
            }
        }

        else // 아이템을 들고 있지 않은 경우 (아이템 들기)
        {
            if (m_SlotState == E_SLOTSTATE.Empty) // 빈 슬롯 리턴
                return;

            switch (eventData.pointerId)
            {
                // 마우스 좌클릭 :: 전체 다 들기
                case -1:
                    ItemHold(tempManager, eventData);
                    break;

                // 마우스 우클릭 :: 절반(올림) 들기
                case -2:
                    ItemHold(tempManager, eventData);  
                    break;
            }
        }
        
        if (transform.parent.CompareTag("CreaftingTable")) // 제작대에 접근하면 테이블 정보 갱신
        {
            CreaftingTable.GetInstance.TableInfoRenewal();
        }
    }

    // 아이템 들기 (좌클릭, 우클릭 구분)
    void ItemHold(EventManager p_eventManager, PointerEventData p_data)
    {
        p_eventManager.m_isDragging = true;
        p_eventManager.m_DraggingItem.SetActive(true);
        p_eventManager.m_BeginDraggingSlotInfo = this;  // 드래그 시작한 슬롯 정보 저장
        p_eventManager.m_DraggingItemInfo = m_ItemInfo; // 아이템 정보 가져오기

        Image tempImage = p_eventManager.m_DraggingItem.GetComponent<Image>();           // 들고 있는 아이템 이미지에 접근
        Text tempText = p_eventManager.m_DraggingItem.GetComponentInChildren<Text>();    // 들고 있는 아이템 텍스트에 접근

        switch(p_data.pointerId)
        {
            // 좌클릭
            case -1:
                tempImage.sprite = m_SlotItemImage.sprite;  // 아이템 이미지
                tempText.text = m_SlotItemCount.text;       // 아이템 개수

                ReSetSlotUI();  // 해당 슬롯 초기화
                break;

            // 우클릭
            case -2:
                int curritemCount = int.Parse(m_SlotItemCount.text);        // 현재 아이템 개수

                if (curritemCount % 2 == 0) // 짝수
                {
                    tempImage.sprite = m_SlotItemImage.sprite;              // 드래그 아이템 이미지
                    tempText.text = (curritemCount * 0.5).ToString();       // 드래그 아이템 개수
                    m_SlotItemCount.text = (curritemCount * 0.5).ToString();// 남은 아이템 개수 갱신
                }

                else // 홀수
                {
                    int roundInt = Mathf.CeilToInt(curritemCount * 0.5f);           // 올림
                    tempImage.sprite = m_SlotItemImage.sprite;                      // 드래그 아이템 이미지
                    tempText.text = roundInt.ToString();                            // 드래그 아이템 개수
                    m_SlotItemCount.text = (curritemCount - roundInt).ToString();   // 남은 아이템 개수 갱신

                    // 아이템이 하나도 남지 않으면 초기화
                    if (m_SlotItemCount.text == "0")
                        ReSetSlotUI();
                }
                break;
        }
    }

    // 아이템 놓기 (좌클릭, 우클릭 구분)
    void ItemDrop(EventManager p_eventManager, PointerEventData p_data)
    {
        int DropItemCount = 1;

        m_ItemInfo = p_eventManager.m_DraggingItemInfo;   // 아이템 정보 가져오기
        Image tempImage = p_eventManager.m_DraggingItem.GetComponent<Image>();          // 들고 있던 아이템 이미지에 접근
        Text tempText = p_eventManager.m_DraggingItem.GetComponentInChildren<Text>();   // 들고 있던 아이템 텍스트에 접근

        switch(p_data.pointerId)
        {
            // 좌클릭
            case -1:
                p_eventManager.m_isDragging = false;            // 드래그 해제
                m_SlotItemImage.sprite = tempImage.sprite;      // 슬롯 아이템 이미지 할당
                m_SlotItemCount.text = tempText.text;           // 슬롯 아이템 개수 할당

                p_eventManager.m_DraggingItem.SetActive(false); // 드래그 아이템 오브젝트 비활성화
                
                break;
            
            // 우클릭
            case -2:
                if (int.Parse(tempText.text) - DropItemCount == 0)    // 드래그 중인 아이템 개수가 -1개(드래그 종료)
                {
                    p_eventManager.m_isDragging = false;              // 드래그 해제
                    p_eventManager.m_DraggingItem.SetActive(false);   // 드래그 아이템 오브젝트 비활성화
                }

                m_SlotItemImage.sprite = tempImage.sprite;                                           // 슬롯 아이템 이미지 할당
                m_SlotItemCount.text = (int.Parse(m_SlotItemCount.text) + DropItemCount).ToString(); // 슬롯 아이템 개수 할당
                tempText.text = (int.Parse(tempText.text) - DropItemCount).ToString();               // 드래그 중인 아이템 개수 -1

                
                break;
        }

        m_SlotState = E_SLOTSTATE.Full; // 슬롯 상태 변경
        UpdateSlotUI(); // UI 업데이트
    }

    // 슬롯 초기화
    public void ReSetSlotUI()
    {
        m_ItemInfo = null;                  // 아이템 정보 초기화
        m_SlotState = E_SLOTSTATE.Empty;    // 아이템 슬롯 빈 상태로 설정
        m_SlotItemCount.text = "0";         // 아이템 텍스트 초기화

        m_ItemInfo = new Item(E_ITEMS.None, null);

        UpdateSlotUI();                     // UI 업데이트
    }

    public void UpdateSlotUI()
    {
        /* 슬롯이 빈 상태면 비활성화 */
        m_SlotItemImage.enabled = m_SlotState == E_SLOTSTATE.Full ? true : false;
        m_SlotItemCount.enabled = m_SlotState == E_SLOTSTATE.Full ? true : false;
    }


    void Update()
    {
        
    }
}
