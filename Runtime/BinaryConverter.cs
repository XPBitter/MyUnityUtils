using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace MyUtil.Serialization
{
    public static class BinaryConverter
    {
        /// <summary>
        /// ��ü�� ������ ���� ��ο� ���̳ʸ� �������� �����մϴ�.
        /// </summary>
        /// <typeparam name="T">����ȭ ������ Ŭ���� Ÿ��</typeparam>
        /// <param name="filePath">������ ���� ���</param>
        /// <param name="obj">������ ��ü</param>
        /// <returns>���� ���� ����</returns>
        public static bool Save<T>(string filePath, T obj) where T : class, ISerializable
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, obj);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to save object: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// ������ ���� ��ο��� ��ü�� ���̳ʸ� �������� �ε��մϴ�.
        /// </summary>
        /// <typeparam name="T">����ȭ ������ Ŭ���� Ÿ��</typeparam>
        /// <param name="filePath">�ε��� ���� ���</param>
        /// <returns>�ε�� ��ü �Ǵ� null</returns>
        public static T Load<T>(string filePath) where T : class, ISerializable
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(fileStream) as T;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load object: " + e.Message);
                return null;
            }
        }
    }
}