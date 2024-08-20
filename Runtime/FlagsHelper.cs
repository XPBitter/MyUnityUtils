using System;
using System.Collections.Generic;

namespace MyUtil.Helper
{
    /// <summary>
    /// Enum 기반으로 비트 마스크를 쉽게 활용하기 위한 유틸 클래스
    /// </summary>
    public static class FlagsHelper
    {
        /// <summary>
        /// 플래그가 포함되어있는지 여부 반환
        /// </summary>
        /// <typeparam name="T">Enum 타입</typeparam>
        /// <param name="main">검사 대상 플래그</param>
        /// <param name="flag">확인 플래그</param>
        public static bool IsContains<T>(T main, T flag) where T : Enum
        {
            return main.HasFlag(flag);
        }
        /// <summary>
        /// 플래그가 하나라도 포함되어있는지 여부 반환
        /// </summary>
        /// <typeparam name="T">Enum 타입</typeparam>
        /// <param name="main">검사 대상 플래그</param>
        /// <param name="flags">확인 플래그 배열</param>
        public static bool IsContainsAnyOne<T>(T main, params T[] flags) where T : Enum
        {
            foreach (T flag in flags)
            {
                if (main.HasFlag(flag))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 플래그를 추가하는 함수
        /// </summary>
        /// <typeparam name="T">Enum 타입</typeparam>
        /// <param name="main">대상 플래그</param>
        /// <param name="flag">추가 플래그</param>
        public static T Set<T>(T main, T flag) where T : Enum
        {
            return (T)(object)(((int)(object)main | (int)(object)flag));
        }
        /// <summary>
        /// 인자로 들어온 모든 플래그를 추가하는 함수
        /// </summary>
        /// <typeparam name="T">Enum 타입</typeparam>
        /// <param name="main">대상 플래그</param>
        /// <param name="flags">추가 플래그 배열</param>
        public static T Set<T>(T main, params T[] flags) where T : Enum
        {
            int result = (int)(object)main;
            foreach (T flag in flags)
            {
                result |= (int)(object)flag;
            }
            return (T)(object)result;
        }
        /// <summary>
        /// 플래그를 제거하는 함수
        /// </summary>
        /// <typeparam name="T">Enum 타입</typeparam>
        /// <param name="main">대상 플래그</param>
        /// <param name="flag">제거 플래그</param>
        public static T Unset<T>(T main, T flag) where T : Enum
        {
            return (T)(object)((int)(object)main & ~(int)(object)flag);
        }
        /// <summary>
        /// 인자로 들어온 모든 플래그를 제거하는 함수 
        /// </summary>
        /// <typeparam name="T">Enum 타입</typeparam>
        /// <param name="main">대상 플래그</param>
        /// <param name="flag">제거 플래그 배열</param>
        public static T Unset<T>(T main, params T[] flags) where T : Enum
        {
            long result = (int)(object)main;
            foreach (T flag in flags)
            {
                result &= ~(int)(object)flag;
            }
            return (T)(object)result;
        }
        /// <summary>
        /// 플래그의 보수를 반환
        /// </summary>
        /// <typeparam name="T">Enum 타입</typeparam>
        /// <param name="main">대상 플래그</param>
        public static T GetComplement<T>(T main) where T : Enum
        {
            return (T)(object)(~(int)(object)main);
        }
    }
}