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
        /// Ư�� ��ǥ�� �������� ���鿡 �پ��ִ��� Ȯ���ϴ� �Լ�
        /// </summary>
        static public bool CheckGrounded(Vector3 position, float distance, int layer)
        {
            return Physics.CheckSphere(position, distance, layer);
        }
    }
}