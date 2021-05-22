using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 슬롯 상태
public enum E_SLOTSTATE
{
    Empty = 0, // 아이템이 없는 상태
    Full       // 아이템이 있는 상태
}

// 아이템
public enum E_ITEMS
{
    None = -1,

    Wood,               // 나무
    Stick,              // 막대기
    String,             // 실
    Carrot,             // 당근

    WoodenAxe,          // 나무 도끼
    FishingRod,         // 낚싯대
    CarrotOnAStick,     // 당근 낚싯대

    Max
}

