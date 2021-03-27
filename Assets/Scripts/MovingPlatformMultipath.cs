using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatformMultipath : MonoBehaviour
{
    public List<Transform> path;
    public float speed;

    void Start()
    {
        path.Add(transform);

        var sequence = DOTween.Sequence().SetLoops(-1);
        foreach (var point in path)
        {
            sequence.Append(transform.DOMove(point.position, speed).SetEase(Ease.Linear).SetSpeedBased().SetDelay(1f));
        }
    }
}
