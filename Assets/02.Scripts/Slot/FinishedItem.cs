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

    private object m_FinishedItem = null;       // 완성 아이템 정보
    private Image m_FinishedItemImage = null;   // 완성품 이미지
    private Text m_FinishedItemText = null;     // 완성품 개수
    public E_SLOTSTATE m_SlotState;             // 슬롯 상태

    // 완성품 확인
    public void FinishedCheak()
    {
        object tempItem = CreaftingTable.GetInstance.CompareTableToRecipe(); 

        // 레시피와 맞지않으면 null로 받아옴
        if (tempItem == null)
        {
            ResetSlotUI();
            return;
        }
        
        m_FinishedItem = tempItem;                                  // 완성 아이템 정보 할당


        m_FinishedItemImage.sprite = 
            Core.GetFiledInfoToReflectionReferenceType
            (m_FinishedItem, m_FinishedItemImage.sprite);           // 완성 아이템 이미지 적용

        m_FinishedItemText.text = "1";                              // 완성 아이템 개수 적용

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

        tempManager.m_isDragging = true;                 // 드래그 시작
        tempManager.m_DraggingItem.SetActive(true);
        tempManager.m_BeginDraggingSlotInfo = null;      // 드래그 시작한 슬롯 정보 저장
        tempManager.m_DraggingItemInfo = m_FinishedItem; // 드래그 아이템 정보 할당

        Image tempImage = tempManager.m_DraggingItem.GetComponent<Image>();           // 드래그 아이템 이미지에 접근
        Text tempText = tempManager.m_DraggingItem.GetComponentInChildren<Text>();    // 드래그 아이템 텍스트에 접근    

        E_ITEMTYPE tempItemType = E_ITEMTYPE.None;                                    // 아이템 타입 확인용 임시 변수
        tempItemType = Core.GetFiledInfoToReflectionValueType
            (m_FinishedItem, tempItemType);                                           // 완성품의 아이템 타입 가져오기

        tempImage.sprite = m_FinishedItemImage.sprite;                                // 드래그 아이템 이미지 설정

        // 장비
        if (tempItemType == E_ITEMTYPE.Equipment)                                     // 드래그 아이템 개수 설정
        {
            tempText.text = "1";
            tempText.enabled = false; // 장비 아이템 개수 비활성화
        }
        // 소비
        else
            tempText.text = (int.Parse(m_FinishedItemText.text)).ToString();
     
        ResetSlotUI();                                  // 완성품 슬롯 초기화
        CreaftingTable.GetInstance.TableReset();        // 제작 테이블 재설정
    }

    public void ResetSlotUI()
    {
        m_FinishedItem = null;                  // 아이템 정보 초기화
        m_SlotState = E_SLOTSTATE.Empty;        // 아이템 슬롯 빈 상태로 설정
        m_FinishedItemText.text = "0";          // 아이템 개수 초기화

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
        m_FinishedItemText = m_FinishedItemImage.GetComponentInChildren<Text>();

        m_SlotState = E_SLOTSTATE.Empty;       // 빈상태로 시작
        m_FinishedItemImage.enabled = false;   // 이미지 비활성화
        m_FinishedItemText.enabled = false;    // 텍스트 비활성화
    }
    
    void Update()
    {
        
    }
}
