using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Image m_SlotItemImage = null;   // ������ �̹���
    private Text m_SlotItemCount = null;    // ������ ����

    private E_SLOTSTATE m_SlotState;        // ���� ����

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
