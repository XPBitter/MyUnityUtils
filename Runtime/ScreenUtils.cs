using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MyUtil.ScreenUtilities
{
    public static class ScreenUtils
    {
        /// <summary>
        /// ȭ���� �߽� ��ǥ�� ����Ͽ� ��ȯ�ϴ� �Լ�
        /// </summary>
        static public Vector3 GetScreenCenter()
        {
            if (Camera.main == null)
            {
                throw new InvalidOperationException("Main camera is not available.");
            }

            float screenWidth = Camera.main.pixelWidth;
            float screenHeight = Camera.main.pixelHeight;

            return new Vector3(screenWidth / 2, screenHeight / 2, 0f);
        }
    }
}