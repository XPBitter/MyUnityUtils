using UnityEngine;

namespace MyUtil.ObjectPool
{
    /// <summary>
    /// Ǯ�� ������ ��ü
    /// </summary>
    public abstract class Poolable : MonoBehaviour
    {
        /// <summary>
        /// ��ü �ĺ��� Enum ��
        /// </summary>
        abstract public string ID { get; protected set; }
        /// <summary>
        /// Ǯ�� ������ ��ü�� Ȱ��ȭ
        /// </summary>
        abstract public void Activate();
        /// <summary>
        /// Ǯ�� ������ ��ü�� ��Ȱ��ȭ
        /// </summary>
        abstract public void DeActivate();
        /// <summary>
        /// Ǯ�� ������ ��ü�� ����
        /// </summary>
        abstract public void Delete();
    }
}