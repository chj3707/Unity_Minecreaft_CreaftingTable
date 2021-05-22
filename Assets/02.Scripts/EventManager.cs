using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton_Mono<EventManager> // �̱��� ����
{
    public GameObject m_DraggingItem = null;      // ��� �ִ� ������ ������Ʈ
    public Item m_DraggingItemInfo = null;        // ��� �ִ� ������ ����

    public Slot m_BeginDraggingSlotInfo = null;   // �巡�� ������ ���� ����

    public bool m_isDragging = false;             // ������ ��� �ִ��� Ȯ�ο�

    void Start()
    {
        m_DraggingItem.SetActive(false);
    }
    
    void Update()
    {
        if (!m_isDragging) // ������ ��� ������ ���콺 ���������� ������Ʈ
        {
            return;
        }
        m_DraggingItem.transform.position = Input.mousePosition;
    }
}
