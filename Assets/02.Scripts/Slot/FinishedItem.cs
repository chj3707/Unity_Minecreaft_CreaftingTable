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

    private object m_FinishedItem = null;       // �ϼ� ������ ����
    private Image m_FinishedItemImage = null;   // �ϼ�ǰ �̹���
    private Text m_FinishedItemText = null;     // �ϼ�ǰ ����
    public E_SLOTSTATE m_SlotState;             // ���� ����

    // �ϼ�ǰ Ȯ��
    public void FinishedCheak()
    {
        object tempItem = CreaftingTable.GetInstance.CompareTableToRecipe(); 

        // �����ǿ� ���������� null�� �޾ƿ�
        if (tempItem == null)
        {
            ResetSlotUI();
            return;
        }
        
        m_FinishedItem = tempItem;                                  // �ϼ� ������ ���� �Ҵ�


        m_FinishedItemImage.sprite = 
            Core.GetFiledInfoToReflectionReferenceType
            (m_FinishedItem, m_FinishedItemImage.sprite);           // �ϼ� ������ �̹��� ����

        m_FinishedItemText.text = "1";                              // �ϼ� ������ ���� ����

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

        tempManager.m_isDragging = true;                 // �巡�� ����
        tempManager.m_DraggingItem.SetActive(true);
        tempManager.m_BeginDraggingSlotInfo = null;      // �巡�� ������ ���� ���� ����
        tempManager.m_DraggingItemInfo = m_FinishedItem; // �巡�� ������ ���� �Ҵ�

        Image tempImage = tempManager.m_DraggingItem.GetComponent<Image>();           // �巡�� ������ �̹����� ����
        Text tempText = tempManager.m_DraggingItem.GetComponentInChildren<Text>();    // �巡�� ������ �ؽ�Ʈ�� ����    

        E_ITEMTYPE tempItemType = E_ITEMTYPE.None;                                    // ������ Ÿ�� Ȯ�ο� �ӽ� ����
        tempItemType = Core.GetFiledInfoToReflectionValueType
            (m_FinishedItem, tempItemType);                                           // �ϼ�ǰ�� ������ Ÿ�� ��������

        tempImage.sprite = m_FinishedItemImage.sprite;                                // �巡�� ������ �̹��� ����

        // ���
        if (tempItemType == E_ITEMTYPE.Equipment)                                     // �巡�� ������ ���� ����
        {
            tempText.text = "1";
            tempText.enabled = false; // ��� ������ ���� ��Ȱ��ȭ
        }
        // �Һ�
        else
            tempText.text = (int.Parse(m_FinishedItemText.text)).ToString();
     
        ResetSlotUI();                                  // �ϼ�ǰ ���� �ʱ�ȭ
        CreaftingTable.GetInstance.TableReset();        // ���� ���̺� �缳��
    }

    public void ResetSlotUI()
    {
        m_FinishedItem = null;                  // ������ ���� �ʱ�ȭ
        m_SlotState = E_SLOTSTATE.Empty;        // ������ ���� �� ���·� ����
        m_FinishedItemText.text = "0";          // ������ ���� �ʱ�ȭ

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
        m_FinishedItemText = m_FinishedItemImage.GetComponentInChildren<Text>();

        m_SlotState = E_SLOTSTATE.Empty;       // ����·� ����
        m_FinishedItemImage.enabled = false;   // �̹��� ��Ȱ��ȭ
        m_FinishedItemText.enabled = false;    // �ؽ�Ʈ ��Ȱ��ȭ
    }
    
    void Update()
    {
        
    }
}
