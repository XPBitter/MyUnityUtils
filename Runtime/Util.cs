using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MyUtil
{ 
    public class Util
    {
       

        static public bool CheckGrounded(Vector3 position, float distance, int layer)
        {
            return Physics.CheckSphere(position, distance, layer);
        }



       

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
