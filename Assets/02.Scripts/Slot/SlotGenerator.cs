using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���ÿ� Ŭ����
 */

public class SlotGenerator : Singleton_Mono<SlotGenerator> // �̱��� ����
{
    // �ܺο��� �������� �ʵ��� ������ protected ����
    protected SlotGenerator() { }

    public GameObject m_Slot = null; // ������ ������Ʈ(����)

    void Start()
    {
        m_Slot.SetActive(false);
    }

    // ���� ���� �Լ�
    public void SetSlot(int p_slotcount, Transform p_transform)
    {
        for (int i = 0; i < p_slotcount; i++)
        {
            GameObject copyObj = GameObject.Instantiate(m_Slot);    // ���� ������Ʈ ����
            copyObj.SetActive(true);                                // ������ ������Ʈ Ȱ��ȭ
            copyObj.name = string.Format($"SLOT_{i + 1}");          // ������ ������Ʈ �̸�
            copyObj.transform.SetParent(p_transform);               // ������ ������Ʈ ���� ������Ʈ ����
        }
    }
 
    void Update()
    {
        
    }
}
