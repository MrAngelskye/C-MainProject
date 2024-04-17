using Model.Runtime.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnitBrains.Player;
using UnityEngine;

public class ThirdUnitBrain : DefaultPlayerUnitBrain
{
    public override string TargetUnitName => "Ironclad Behemoth";

    private bool _isAttacking = false;
    private bool _isMoving = false;
    private float _transitionTimer = 0f;
    private const float TransitionTime = 1f;
    protected override void GenerateProjectiles(Vector2Int forTarget, List<BaseProjectile> intoList)
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _transitionTimer = TransitionTime;
        }

        if (_isAttacking && _transitionTimer <= 0f)
        {
            if (IsTargetInRange(forTarget))
            {
                var projectile = CreateProjectile(forTarget);
                AddProjectileToList(projectile, intoList);
            }
        }
    }
    protected override List<Vector2Int> SelectTargets()
    {
        if (!_isMoving)
        {
            _isMoving = true;
            _transitionTimer = TransitionTime;
        }

        return new List<Vector2Int>(); 
    }
    public override void Update(float deltaTime, float time)
    {
        if (_transitionTimer > 0f)
        {
            _transitionTimer -= deltaTime;
            if (_transitionTimer <= 0f)
            {
                _isMoving = !_isMoving;
                _isAttacking = !_isAttacking;
            }
        }

        base.Update(deltaTime, time);
    }
}
