using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���ÿ� Ŭ����
 */

public class SlotGenerator : MonoBehaviour
{
    private static SlotGenerator m_Instance = null;

    public GameObject m_Slot = null; // ������ ������Ʈ(����)

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

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

    public static SlotGenerator GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = FindObjectOfType<SlotGenerator>();
            return m_Instance;
        }

        return m_Instance;
    }

    
    void Update()
    {
        
    }
}
