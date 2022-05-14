using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class RadialFormation : FormationBase {
    public int _amount = 0;
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _radiusGrowthMultiplier = 0;
    [SerializeField] private float _rotations = 1;
    [SerializeField] private int _rings = 1;
    [SerializeField] private float _ringOffset = 1;
    [SerializeField] private float _nthOffset = 0;

    private int m_numberOfIteration;

    public override IEnumerable<Vector3> EvaluatePoints() {
        if (_amount == 0) _rings = 1;
        else
        {
            _rings = (int)(_amount * 0.1f + 1);
        }
        int amountPerRing = _amount / _rings;
        var ringOffset = 0f;
        for (var i = 0; i < _rings; i++){

            if (i == _rings - _amount % _rings) amountPerRing++;

            for (var j = 0; j < amountPerRing; j++) {
                var angle = j * Mathf.PI * (2 * _rotations) / amountPerRing + (i % 2 != 0 ? _nthOffset : 0);

                var radius = _radius + ringOffset + j * _radiusGrowthMultiplier;
                var x = Mathf.Cos(angle) * radius;
                var z = Mathf.Sin(angle) * radius;

                var pos = new Vector3(x, 0, z);

                pos += GetNoise(pos);

                pos *= Spread;

                m_numberOfIteration++;
                Debug.Log($"nombre d'itération: {m_numberOfIteration}");

                yield return pos;
            }
            ringOffset += _ringOffset;
        }
        
        
        m_numberOfIteration = 0;
    }
}