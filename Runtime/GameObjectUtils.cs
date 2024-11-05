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
        /// ���� ������Ʈ���� ������Ʈ�� �������ų� ���ٸ� �߰��ϴ� �Լ�
        /// </summary>
        static public T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            if (go == null)
            {
                throw new ArgumentNullException(nameof(go), "GameObject cannot be null");
            }

            // ������Ʈ�� ��������, ������ �߰��� �� ��ȯ
            return go.GetComponent<T>() ?? go.AddComponent<T>();
        }

        /// <summary>
        /// Ư���� �̸��� ������ �ִ� �ڽ� ������Ʈ�� ã�� ��ȯ�ϴ� �Լ�
        /// </summary>
        static public GameObject FindChildByName(GameObject go, string name, bool recursive = false)
        {
            Transform transform = FindComponentInChildrenByName<Transform>(go, name, recursive);
            return transform?.gameObject;
        }

        /// <summary>
        /// Ư���� �̸��� ������ �ִ� �ڽ� ������Ʈ�� ã�� ��ȯ�ϴ� �Լ�
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
        /// ��� �ڽ� ������Ʈ�� �����ϴ� �Լ�
        /// </summary>
        static public void DestroyAllChildren(GameObject go)
        {
            if (go == null)
            {
                throw new ArgumentNullException(nameof(go), "GameObject cannot be null");
            }

            // Transform�� ����Ͽ� �ڽ� ������Ʈ���� ���� ��ȸ
            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(go.transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// ��� �ڽ� ������Ʈ�� ��ȯ�ϴ� �Լ�
        /// </summary>
        static public GameObject[] GetAllChildren(GameObject go)
        {
            if (go == null)
            {
                throw new ArgumentNullException(nameof(go), "GameObject cannot be null");
            }

            // GetComponentsInChildren�� ����ϵ�, ù ��° ��Ҵ� �θ� �ڽ��̹Ƿ� ����
            Transform[] childrenTransforms = go.GetComponentsInChildren<Transform>(true);
            List<GameObject> children = new();

            for (int i = 1; i < childrenTransforms.Length; i++) // i=1���� �����Ͽ� �θ� ����
            {
                children.Add(childrenTransforms[i].gameObject);
            }

            return children.ToArray();
        }
    }
}