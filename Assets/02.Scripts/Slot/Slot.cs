using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,
    IPointerClickHandler   // ������ ������ �� �� ȣ��
{
    public Item m_ItemInfo = null;          // ������ ����
    private Image m_SlotItemImage = null;   // ������ �̹���
    private Text m_SlotItemCount = null;    // ������ ����

    public E_SLOTSTATE m_SlotState;         // ���� ����

    private void Awake()
    {
        /* ���� ���� ������Ʈ Image,Text ������Ʈ�� ���� */
        m_SlotItemImage = transform.GetChild(0).GetComponent<Image>();
        m_SlotItemCount = m_SlotItemImage.transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {
        m_SlotState = E_SLOTSTATE.Empty;
        m_ItemInfo = new Item(E_ITEMS.None, null);

        UpdateSlotUI();
    }

    // ������ ������ �� �� ȣ�� (��,�� Ŭ�� �����ؼ� ����)
    // https://docs.unity3d.com/kr/530/ScriptReference/EventSystems.PointerEventData.html PointerEventData
    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager tempManager = EventManager.GetInstance;
        Text tempText;
        
        // �������� ��� �ִ� ���
        if (tempManager.m_isDragging)
        {
            switch(m_SlotState) 
            {
                // ������ ���� ����(���)
                case E_SLOTSTATE.Empty:
                    switch (eventData.pointerId)
                    {
                        // ���콺 ��Ŭ�� (��ü)
                        case -1:
                            ItemDrop(tempManager, eventData);
                            break;

                        // ���콺 ��Ŭ�� (1����)
                        case -2:
                            ItemDrop(tempManager, eventData);
                            break;
                    }
                    
                    break;

                // ������ �ִ� ����(������ ��ġ��, ����) 
                case E_SLOTSTATE.Full:
                    int currItemCount = int.Parse(m_SlotItemCount.text); // ���� ���Կ� �ִ� ������ ����
                    
                    if (currItemCount == ItemGenerator.GetInstance.m_MaxItemCount || 
                        this.m_ItemInfo != tempManager.m_DraggingItemInfo) // ���� ������ ������ ���� �� ���(����) �̰ų� ���� �ٸ� ������ �� ���
                    {
                        if (eventData.pointerId == -2) // ��Ŭ�� ����
                            return;
                        tempManager.m_isDragging = false;            // �巡�� ����

                        // �巡�� ���� ���Կ� ���� ���� ���� �Ҵ�
                        tempManager.m_BeginDraggingSlotInfo.m_ItemInfo = this.m_ItemInfo;                           // ������ ����
                        tempManager.m_BeginDraggingSlotInfo.m_SlotItemImage.sprite = this.m_SlotItemImage.sprite;   // ������ �̹���
                        tempManager.m_BeginDraggingSlotInfo.m_SlotItemCount.text = this.m_SlotItemCount.text;       // ������ ����
                        tempManager.m_BeginDraggingSlotInfo.m_SlotState = E_SLOTSTATE.Full;                         // ���� ���� ����
                        tempManager.m_BeginDraggingSlotInfo.UpdateSlotUI();                                         // UI ������Ʈ

                        // ���� ���Կ� �巡�� �Ǿ� �ִ� ���� �Ҵ�
                        m_ItemInfo = tempManager.m_DraggingItemInfo;                                                // ������ ���� ��������
                        m_SlotItemImage.sprite = tempManager.m_DraggingItem.GetComponent<Image>().sprite;           // ��� �ִ� ������ �̹����� ����
                        m_SlotItemCount.text = tempManager.m_DraggingItem.GetComponentInChildren<Text>().text;      // ��� �ִ� ������ �ؽ�Ʈ�� ����
                        UpdateSlotUI();                                                                             // UI ������Ʈ

                        tempManager.m_DraggingItem.SetActive(false); // �巡�� ������ ������Ʈ ��Ȱ��ȭ
                    }
                    else    // ���� ������ ������ ���� ���� ���� ���(������ ��ġ��) :: ���� ������
                    {
                        switch(eventData.pointerId)
                        {
                            // ��Ŭ�� (��°�� ��ħ)
                            case -1:
                                tempText = tempManager.m_DraggingItem.GetComponentInChildren<Text>();                   // �巡�� ���� ������ �ؽ�Ʈ�� ����
                                int itemCount = int.Parse(this.m_SlotItemCount.text);                                   // ���� ������ ����
                                int dragItemCount = int.Parse(tempText.text);                                           // �巡�� ���� ������ ����

                                // ������ �ִ� ������ + �巡�׷� ������ �������� �ִ� ������ ������
                                if (itemCount + dragItemCount > ItemGenerator.GetInstance.m_MaxItemCount)
                                {
                                    int totalCount = itemCount + dragItemCount;
                                    m_SlotItemCount.text = ItemGenerator.GetInstance.m_MaxItemCount.ToString();         // �ִ� ������ ����
                                    tempText.text = (totalCount - ItemGenerator.GetInstance.m_MaxItemCount).ToString(); // �巡�� ���� ������ ���� �Ѱ���-�ִ밳��
                                    UpdateSlotUI();                                                                     // UI ������Ʈ
                                    return;
                                }

                                // �ִ� �������� ������
                                m_SlotItemCount.text = (itemCount + dragItemCount).ToString();   // ������ ��ġ��
                                tempManager.m_isDragging = false;                                // �巡�� ����
                                tempManager.m_DraggingItem.SetActive(false);                     // �巡�� ������ ������Ʈ ��Ȱ��ȭ
                                UpdateSlotUI();                                                  // UI ������Ʈ
                                break;

                            // ��Ŭ�� (�ϳ��� ��ħ)
                            case -2:
                                ItemDrop(tempManager, eventData);

                                break;

                        }
                        
                    }
                    break;
            }
        }

        else // �������� ��� ���� ���� ��� (������ ���)
        {
            if (m_SlotState == E_SLOTSTATE.Empty) // �� ���� ����
                return;

            switch (eventData.pointerId)
            {
                // ���콺 ��Ŭ�� :: ��ü �� ���
                case -1:
                    ItemHold(tempManager, eventData);
                    break;

                // ���콺 ��Ŭ�� :: ����(�ø�) ���
                case -2:
                    ItemHold(tempManager, eventData);  
                    break;
            }
        }
        
        if (transform.parent.CompareTag("CreaftingTable")) // ���۴뿡 �����ϸ� ���̺� ���� ����
        {
            CreaftingTable.GetInstance.TableInfoRenewal();
        }
    }

    // ������ ��� (��Ŭ��, ��Ŭ�� ����)
    void ItemHold(EventManager p_eventManager, PointerEventData p_data)
    {
        p_eventManager.m_isDragging = true;
        p_eventManager.m_DraggingItem.SetActive(true);
        p_eventManager.m_BeginDraggingSlotInfo = this;  // �巡�� ������ ���� ���� ����
        p_eventManager.m_DraggingItemInfo = m_ItemInfo; // ������ ���� ��������

        Image tempImage = p_eventManager.m_DraggingItem.GetComponent<Image>();           // ��� �ִ� ������ �̹����� ����
        Text tempText = p_eventManager.m_DraggingItem.GetComponentInChildren<Text>();    // ��� �ִ� ������ �ؽ�Ʈ�� ����

        switch(p_data.pointerId)
        {
            // ��Ŭ��
            case -1:
                tempImage.sprite = m_SlotItemImage.sprite;  // ������ �̹���
                tempText.text = m_SlotItemCount.text;       // ������ ����

                ReSetSlotUI();  // �ش� ���� �ʱ�ȭ
                break;

            // ��Ŭ��
            case -2:
                int curritemCount = int.Parse(m_SlotItemCount.text);        // ���� ������ ����

                if (curritemCount % 2 == 0) // ¦��
                {
                    tempImage.sprite = m_SlotItemImage.sprite;              // �巡�� ������ �̹���
                    tempText.text = (curritemCount * 0.5).ToString();       // �巡�� ������ ����
                    m_SlotItemCount.text = (curritemCount * 0.5).ToString();// ���� ������ ���� ����
                }

                else // Ȧ��
                {
                    int roundInt = Mathf.CeilToInt(curritemCount * 0.5f);           // �ø�
                    tempImage.sprite = m_SlotItemImage.sprite;                      // �巡�� ������ �̹���
                    tempText.text = roundInt.ToString();                            // �巡�� ������ ����
                    m_SlotItemCount.text = (curritemCount - roundInt).ToString();   // ���� ������ ���� ����

                    // �������� �ϳ��� ���� ������ �ʱ�ȭ
                    if (m_SlotItemCount.text == "0")
                        ReSetSlotUI();
                }
                break;
        }
    }

    // ������ ���� (��Ŭ��, ��Ŭ�� ����)
    void ItemDrop(EventManager p_eventManager, PointerEventData p_data)
    {
        int DropItemCount = 1;

        m_ItemInfo = p_eventManager.m_DraggingItemInfo;   // ������ ���� ��������
        Image tempImage = p_eventManager.m_DraggingItem.GetComponent<Image>();          // ��� �ִ� ������ �̹����� ����
        Text tempText = p_eventManager.m_DraggingItem.GetComponentInChildren<Text>();   // ��� �ִ� ������ �ؽ�Ʈ�� ����

        switch(p_data.pointerId)
        {
            // ��Ŭ��
            case -1:
                p_eventManager.m_isDragging = false;            // �巡�� ����
                m_SlotItemImage.sprite = tempImage.sprite;      // ���� ������ �̹��� �Ҵ�
                m_SlotItemCount.text = tempText.text;           // ���� ������ ���� �Ҵ�

                p_eventManager.m_DraggingItem.SetActive(false); // �巡�� ������ ������Ʈ ��Ȱ��ȭ
                
                break;
            
            // ��Ŭ��
            case -2:
                if (int.Parse(tempText.text) - DropItemCount == 0)    // �巡�� ���� ������ ������ -1��(�巡�� ����)
                {
                    p_eventManager.m_isDragging = false;              // �巡�� ����
                    p_eventManager.m_DraggingItem.SetActive(false);   // �巡�� ������ ������Ʈ ��Ȱ��ȭ
                }

                m_SlotItemImage.sprite = tempImage.sprite;                                           // ���� ������ �̹��� �Ҵ�
                m_SlotItemCount.text = (int.Parse(m_SlotItemCount.text) + DropItemCount).ToString(); // ���� ������ ���� �Ҵ�
                tempText.text = (int.Parse(tempText.text) - DropItemCount).ToString();               // �巡�� ���� ������ ���� -1

                
                break;
        }

        m_SlotState = E_SLOTSTATE.Full; // ���� ���� ����
        UpdateSlotUI(); // UI ������Ʈ
    }

    // ���� �ʱ�ȭ
    public void ReSetSlotUI()
    {
        m_ItemInfo = null;                  // ������ ���� �ʱ�ȭ
        m_SlotState = E_SLOTSTATE.Empty;    // ������ ���� �� ���·� ����
        m_SlotItemCount.text = "0";         // ������ �ؽ�Ʈ �ʱ�ȭ

        m_ItemInfo = new Item(E_ITEMS.None, null);

        UpdateSlotUI();                     // UI ������Ʈ
    }

    public void UpdateSlotUI()
    {
        /* ������ �� ���¸� ��Ȱ��ȭ */
        m_SlotItemImage.enabled = m_SlotState == E_SLOTSTATE.Full ? true : false;
        m_SlotItemCount.enabled = m_SlotState == E_SLOTSTATE.Full ? true : false;
    }


    void Update()
    {
        
    }
}
