using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 슬롯 상태
public enum E_SLOTSTATE
{
    Empty = 0, // 아이템이 없는 상태
    Full       // 아이템이 있는 상태
}

// 장비 아이템
public enum E_EQUIPMENT_ITEMS
{
    None = -1,

    WoodenAxe,          // 나무 도끼
    FishingRod,         // 낚싯대
    CarrotOnAStick,     // 당근 낚싯대

    Max
}

// 소비 아이템
public enum E_CONSUMER_ITEMS
{
    None = -1,

    Wood,               // 나무
    Stick,              // 막대기
    String,             // 실
    Carrot,             // 당근

    Max
}

// 아이템 타입
public enum E_ITEMTYPE
{
    None = -1,

    Equipment,       // 장비 아이템
    Consumption      // 소모 아이템
}
