using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MyUtil.PhysicsUtilities
{
    public static class PhysicsUtils
    {
        /// <summary>
        /// 특정 좌표를 기준으로 지면에 붙어있는지 확인하는 함수
        /// </summary>
        static public bool CheckGrounded(Vector3 position, float distance, int layer)
        {
            return Physics.CheckSphere(position, distance, layer);
        }
    }
}