using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MyUtil.Editor
{
    /// <summary>
    /// 에디터 헬퍼 클래스 
    /// </summary>
    public static class EditorHelper
    {
        /// <summary>
        /// 중앙에 라벨 필드를 추가하는 함수
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
        /// 중앙에 버튼 필드를 추가하는 함수
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
        /// 씬 뷰에서 레이를 쏴서 가상의 평면을 감지하기 위한 커스텀 레이캐스트
        /// </summary>
        public static void Raycast(Vector3 rayOriPos, Vector3 rayDestPos, out Vector3 hitPos)
        {
            // -z 방향 즉, 일반적으로 유니티에서 화면을 바라보는 방향의 평면을 구성하기 위한 3개의 점
            Vector3 planePos01 = Vector3.up;
            Vector3 planePos02 = Vector3.right;
            Vector3 planePos03 = Vector3.down;

            // 화면을 바라보는 방향 즉, 평면의 방향을 외적으로 계산
            Vector3 planeDir = Vector3.Cross((planePos02 - planePos01).normalized, (planePos03 - planePos01).normalized);
            // 시작 점에서 목표 점을 향하는 벡터 계산
            Vector3 lineDir = (rayDestPos - rayOriPos).normalized;

            // 두 벡터를 내적해서 레이와 평면 사이의 관계를 표시
            // 레이와 평면이 얼마나 가깝게 교차하는지, 또는 얼마나 멀리 떨어져 있는지를 나타낸다. 
            // 3차원 평면과 직선의 충돌점 찾는 공식 이용
            float dotLinePlane = Vector3.Dot(lineDir, planeDir);
            float t = Vector3.Dot(planePos01 - rayOriPos, planeDir) / dotLinePlane;

            hitPos = rayOriPos + (lineDir * t);
        }

        public static Texture2D MakeTexture(int width, int height, Color color)
        {
            // 새로운 2D 텍스처를 생성합니다.
            Texture2D texture = new Texture2D(width, height);

            // 모든 픽셀을 지정된 색상으로 채웁니다.
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }
            texture.SetPixels(pixels);

            // 변경 사항을 적용합니다.
            texture.Apply();

            return texture;
        }
    }
}