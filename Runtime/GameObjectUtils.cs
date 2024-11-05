using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MyUtil.GameObjectUtilities
{
    public static class GameObjectUtils
    {
        /// <summary>
        /// 게임 오브젝트에서 컴포넌트를 가져오거나 없다면 추가하는 함수
        /// </summary>
        static public T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            if (go == null)
            {
                throw new ArgumentNullException(nameof(go), "GameObject cannot be null");
            }

            // 컴포넌트를 가져오고, 없으면 추가한 후 반환
            return go.GetComponent<T>() ?? go.AddComponent<T>();
        }

        /// <summary>
        /// 특정한 이름을 가지고 있는 자식 오브젝트를 찾아 반환하는 함수
        /// </summary>
        static public GameObject FindChildByName(GameObject go, string name, bool recursive = false)
        {
            Transform transform = FindComponentInChildrenByName<Transform>(go, name, recursive);
            return transform?.gameObject;
        }

        /// <summary>
        /// 특정한 이름을 가지고 있는 자식 컴포넌트를 찾아 반환하는 함수
        /// </summary>
        static public T FindComponentInChildrenByName<T>(GameObject go, string name, bool recursive = false) where T : UnityEngine.Component
        {
            if (go == null)
            {
                throw new ArgumentNullException(nameof(go), "GameObject cannot be null");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }

            if (!recursive)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    Transform childTransform = go.transform.GetChild(i);
                    if (childTransform.name == name)
                    {
                        T component = childTransform.GetComponent<T>();
                        if (component != null)
                        {
                            return component;
                        }
                    }
                }
            }
            else
            {
                foreach (T component in go.GetComponentsInChildren<T>())
                {
                    if (component.name == name)
                    {
                        return component;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 모든 자식 오브젝트를 삭제하는 함수
        /// </summary>
        static public void DestroyAllChildren(GameObject go)
        {
            if (go == null)
            {
                throw new ArgumentNullException(nameof(go), "GameObject cannot be null");
            }

            // Transform을 사용하여 자식 오브젝트들을 직접 순회
            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(go.transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// 모든 자식 오브젝트를 반환하는 함수
        /// </summary>
        static public GameObject[] GetAllChildren(GameObject go)
        {
            if (go == null)
            {
                throw new ArgumentNullException(nameof(go), "GameObject cannot be null");
            }

            // GetComponentsInChildren을 사용하되, 첫 번째 요소는 부모 자신이므로 제외
            Transform[] childrenTransforms = go.GetComponentsInChildren<Transform>(true);
            List<GameObject> children = new();

            for (int i = 1; i < childrenTransforms.Length; i++) // i=1부터 시작하여 부모 제외
            {
                children.Add(childrenTransforms[i].gameObject);
            }

            return children.ToArray();
        }
    }
}