using System;
using UnityEngine;
using UnityEngine.UIElements;

public delegate void Then();

public class AnimatedTranslation
{
    private readonly Transform _object;
    private readonly Vector3 _to;
    private readonly float _speed;
    private bool _completed;
    private Vector3 _offset;

    public AnimatedTranslation(Transform obj, Vector3 to, float speed, Vector3 offset = default)
    {
        _object = obj;
        _to = to;
        _speed = speed;
        _offset = offset;
    }

    public void _reset()
    {
        _completed = false;
    }

    public bool isFinished()
    {
        return _completed;
    }

    protected void _execute(Then then)
    {
        if (!_completed)
        {
            _object.localPosition = Vector3.MoveTowards(_object.localPosition,
                _to + _offset, _speed * Time.deltaTime);
            if (_object.localPosition.Equals(_to + _offset))
            {
                _completed = true;
                then?.Invoke();
            }
        }
    }

    public virtual void Execute(Then then = null)
    {
        _execute(then);
    }

    public virtual void Reset()
    {
        _reset();
    }
}