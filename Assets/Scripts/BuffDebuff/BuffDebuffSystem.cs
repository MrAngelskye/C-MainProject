using Model.Runtime.ReadOnly;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class BuffDebuffSystem : MonoBehaviour
{
    private Dictionary<IReadOnlyUnit, List<BuffDebuff>> _unitBuffs = new();

    private void Awake()
    {
        ServiceLocator.Register(this);
        StartCoroutine(BuffDebuffCoroutine());
    }

    public void AddBuff(IReadOnlyUnit unit, BuffDebuff buff)
    {
        if (!_unitBuffs.ContainsKey(unit))
        {
            _unitBuffs[unit] = new List<BuffDebuff>();
        }
        _unitBuffs[unit].Add(buff);
    }

    public void RemoveBuff(IReadOnlyUnit unit, BuffDebuff buff)
    {
        if (_unitBuffs.ContainsKey(unit))
        {
            _unitBuffs[unit].Remove(buff);
        }
    }

    public BuffModifiers GetModifiers(IReadOnlyUnit unit)
    {
        BuffModifiers modifiers = new BuffModifiers();
        if (_unitBuffs.ContainsKey(unit))
        {
            foreach (var buff in _unitBuffs[unit])
            {
                modifiers.MoveSpeedMultiplier *= buff.MoveSpeedModifier;
                modifiers.AttackSpeedMultiplier *= buff.AttackSpeedModifier;
            }
        }
        return modifiers;
    }

    private IEnumerator BuffDebuffCoroutine()
    {
        while (true)
        {
            float deltaTime = Time.deltaTime;
            foreach (var unitBuffsPair in _unitBuffs)
            {
                for (int i = unitBuffsPair.Value.Count - 1; i >= 0; i--)
                {
                    unitBuffsPair.Value[i].Duration -= deltaTime;
                    if (unitBuffsPair.Value[i].Duration <= 0)
                    {
                        unitBuffsPair.Value.RemoveAt(i);
                    }
                }
            }
            yield return null;
        }
    }
}
