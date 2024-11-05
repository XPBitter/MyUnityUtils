using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtil.ObjectPool
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        protected Dictionary<string, ObjectPool> mPoolDic = new();

        [field: SerializeField] private List<ObjectPoolInfo> mObjectPoolInfos = new();

        protected override void Awake()
        {
            base.Awake();
            InitPoolManager();
        }

        public virtual void InitPoolManager()
        {
            foreach (ObjectPoolInfo info in mObjectPoolInfos)
            {
                ObjectPool pool = new ObjectPool(info.Size, info.Prefab, info.Parent);
                mPoolDic.Add(info.Id, pool);
            }
        }

        public virtual T Rent<T>(string id, Transform point) where T : Poolable
        {
            return mPoolDic[id].TakeFromPool(point) as T;
        }

        public virtual T Rent<T>(string id, Vector3 vector) where T : Poolable
        {
            return mPoolDic[id].TakeFromPool(vector) as T;
        }

        public virtual void Return<T>(string id, T poolable) where T : Poolable
        {
            mPoolDic[id].ReturnToPool(poolable);
        }
    }

    [Serializable]
    public struct ObjectPoolInfo
    {
        public int Size;
        public string Id;
        public Poolable Prefab;
        public Transform Parent;
    }
}