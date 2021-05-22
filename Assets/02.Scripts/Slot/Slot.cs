using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,
    IPointerClickHandler   // ������ ������ �� �� ȣ��
{
    public object m_ItemInfo = null;        // ������ ����
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

        UpdateSlotUI();
    }

    // ������ ������ �� �� ȣ��
    // https://docs.unity3d.com/kr/530/ScriptReference/EventSystems.PointerEventData.html PointerEventData
    public void OnPointerClick(PointerEventData eventData)
    {
        Image tempImage;
        Text tempText;
        EventManager tempManager = EventManager.GetInstance;

        // �������� ��� �ִ� ���
        if (tempManager.m_isDragging)
        {
            switch(m_SlotState) 
            {
                // ������ ���� ����(���)
                case E_SLOTSTATE.Empty:
                    tempManager.m_isDragging = false;              // �巡�� ����
                    m_ItemInfo = tempManager.m_DraggingItemInfo;   // ������ ���� ��������

                    tempImage = tempManager.m_DraggingItem.GetComponent<Image>();           // ��� �ִ� ������ �̹����� ����
                    tempText = tempManager.m_DraggingItem.GetComponentInChildren<Text>();   // ��� �ִ� ������ �ؽ�Ʈ�� ����

                    m_SlotItemImage.sprite = tempImage.sprite;   // ���� ������ �̹��� �Ҵ�
                    m_SlotItemCount.text = tempText.text;        // ���� ������ ���� �Ҵ�

                    tempManager.m_DraggingItem.SetActive(false); // �巡�� ������ ������Ʈ ��Ȱ��ȭ
                    m_SlotState = E_SLOTSTATE.Full;              // ���� ���� ����
                    UpdateSlotUI();                              // UI ������Ʈ
                    break;

                // ������ �ִ� ����(������ ��ġ��, ����)
                case E_SLOTSTATE.Full:
                    int currItemCount = int.Parse(m_SlotItemCount.text); // ���� ���Կ� �ִ� ������ ����
                    
                    if (currItemCount == ItemGenerator.GetInstance.m_MaxItemCount || 
                        this.m_ItemInfo != tempManager.m_DraggingItemInfo) // ���� ������ ������ ���� �� ���(����) �̰ų� ���� �ٸ� ������ �� ���
                    {
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
                        // ������ ���� ����
                        int tempCount = int.Parse(this.m_SlotItemCount.text);
                        m_SlotItemCount.text = tempManager.m_DraggingItem.GetComponentInChildren<Text>().text;     
                        tempManager.m_DraggingItem.GetComponentInChildren<Text>().text = tempCount.ToString();

                        UpdateSlotUI(); // UI ������Ʈ
                    }
                    break;
            }
        }

        else // �������� ��� ���� ���� ���
        {
            if (m_SlotState == E_SLOTSTATE.Empty) // �� ���� ����
                return;

            switch (eventData.pointerId)
            {
                // ���콺 ��Ŭ��
                case -1:
                    tempManager.m_isDragging = true;
                    tempManager.m_DraggingItem.SetActive(true);
                    tempManager.m_BeginDraggingSlotInfo = this;  // �巡�� ������ ���� ���� ����
                    tempManager.m_DraggingItemInfo = m_ItemInfo; // ������ ���� ��������
                    
                    tempImage = tempManager.m_DraggingItem.GetComponent<Image>();           // ��� �ִ� ������ �̹����� ����
                    tempText = tempManager.m_DraggingItem.GetComponentInChildren<Text>();   // ��� �ִ� ������ �ؽ�Ʈ�� ����

                    tempImage.sprite = m_SlotItemImage.sprite;  // ������ �̹���
                    tempText.text = m_SlotItemCount.text;       // ������ ����

                    ReSetSlotUI();  // �ش� ���� �ʱ�ȭ
                    break;

                // ���콺 ��Ŭ��
                case -2:
                    tempManager.m_isDragging = true;
                    tempManager.m_DraggingItem.SetActive(true);
                    tempManager.m_BeginDraggingSlotInfo = this;  // �巡�� ������ ���� ���� ����
                    tempManager.m_DraggingItemInfo = m_ItemInfo; // ������ ���� ��������

                    tempImage = tempManager.m_DraggingItem.GetComponent<Image>();           // ��� �ִ� ������ �̹����� ����
                    tempText = tempManager.m_DraggingItem.GetComponentInChildren<Text>();   // ��� �ִ� ������ �ؽ�Ʈ�� ����

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
    }

    // ���� �ʱ�ȭ
    public void ReSetSlotUI()
    {
        m_ItemInfo = null;                  // ������ ���� �ʱ�ȭ
        m_SlotState = E_SLOTSTATE.Empty;    // ������ ���� �� ���·� ����
        m_SlotItemCount.text = "0";         // ������ �ؽ�Ʈ �ʱ�ȭ

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
