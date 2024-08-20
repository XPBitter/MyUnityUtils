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
        /// 객체를 지정된 파일 경로에 바이너리 형식으로 저장합니다.
        /// </summary>
        /// <typeparam name="T">직렬화 가능한 클래스 타입</typeparam>
        /// <param name="filePath">저장할 파일 경로</param>
        /// <param name="obj">저장할 객체</param>
        /// <returns>저장 성공 여부</returns>
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
        /// 지정된 파일 경로에서 객체를 바이너리 형식으로 로드합니다.
        /// </summary>
        /// <typeparam name="T">직렬화 가능한 클래스 타입</typeparam>
        /// <param name="filePath">로드할 파일 경로</param>
        /// <returns>로드된 객체 또는 null</returns>
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