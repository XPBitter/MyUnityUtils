using UnityEngine;

namespace MyUtil.ObjectPool
{
    /// <summary>
    /// 풀링 가능한 객체
    /// </summary>
    public abstract class Poolable : MonoBehaviour
    {
        /// <summary>
        /// 객체 식별용 Enum 값
        /// </summary>
        abstract public string ID { get; protected set; }
        /// <summary>
        /// 풀링 가능한 객체를 활성화
        /// </summary>
        abstract public void Activate();
        /// <summary>
        /// 풀링 가능한 객체를 비활성화
        /// </summary>
        abstract public void DeActivate();
        /// <summary>
        /// 풀링 가능한 객체를 삭제
        /// </summary>
        abstract public void Delete();
    }
}