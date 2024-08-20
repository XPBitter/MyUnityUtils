using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


namespace MyUtil.Random
{
    /// <summary>
    /// ���� ��ȯ�� ��� �� ���
    /// </summary>
    public enum RandomMode
    {
        WeightVariance // ����ġ �л� ���
    }

    public class RandomByWeight<T>
    {
        /// <summary>
        /// ����ġ ������ �з��� �ĺ��� ����Ʈ
        /// </summary>
        private List<(T, float)> candidatesByWeight = new();
        /// <summary>
        /// ����� ��(0.0 ~ 1.0)���� �з��� �ĺ��� ����Ʈ
        /// </summary>
        private List<(T, float)> candidatesByPercent = new();

        /// <summary>
        /// �� ����ġ�� ��
        /// </summary>
        public float TotalWeight { get; set; }
        [field: Range(0f, 1f)]
        /// <summary>
        /// ����ġ ���� ����
        /// </summary>
        public float VarianceRatio { get; set; } = 0.01f;

        /// <summary>
        /// ����ġ �л� Ƚ��
        /// </summary>
        public int VarianceCount { get; private set; }

        private float mVarianceAmount;


        /// <summary>
        /// Ʃ�� �迭 ���ڸ� ���� �ʱ� �����ϴ� ������
        /// </summary>
        /// <param name="tuples"></param>
        public RandomByWeight(float ratio, params (T, float)[] tuples)
        {

            VarianceRatio = ratio;
            // ����ġ ���� ����Ʈ �߰�
            foreach ((T, float) tuple in tuples)
            {
                candidatesByWeight.Add(tuple);
            }

            // �� ����ġ�� ���� ����
            SetTotalWeight();
            // ����ġ�� �л��� ���� ����
            SetVarianceAmount();
            // �־��� ����ġ�� ������� ġȯ
            SetPercentage();
            // ����ġ�� ������������ ����
            SortByWeight();
        }

        public RandomByWeight(float ratio, float weight, params T[] temps)
        {
            VarianceRatio = ratio;
            // ����ġ ���� ����Ʈ �߰�
            foreach (T temp in temps)
            {
                candidatesByWeight.Add((temp, weight));
            }

            // �� ����ġ�� ���� ����
            SetTotalWeight();
            // ����ġ�� �л��� ���� ����
            SetVarianceAmount();
            // �־��� ����ġ�� ������� ġȯ
            SetPercentage();
            // ����ġ�� ������������ ����
            SortByWeight();
        }


        /// <summary>
        /// ������ ��带 ���� ���� ���� �޾ƿ�
        /// </summary>
        public T GetRandom(RandomMode mode)
        {
            switch (mode)
            {
                case RandomMode.WeightVariance:
                    VarianceCount++;
                    return GetRandomWithVariance();
                default:
                    return default(T);
            }
        }

        /// <summary>
        /// ��� ������ ���� ���� ���� �� ���� ��ȯ (�ܼ� ���� ��ȯ)
        /// </summary>
        public T GetRandom()
        {
            // ���� �� ���� (0 ~ ����ġ �� ��)
            float rand = UnityEngine.Random.Range(0, TotalWeight);

            float add = 0;
            foreach ((T type, float weight) elem in candidatesByWeight)
            {
                add += elem.weight;

                if (rand <= add)
                {
                    return elem.type;
                }
            }
            return default(T);
        }

        /// <summary>
        /// ����ġ �л� ��带 �������� �� ���� ��ȯ (���� ��ȯ ���� �ڵ����� ����ġ�� ��ü �뷱���� ������)
        /// </summary>
        private T GetRandomWithVariance()
        {
            T t = default(T);
            // ���� �� ���� (0 ~ ����ġ �� ��)
            float rand = UnityEngine.Random.Range(0, TotalWeight);
            // �ݺ������� ������ ��
            float add = 0;
            // ���� ���� ������ ����
            bool find = false;
            // �ĺ��� ����Ʈ�� ��ȸ�Ͽ� ����ġ�� �����ϸ鼭 ���� �� �̻��϶� ����
            for (int i = 0; i < candidatesByWeight.Count; i++)
            {
                (T type, float weight) candidate = candidatesByWeight[i];
                add += candidate.weight;
                // ���� �������� �ʾҴµ� ����ġ ����ġ�� ���� ���� �� �̻��̸� ���� �� ���� ���� true
                if (!find && rand <= add)
                {
                    t = candidate.type;
                    // ���ù��� ����� ����ġ�� ����
                    candidate.weight -= mVarianceAmount;
                    find = true;
                }
                else
                {
                    // ���ù��� ���� ����� ����ġ�� ����.
                    candidate.weight += mVarianceAmount;
                }
                candidatesByWeight[i] = candidate;
            }
            #region ����ġ �� ���� ���� �� ����
            SetTotalWeight();
            SetPercentage();
            SortByWeight();
            #endregion
            return t;
        }

        /// <summary>
        /// �ĺ��� ����Ʈ�� ����ġ ���� ������������ ����
        /// </summary>
        private void SortByWeight()
        {
            candidatesByWeight = candidatesByWeight.OrderBy(x => x.Item2).ToList();
            candidatesByPercent = candidatesByPercent.OrderBy(x => x.Item2).ToList();
        }

        /// <summary>
        /// �ĺ����� ����ġ ����� ����Ʈ�� ����
        /// </summary>
        private void SetPercentage()
        {
            foreach ((T type, float weight) elem in candidatesByWeight)
            {
                candidatesByPercent.Add((elem.type, elem.weight / TotalWeight));
            }
        }

        /// <summary>
        /// �� ����ġ�� �ջ��ؼ� ���ϴ� �Լ�
        /// </summary>
        private void SetTotalWeight()
        {
            TotalWeight = 0;
            foreach ((T type, float weight) elem in candidatesByWeight)
            {
                TotalWeight += elem.weight;
            }
        }

        /// <summary>
        /// ����ġ�� �й��� ���� �����ϴ� �Լ�
        /// </summary>
        private void SetVarianceAmount()
        {
            double value = (TotalWeight * VarianceRatio) / (candidatesByWeight.Count - 1) * 100;
            value = Math.Truncate(value);
            mVarianceAmount = (float)value * 0.01f;
        }
    }
}