using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MyUtil.Random
{
    public static class RandomUtils
    {
        /// <summary>
        /// 배열 내 무작위 원소를 반환하는 함수
        /// </summary>
        static public T GetRandomElement<T>(T[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                throw new ArgumentException("Array cannot be null or empty", nameof(arr));
            }

            return arr[UnityEngine.Random.Range(0, arr.Length)];
        }

        /// <summary>
        /// 여러 인자(배열)를 받아 그 내부의 값 중 중복 없이 일정 개수를 반환하는 함수
        /// </summary>
        static public T[] GetUniqueRandomElements<T>(int num, params T[] arr)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr), "Array cannot be null.");
            }

            if (num <= 0)
            {
                return Array.Empty<T>();
            }

            if (num >= arr.Length)
            {
                // 요청한 개수보다 배열의 크기가 작거나 같으면 배열 전체를 반환합니다.
                return arr;
            }

            List<T> result = new List<T>(num);
            List<int> indices = new List<int>(Enumerable.Range(0, arr.Length));

            for (int i = 0; i < num; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, indices.Count);
                int selectedIndex = indices[randomIndex];
                result.Add(arr[selectedIndex]);
                indices.RemoveAt(randomIndex);
            }

            return result.ToArray();
        }
    }
}