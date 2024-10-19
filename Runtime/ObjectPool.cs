using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtil.ObjectPool
{
    /// <summary>
    /// 오브젝트 풀 일반화 클래스
    /// </summary>
    public class ObjectPool
    {
        protected Poolable mPoolableObject;
        /// <summary>
        /// 오브젝트 풀 사이즈
        /// </summary>
        protected int mPoolSize;
        protected Transform mParents;

        /// <summary>
        /// 오브젝트 풀링용 큐
        /// </summary>
        protected Queue<Poolable> mPool = new();

        public Type PoolableType() => mPoolableObject.GetType();


        public ObjectPool(int size, Poolable poolable, Transform parents)
        {
            mPoolSize = size;
            mPoolableObject = poolable;
            mParents = parents;
            InitPool();
        }

        protected virtual Poolable InitPoolable(Transform parents)
        {
            GameObject go = UnityEngine.Object.Instantiate(mPoolableObject.gameObject, parents);
            Poolable t = go.GetComponent<Poolable>();
            t.DeActivate();
            return t;
        }

        public virtual void InitPool()
        {
            for (int i = 0; i < mPoolSize; i++)
            {
                mPool.Enqueue(InitPoolable(mParents));
            }
        }

        public virtual Poolable TakeFromPool(Transform point)
        {
            Poolable t;
            if (mPool.Count > 0)
            {
                t = mPool.Dequeue();
            }
            else
            {
                t = InitPoolable(mParents);
            }
            t.gameObject.transform.position = point.position;
            return t;
        }

        public virtual Poolable TakeFromPool(Vector3 vector)
        {
            Poolable t;
            if (mPool.Count > 0)
            {
                t = mPool.Dequeue();
            }
            else
            {
                t = InitPoolable(mParents);
            }
            t.gameObject.transform.position = vector;
            return t;
        }

        public virtual void ReturnToPool(Poolable t)
        {
            if (mPool.Count < mPoolSize)
            {
                t.DeActivate();
                mPool.Enqueue(t);
            }
            else
            {
                t.DeActivate();
                t.Delete();
            }
        }
    }
}