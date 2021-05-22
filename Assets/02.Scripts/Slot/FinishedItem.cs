using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * �ϼ�ǰ
 */

public class FinishedItem : Singleton_Mono<FinishedItem>, // �̱��� ����
    IPointerClickHandler   // ������ ������ �� �� ȣ��
{
    protected FinishedItem() { }

    private Item m_FinishedItem = null;          // �ϼ� ������ ����
    private Image m_FinishedItemImage = null;    // �ϼ�ǰ �̹���
    public E_SLOTSTATE m_SlotState;             // ���� ����

    // �ϼ�ǰ Ȯ��
    public void FinishedCheak()
    {
        Item tempItem = CreaftingTable.GetInstance.CompareTableToRecipe(); 

        // �����ǿ� ���������� null�� �޾ƿ�
        if (tempItem == null)
        {
            ResetSlotUI();
            return;
        }
        
        m_FinishedItem = tempItem;                                  // �ϼ� ������ ���� �Ҵ�
        m_FinishedItemImage.sprite = m_FinishedItem.m_ItemSprite;   // �ϼ� ������ �̹��� ����
        m_SlotState = E_SLOTSTATE.Full;                             // ���� ���� ����

        UpdateUI();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_SlotState == E_SLOTSTATE.Empty) // �� ���� ����
            return;

        
        EventManager tempManager = EventManager.GetInstance;
        if (tempManager.m_isDragging)         // �������� �� ���·� ���� �Ұ�
            return;

        tempManager.m_isDragging = true;
        tempManager.m_DraggingItem.SetActive(true);
        tempManager.m_DraggingItemInfo = m_FinishedItem; // �巡�� ������ ���� �Ҵ�


        ResetSlotUI();
    }
    public void ResetSlotUI()
    {
        m_FinishedItem = null;                  // ������ ���� �ʱ�ȭ
        m_SlotState = E_SLOTSTATE.Empty;        // ������ ���� �� ���·� ����

        UpdateUI();                             // UI ������Ʈ
    }
    public void UpdateUI()
    {
        // ���� ������� ��Ȱ��ȭ
        m_FinishedItemImage.enabled = m_SlotState == E_SLOTSTATE.Empty ? false : true;
    }

    void Start()
    {
        m_FinishedItemImage = transform.GetChild(0).GetComponent<Image>();

        m_SlotState = E_SLOTSTATE.Empty;       // ����·� ����
        m_FinishedItemImage.enabled = false;   // �̹��� ��Ȱ��ȭ
    }
    
    void Update()
    {
        
    }
}
