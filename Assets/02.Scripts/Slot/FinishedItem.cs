using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * 완성품
 */

public class FinishedItem : Singleton_Mono<FinishedItem>, // 싱글톤 적용
    IPointerClickHandler   // 포인터 누르고 뗄 때 호출
{
    protected FinishedItem() { }

    private Item m_FinishedItem = null;          // 완성 아이템 정보
    private Image m_FinishedItemImage = null;    // 완성품 이미지
    public E_SLOTSTATE m_SlotState;             // 슬롯 상태

    // 완성품 확인
    public void FinishedCheak()
    {
        Item tempItem = CreaftingTable.GetInstance.CompareTableToRecipe(); 

        // 레시피와 맞지않으면 null로 받아옴
        if (tempItem == null)
        {
            ResetSlotUI();
            return;
        }
        
        m_FinishedItem = tempItem;                                  // 완성 아이템 정보 할당
        m_FinishedItemImage.sprite = m_FinishedItem.m_ItemSprite;   // 완성 아이템 이미지 적용
        m_SlotState = E_SLOTSTATE.Full;                             // 슬롯 상태 변경

        UpdateUI();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_SlotState == E_SLOTSTATE.Empty) // 빈 슬롯 리턴
            return;

        
        EventManager tempManager = EventManager.GetInstance;
        if (tempManager.m_isDragging)         // 아이템을 든 상태로 접근 불가
            return;

        tempManager.m_isDragging = true;
        tempManager.m_DraggingItem.SetActive(true);
        tempManager.m_DraggingItemInfo = m_FinishedItem; // 드래그 아이템 정보 할당


        ResetSlotUI();
    }
    public void ResetSlotUI()
    {
        m_FinishedItem = null;                  // 아이템 정보 초기화
        m_SlotState = E_SLOTSTATE.Empty;        // 아이템 슬롯 빈 상태로 설정

        UpdateUI();                             // UI 업데이트
    }
    public void UpdateUI()
    {
        // 슬롯 비었으면 비활성화
        m_FinishedItemImage.enabled = m_SlotState == E_SLOTSTATE.Empty ? false : true;
    }

    void Start()
    {
        m_FinishedItemImage = transform.GetChild(0).GetComponent<Image>();

        m_SlotState = E_SLOTSTATE.Empty;       // 빈상태로 시작
        m_FinishedItemImage.enabled = false;   // 이미지 비활성화
    }
    
    void Update()
    {
        
    }
}
