using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MyUtil.Editor
{
    /// <summary>
    /// ������ ���� Ŭ���� 
    /// </summary>
    public static class EditorHelper
    {
        /// <summary>
        /// �߾ӿ� �� �ʵ带 �߰��ϴ� �Լ�
        /// </summary>
        public static void DrawCenterLabel(GUIContent content, Color color, int size, FontStyle style)
        {
            var guiStyle = new GUIStyle
            {
                fontSize = size,
                fontStyle = style,
                normal = { textColor = color },
                padding = { top = 10, bottom = 10 }
            };

            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label(content, guiStyle);
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }

        public static void DrawScriptLine(GUIContent content, Color color, int size, FontStyle style)
        {
            var guiStyle = new GUIStyle
            {
                fontSize = size,
                fontStyle = style,
                normal = { textColor = color },
                padding = { top = 10, bottom = 10 }
            };

            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label(content, guiStyle, GUILayout.Width(Screen.width), GUILayout.Height(50));
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// �߾ӿ� ��ư �ʵ带 �߰��ϴ� �Լ�
        /// </summary>
        public static bool DrawCenterButton(string text, Vector2 size)
        {
            bool clicked;

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                clicked = GUILayout.Button(text, GUILayout.Width(size.x), GUILayout.Height(size.y));
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            return clicked;
        }

        public static Vector2 DrawPalette(Vector2 scrollPos, int gapSpace, int itemCnt, float areaWidth, Vector2 slotSize, Action<int> onDrawer)
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);
            {
                int horCnt = (int)(areaWidth / slotSize.x);
                if (horCnt <= 0)
                {
                    horCnt = 1;
                }

                int verCnt = itemCnt / horCnt;

                if (itemCnt % horCnt > 0)
                {
                    verCnt++;
                }
                if (verCnt <= 0)
                {
                    verCnt = 1;
                }

                GUILayout.BeginVertical();
                {
                    for (int i = 0; i < verCnt; i++)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            for (int j = 0; j < horCnt; j++)
                            {
                                int idx = j + (i * horCnt);
                                if (idx >= itemCnt)
                                {
                                    break;
                                }

                                onDrawer(idx);

                                GUILayout.Space(gapSpace);
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndScrollView();

            return scrollPos;
        }

        /// <summary>
        /// �� �信�� ���̸� ���� ������ ����� �����ϱ� ���� Ŀ���� ����ĳ��Ʈ
        /// </summary>
        public static void Raycast(Vector3 rayOriPos, Vector3 rayDestPos, out Vector3 hitPos)
        {
            // -z ���� ��, �Ϲ������� ����Ƽ���� ȭ���� �ٶ󺸴� ������ ����� �����ϱ� ���� 3���� ��
            Vector3 planePos01 = Vector3.up;
            Vector3 planePos02 = Vector3.right;
            Vector3 planePos03 = Vector3.down;

            // ȭ���� �ٶ󺸴� ���� ��, ����� ������ �������� ���
            Vector3 planeDir = Vector3.Cross((planePos02 - planePos01).normalized, (planePos03 - planePos01).normalized);
            // ���� ������ ��ǥ ���� ���ϴ� ���� ���
            Vector3 lineDir = (rayDestPos - rayOriPos).normalized;

            // �� ���͸� �����ؼ� ���̿� ��� ������ ���踦 ǥ��
            // ���̿� ����� �󸶳� ������ �����ϴ���, �Ǵ� �󸶳� �ָ� ������ �ִ����� ��Ÿ����. 
            // 3���� ���� ������ �浹�� ã�� ���� �̿�
            float dotLinePlane = Vector3.Dot(lineDir, planeDir);
            float t = Vector3.Dot(planePos01 - rayOriPos, planeDir) / dotLinePlane;

            hitPos = rayOriPos + (lineDir * t);
        }

        public static Texture2D MakeTexture(int width, int height, Color color)
        {
            // ���ο� 2D �ؽ�ó�� �����մϴ�.
            Texture2D texture = new Texture2D(width, height);

            // ��� �ȼ��� ������ �������� ä��ϴ�.
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }
            texture.SetPixels(pixels);

            // ���� ������ �����մϴ�.
            texture.Apply();

            return texture;
        }
    }
}