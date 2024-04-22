//using System.Collections.Generic;
//using Model.Runtime.Projectiles;
//using UnityEngine;
//using static UnityEngine.GraphicsBuffer;
//using System.Linq;
//using Utilities;
//using Model;
//using System;
//using UnityEngine.UIElements;

//namespace UnitBrains.Player
//{
//    public class SecondUnitBrain : DefaultPlayerUnitBrain
//    {
//        public override string TargetUnitName => "Cobra Commando";
//        private const float OverheatTemperature = 3f;
//        private const float OverheatCooldown = 2f;
//        private float _temperature = 0f;
//        private float _cooldownTime = 0f;
//        private bool _overheated;
//        private static int unitCounter = 0;
//        private int unitNumber;
//        private const int MaxTargetsToConsider = 3;

//        protected override void GenerateProjectiles(Vector2Int forTarget, List<BaseProjectile> intoList)
//        {
//            float overheatTemperature = OverheatTemperature;
//            ///////////////////////////////////////
//            // Homework 1.3 (1st block, 3rd module)
//            ///////////////////////////////////////           
//            if (GetTemperature() >= overheatTemperature)
//            {
//                return;
//            }

//            IncreaseTemperature();




//            if (IsTargetInRange(forTarget))
//            {
//                var projectile = CreateProjectile(forTarget);
//                AddProjectileToList(projectile, intoList);
//            }
//            ///////////////////////////////////////
//        }

//        protected override List<Vector2Int> SelectTargets()
//        {
//            ///////////////////////////////////////
//            // Homework 1.4 (1st block, 4rd module)
//            ///////////////////////////////////////
//            List<Vector2Int> allTargets = GetReachableTargets();
//            List<Vector2Int> result = new List<Vector2Int>();

//            Vector2Int Target = Vector2Int.zero;

//            int playerID = IsPlayerUnitBrain ? RuntimeModel.PlayerId : RuntimeModel.BotPlayerId;
//            Vector2Int ownBase = runtimeModel.RoMap.Bases[playerID];

//            int enemyPlayerID = 1 - playerID;
//            Vector2Int enemyBase = runtimeModel.RoMap.Bases[enemyPlayerID];

//            if (allTargets.Count == 0)
//            {
//                result.Add(enemyBase);
//            }
//            else
//            {
//                SortByDistanceToOwnBase(allTargets);
//                unitCounter++;
//                unitNumber = unitCounter;
//                int targetIndex = unitNumber % Math.Min(allTargets.Count, MaxTargetsToConsider);
//                if (IsTargetInRange(allTargets[targetIndex]))
//                {
//                    result.Add(allTargets[targetIndex]);
//                }
//            }

//            //Макс. значение расстояния
//            float minDistance = float.MaxValue;
//            ///////////////////////////
//            float maxProjectileRange = 10f;

//            foreach (Vector2Int i in GetAllTargets())
//            {
//                float distance = DistanceToOwnBase(i);
//                if (distance < minDistance)
//                {
//                    minDistance = distance;
//                    Target = i;
//                }
//                foreach (var target in GetAllTargets())
//                {
//                    allTargets.Add(target);
//                }
//                if (minDistance < float.MaxValue)
//                {
//                    if (IsTargetInRange(i))
//                    {
//                        new List<Vector2Int>().Add(i);
//                    }
//                    else
//                    {
//                        int palyerID = IsPlayerUnitBrain ? RuntimeModel.PlayerId : RuntimeModel.BotPlayerId;
//                        Vector2Int _enemyBase = runtimeModel.RoMap.Bases[palyerID];
//                    }
//                }
//            }

//            new List<Vector2Int>().Clear();
//            new List<Vector2Int>().Add(Target);
//            return result;
//        }

//        public override void Update(float deltaTime, float time)
//        {
//            if (_overheated)
//            {
//                _cooldownTime += Time.deltaTime;
//                float t = _cooldownTime / (OverheatCooldown / 10);
//                _temperature = Mathf.Lerp(OverheatTemperature, 0, t);
//                if (t >= 1)
//                {
//                    _cooldownTime = 0;
//                    _overheated = false;
//                }
//            }
//        }

//        private int GetTemperature()
//        {
//            if (_overheated) return (int)OverheatTemperature;
//            else return (int)_temperature;
//        }

//        private void IncreaseTemperature()
//        {
//            _temperature += 1f;
//            if (_temperature >= OverheatTemperature) _overheated = true;
//        }
//    }
//}