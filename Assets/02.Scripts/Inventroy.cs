using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������
 */ 

public class Inventroy : MonoBehaviour
{
    private int m_SlotCount = 27; // ���� ����


    void Start()
    {
        SlotGenerator.GetInstance().SetSlot(m_SlotCount, this.transform); // ���� ����

    }

    
    void Update()
    {
        
    }
}
