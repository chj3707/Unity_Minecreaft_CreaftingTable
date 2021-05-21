using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Image m_SlotItemImage = null;   // 아이템 이미지
    private Text m_SlotItemCount = null;    // 아이템 개수

    private E_SLOTSTATE m_SlotState;        // 슬롯 상태

    private void Awake()
    {
        /* 슬롯 하위 오브젝트 Image,Text 컴포넌트에 접근 */
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
        /* 슬롯이 빈 상태면 비활성화 */
        m_SlotItemImage.enabled = m_SlotState == E_SLOTSTATE.Full ? true : false;
        m_SlotItemCount.enabled = m_SlotState == E_SLOTSTATE.Full ? true : false;
    }

    void Update()
    {
        
    }
}
