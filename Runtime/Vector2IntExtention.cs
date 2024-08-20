using System.Collections.Generic;
using UnityEngine;

namespace MyUtil.Extention
{
    public static class Vector2IntExtention
    {
        /// <summary>
        /// ���ڷ� �� ����Ʈ ���ο� �ش� ���Ͱ� ���ԵǾ� �ִ� ���θ� ��ȯ�ϴ� �Լ�
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
        /// �ش� ���͸� ���� ���� ȸ���� ���� �븻������ �� ���������� �ݿø��� ���ͷ� ��ȯ�ϴ� �Լ�
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