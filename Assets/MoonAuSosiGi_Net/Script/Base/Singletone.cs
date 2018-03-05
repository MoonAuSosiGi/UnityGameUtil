using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonAuSosiGi.Net.Base
{
    /// <summary>
    /// 싱글턴 패턴을 구현하기 위한 베이스 클래스
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singletone<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// 싱글턴 패턴에서의 스태틱 객체. 
        /// </summary>
        private static T m_instance = default(T);

        /// <summary>
        /// 싱글턴 패턴의 객체를 가져온다.
        /// </summary>
        /// <returns>싱글턴 패턴이 가지고 있는 객체</returns>
        public static T Instance()
        {
            if (m_instance == null)
            {
                try
                {
                    m_instance = FindObjectOfType(typeof(T)) as T;

                    if (m_instance == null)
                    {
                        Debug.LogError("Singletone Object is null");
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                    return null;
                }
            }
            return m_instance;
        }
    }

}
