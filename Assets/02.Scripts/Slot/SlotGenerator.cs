using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 슬롯 세팅용 클래스
 */

public class SlotGenerator : Singleton_Mono<SlotGenerator> // 싱글톤 적용
{
    // 외부에서 생성되지 않도록 생성자 protected 선언
    protected SlotGenerator() { }

    public GameObject m_Slot = null; // 복사할 오브젝트(슬롯)

    void Start()
    {
        m_Slot.SetActive(false);
    }

    // 슬롯 세팅 함수
    public void SetSlot(int p_slotcount, Transform p_transform)
    {
        for (int i = 0; i < p_slotcount; i++)
        {
            GameObject copyObj = GameObject.Instantiate(m_Slot);    // 슬롯 오브젝트 복사
            copyObj.SetActive(true);                                // 복사한 오브젝트 활성화
            copyObj.name = string.Format($"SLOT_{i + 1}");          // 복사한 오브젝트 이름
            copyObj.transform.SetParent(p_transform);               // 복사한 오브젝트 상위 오브젝트 설정
        }
    }
 
    void Update()
    {
        
    }
}
