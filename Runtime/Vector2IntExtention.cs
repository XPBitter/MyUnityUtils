using System.Collections.Generic;
using UnityEngine;

namespace MyUtil.Extention
{
    public static class Vector2IntExtention
    {
        /// <summary>
        /// 인자로 들어간 리스트 내부에 해당 벡터가 포함되어 있는 여부를 반환하는 함수
        /// </summary>
        public static bool ContainsPos(this Vector2Int vec, List<Vector2Int> list)
        {
            foreach (Vector2Int v in list)
            {
                if (v.x == vec.x && v.y == vec.y)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 해당 벡터를 일정 각도 회전한 값을 노말라이즈 후 정수형으로 반올림한 벡터로 반환하는 함수
        /// </summary>
        public static Vector2Int Rotate(this Vector2Int vec, float angle)
        {
            Vector3 vec_3 = (Vector2)vec;
            vec_3 = Quaternion.Euler(0, 0, angle) * vec_3;
            vec_3.Normalize();
            return new Vector2Int(Mathf.RoundToInt(vec_3.x), Mathf.RoundToInt(vec_3.y));
        }
    }
}