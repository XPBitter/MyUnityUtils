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
        /// 화면의 중심 좌표를 계산하여 반환하는 함수
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
