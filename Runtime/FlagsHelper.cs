using System;
using System.Collections.Generic;

namespace MyUtil.Helper
{
    /// <summary>
    /// Enum ������� ��Ʈ ����ũ�� ���� Ȱ���ϱ� ���� ��ƿ Ŭ����
    /// </summary>
    public static class FlagsHelper
    {
        /// <summary>
        /// �÷��װ� ���ԵǾ��ִ��� ���� ��ȯ
        /// </summary>
        /// <typeparam name="T">Enum Ÿ��</typeparam>
        /// <param name="main">�˻� ��� �÷���</param>
        /// <param name="flag">Ȯ�� �÷���</param>
        public static bool IsContains<T>(T main, T flag) where T : Enum
        {
            return main.HasFlag(flag);
        }
        /// <summary>
        /// �÷��װ� �ϳ��� ���ԵǾ��ִ��� ���� ��ȯ
        /// </summary>
        /// <typeparam name="T">Enum Ÿ��</typeparam>
        /// <param name="main">�˻� ��� �÷���</param>
        /// <param name="flags">Ȯ�� �÷��� �迭</param>
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
        /// �÷��׸� �߰��ϴ� �Լ�
        /// </summary>
        /// <typeparam name="T">Enum Ÿ��</typeparam>
        /// <param name="main">��� �÷���</param>
        /// <param name="flag">�߰� �÷���</param>
        public static T Set<T>(T main, T flag) where T : Enum
        {
            return (T)(object)(((int)(object)main | (int)(object)flag));
        }
        /// <summary>
        /// ���ڷ� ���� ��� �÷��׸� �߰��ϴ� �Լ�
        /// </summary>
        /// <typeparam name="T">Enum Ÿ��</typeparam>
        /// <param name="main">��� �÷���</param>
        /// <param name="flags">�߰� �÷��� �迭</param>
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
        /// �÷��׸� �����ϴ� �Լ�
        /// </summary>
        /// <typeparam name="T">Enum Ÿ��</typeparam>
        /// <param name="main">��� �÷���</param>
        /// <param name="flag">���� �÷���</param>
        public static T Unset<T>(T main, T flag) where T : Enum
        {
            return (T)(object)((int)(object)main & ~(int)(object)flag);
        }
        /// <summary>
        /// ���ڷ� ���� ��� �÷��׸� �����ϴ� �Լ� 
        /// </summary>
        /// <typeparam name="T">Enum Ÿ��</typeparam>
        /// <param name="main">��� �÷���</param>
        /// <param name="flag">���� �÷��� �迭</param>
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
        /// �÷����� ������ ��ȯ
        /// </summary>
        /// <typeparam name="T">Enum Ÿ��</typeparam>
        /// <param name="main">��� �÷���</param>
        public static T GetComplement<T>(T main) where T : Enum
        {
            return (T)(object)(~(int)(object)main);
        }
    }
}