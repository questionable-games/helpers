using UnityEngine;

public struct ShootData
{
    public readonly Vector3 initialVelocity;
    public readonly float timeToTarget;

    public ShootData(Vector3 initialVelocity, float timeToTarget)
    {
        this.initialVelocity = initialVelocity;
        this.timeToTarget = timeToTarget;
    }
}
