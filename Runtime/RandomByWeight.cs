using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


namespace MyUtil.Random
{
    /// <summary>
    /// 랜덤 반환시 사용 할 모드
    /// </summary>
    public enum RandomMode
    {
        WeightVariance // 가중치 분산 모드
    }

    public class RandomByWeight<T>
    {
        /// <summary>
        /// 가중치 값으로 분류된 후보군 리스트
        /// </summary>
        private List<(T, float)> candidatesByWeight = new();
        /// <summary>
        /// 백분율 값(0.0 ~ 1.0)으로 분류된 후보군 리스트
        /// </summary>
        private List<(T, float)> candidatesByPercent = new();

        /// <summary>
        /// 총 가중치의 합
        /// </summary>
        public float TotalWeight { get; set; }
        [field: Range(0f, 1f)]
        /// <summary>
        /// 가중치 증감 배율
        /// </summary>
        public float VarianceRatio { get; set; } = 0.01f;

        /// <summary>
        /// 가중치 분산 횟수
        /// </summary>
        public int VarianceCount { get; private set; }

        private float mVarianceAmount;


        /// <summary>
        /// 튜플 배열 인자를 통해 초기 설정하는 생성자
        /// </summary>
        /// <param name="tuples"></param>
        public RandomByWeight(float ratio, params (T, float)[] tuples)
        {

            VarianceRatio = ratio;
            // 가중치 정보 리스트 추가
            foreach ((T, float) tuple in tuples)
            {
                candidatesByWeight.Add(tuple);
            }

            // 총 가중치의 합을 설정
            SetTotalWeight();
            // 가중치를 분산할 값을 설정
            SetVarianceAmount();
            // 주어진 가중치를 백분율로 치환
            SetPercentage();
            // 가중치의 오름차순으로 정렬
            SortByWeight();
        }

        public RandomByWeight(float ratio, float weight, params T[] temps)
        {
            VarianceRatio = ratio;
            // 가중치 정보 리스트 추가
            foreach (T temp in temps)
            {
                candidatesByWeight.Add((temp, weight));
            }

            // 총 가중치의 합을 설정
            SetTotalWeight();
            // 가중치를 분산할 값을 설정
            SetVarianceAmount();
            // 주어진 가중치를 백분율로 치환
            SetPercentage();
            // 가중치의 오름차순으로 정렬
            SortByWeight();
        }


        /// <summary>
        /// 정해진 모드를 통해 랜덤 값을 받아옴
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
        /// 모드 설정을 따로 하지 않을 때 랜덤 반환 (단순 랜덤 반환)
        /// </summary>
        public T GetRandom()
        {
            // 랜덤 값 설정 (0 ~ 가중치 총 합)
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
        /// 가중치 분산 모드를 선택했을 때 랜덤 반환 (랜덤 반환 이후 자동으로 가중치의 전체 밸런스를 조절함)
        /// </summary>
        private T GetRandomWithVariance()
        {
            T t = default(T);
            // 랜덤 값 설정 (0 ~ 가중치 총 합)
            float rand = UnityEngine.Random.Range(0, TotalWeight);
            // 반복문에서 가산할 값
            float add = 0;
            // 랜덤 값이 결정된 여부
            bool find = false;
            // 후보군 리스트를 순회하여 가중치를 가산하면서 랜덤 값 이상일때 선택
            for (int i = 0; i < candidatesByWeight.Count; i++)
            {
                (T type, float weight) candidate = candidatesByWeight[i];
                add += candidate.weight;
                // 값이 결정되지 않았는데 가중치 가산치가 랜덤 선택 값 이상이면 선택 후 결정 여부 true
                if (!find && rand <= add)
                {
                    t = candidate.type;
                    // 선택받은 대상은 가중치를 감소
                    candidate.weight -= mVarianceAmount;
                    find = true;
                }
                else
                {
                    // 선택받지 못한 대상은 가중치를 증가.
                    candidate.weight += mVarianceAmount;
                }
                candidatesByWeight[i] = candidate;
            }
            #region 가중치 값 관련 갱신 및 정렬
            SetTotalWeight();
            SetPercentage();
            SortByWeight();
            #endregion
            return t;
        }

        /// <summary>
        /// 후보군 리스트를 가중치 값의 오름차순으로 정렬
        /// </summary>
        private void SortByWeight()
        {
            candidatesByWeight = candidatesByWeight.OrderBy(x => x.Item2).ToList();
            candidatesByPercent = candidatesByPercent.OrderBy(x => x.Item2).ToList();
        }

        /// <summary>
        /// 후보군의 가중치 백분율 리스트를 갱신
        /// </summary>
        private void SetPercentage()
        {
            foreach ((T type, float weight) elem in candidatesByWeight)
            {
                candidatesByPercent.Add((elem.type, elem.weight / TotalWeight));
            }
        }

        /// <summary>
        /// 총 가중치를 합산해서 구하는 함수
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
        /// 가중치를 분배할 값을 설정하는 함수
        /// </summary>
        private void SetVarianceAmount()
        {
            double value = (TotalWeight * VarianceRatio) / (candidatesByWeight.Count - 1) * 100;
            value = Math.Truncate(value);
            mVarianceAmount = (float)value * 0.01f;
        }
    }
}