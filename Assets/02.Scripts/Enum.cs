using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ����
public enum E_SLOTSTATE
{
    Empty = 0, // �������� ���� ����
    Full       // �������� �ִ� ����
}

// ������
public enum E_ITEMS
{
    None = -1,

    Wood,               // ����
    Stick,              // �����
    String,             // ��
    Carrot,             // ���

    WoodenAxe,          // ���� ����
    FishingRod,         // ���˴�
    CarrotOnAStick,     // ��� ���˴�

    Max
}

