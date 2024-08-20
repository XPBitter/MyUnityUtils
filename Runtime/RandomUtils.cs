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
        /// �迭 �� ������ ���Ҹ� ��ȯ�ϴ� �Լ�
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
        /// ���� ����(�迭)�� �޾� �� ������ �� �� �ߺ� ���� ���� ������ ��ȯ�ϴ� �Լ�
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
                // ��û�� �������� �迭�� ũ�Ⱑ �۰ų� ������ �迭 ��ü�� ��ȯ�մϴ�.
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