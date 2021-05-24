using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/*
 * 전체적으로 사용할 정적 클래스
 */

public static class Core
{
    // 밑에 코드로 바꿈
    // 리플렉션 사용해서 클래스의 멤버변수 정보(Sprite) 가져오기
    //void static GetFiledInfoToSprite(object p_object, ref Image p_image)
    //{
    //    // https://ansohxxn.github.io/c%20sharp/ch9-8/ 리플렉션 참고
    //    // 리플렉션으로 멤버 변수에 접근 (m_ItemInfo :: 자료형 objct)
    //    Type type = p_object.GetType();
    //    FieldInfo[] fields = type.GetFields(BindingFlags.Public |
    //                                        BindingFlags.Instance);
    //    foreach (var field in fields)
    //    {
    //        if (field.FieldType.Name == "Sprite") // 자료형이 Sprite인 멤버 변수
    //        {
    //            p_image.sprite = field.GetValue(p_object) as Sprite; // sprite 할당
    //            break;
    //        }
    //    }
    //}

    // 리플렉션 사용해서 클래스 멤버변수 데이터 가져오기 (참조 형식)
    public static T GetFiledInfoToReflectionReferenceType<T>(object p_object, T p_dataType) where T : class
    {
        // https://ansohxxn.github.io/c%20sharp/ch9-8/ 리플렉션 참고
        // 리플렉션으로 멤버 변수에 접근 (m_ItemInfo :: 자료형 objct)
        T retValue;
        Type type = p_object.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                                            BindingFlags.Instance);
        foreach (var field in fields)
        {
            if (field.FieldType == p_dataType.GetType())  // 매개변수로 가져온 자료형과 같은 자료형 찾기
            {
                retValue = field.GetValue(p_object) as T; // 형변환
                return retValue;
            }
        }
        return null;
    }

    // 리플렉션 사용해서 클래스 멤버변수 데이터 가져오기 (값 형식)
    public static T GetFiledInfoToReflectionValueType<T>(object p_object, T p_dataType) where T : struct
    {
        // 리플렉션으로 멤버 변수에 접근 (m_ItemInfo :: 자료형 objct)
        T retValue;
        Type type = p_object.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                                            BindingFlags.Instance);
        foreach (var field in fields)
        {
            if (field.FieldType == p_dataType.GetType())  // 매개변수로 가져온 자료형과 같은 자료형 찾기
            {
                retValue = (T)field.GetValue(p_object);   // 형변환
                return retValue;
            }
        }
        return default(T);
    }
}
