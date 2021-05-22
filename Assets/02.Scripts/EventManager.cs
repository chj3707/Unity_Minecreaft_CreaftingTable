using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton_Mono<EventManager> // 싱글톤 적용
{
    public GameObject m_DraggingItem = null;      // 들고 있는 아이템 오브젝트
    public Item m_DraggingItemInfo = null;        // 들고 있는 아이템 정보

    public Slot m_BeginDraggingSlotInfo = null;   // 드래그 시작한 슬롯 정보

    public bool m_isDragging = false;             // 아이템 들고 있는지 확인용

    void Start()
    {
        m_DraggingItem.SetActive(false);
    }
    
    void Update()
    {
        if (!m_isDragging) // 아이템 들고 있으면 마우스 포지션으로 업데이트
        {
            return;
        }
        m_DraggingItem.transform.position = Input.mousePosition;
    }
}
