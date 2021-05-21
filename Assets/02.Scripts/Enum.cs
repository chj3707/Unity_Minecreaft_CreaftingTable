using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 슬롯 상태
public enum E_SLOTSTATE
{
    Empty = 0, // 아이템이 없는 상태
    Full       // 아이템이 있는 상태
}

// 아이템 타입
public enum E_ITEMTYPE
{
    None = -1,

    Equipment,       // 장비 아이템
    Consumption      // 소모 아이템
}
