
using UnityEngine;

public class Kinematic
{

    /// <summary>
    /// Predicts the collision time of two objects moving to the right.
    /// This comes from <code> S_b = S_a + h </code>, where
    /// <code> 
    /// S = u * t + (a * t^2) / 2 
    /// </code>. 
    /// Substituting S in the previous
    /// equation we get the equation used to solve the problem
    /// in this method:
    /// <code>
    /// t^2 * (a_b - a_a) + t * (2 * u_b - 2 * u_a) - 2 * h = 0
    /// </code>
    /// This code is based on the following video:
    /// https://www.youtube.com/watch?v=v1V3T5BPd7E
    /// </summary>
    /// <returns>The collision time.</returns>
    /// <param name="x_a">X position a (right object).</param>
    /// <param name="x_b">X position b (left object).</param>
    /// <param name="a_a">Acceleration of object A.</param>
    /// <param name="a_b">Acceleration of object B.</param>
    /// <param name="u_a">Initial velocity of object A.</param>
    /// <param name="u_b">Initial velocity of object B.</param>
    public static float PredictCollisionTime(float x_a, float x_b,
                                       float a_a, float a_b,
                                       float u_a, float u_b)
    {
        float h = x_a - x_b;

        float coeff_a = a_b - a_a;
        float coeff_b = 2 * (u_b - u_a);
        float coeff_c = -2 * h;

        return (-coeff_b + Mathf.Sqrt(coeff_b * coeff_b - 4 * coeff_a * coeff_c))
                 / (2 * coeff_a);
    }

    /// <summary>
    /// Calculates the params needed to shoot the source object to the
    /// target one.
    /// </summary>
    /// <returns><code>ShootData</code> object containing Initial velocity and 
    /// Time to target.</returns>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    /// <param name="gravity">Gravity.</param>
    /// <param name="delta_h">The maximum height of the shoot starting from
    /// target y postion.</param>
    public static ShootData CalculateShoot(Transform source, Transform target,
                                           float gravity, float delta_h)
    {
        // Calculate highest vertical point of the shoot
        // Must be greater than target.position.y
        float h = target.position.y + delta_h;

        float displacementX = target.position.x - source.position.x;
        float displacementY = target.position.y - source.position.y;
        float displacementZ = target.position.z - source.position.z;

        float timeToTarget = Mathf.Sqrt(-2 * h / gravity) +
                     Mathf.Sqrt(2 * (displacementY - h) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);

        // Calculate X and Z together as the equation is the same for both
        Vector3 displacementXZ = new Vector3(displacementX,
                                             0,
                                             displacementZ);
        Vector3 velocityXZ = displacementXZ / timeToTarget;

        // Sum vectors to get the three components together into a single vector
        // make sure Y velocity is always against the gravity 
        Vector3 initialVelocity = velocityXZ + velocityY * -Mathf.Sign(gravity);

        return new ShootData(initialVelocity, timeToTarget);
    }

    /// <summary>
    /// Draws the path of the shoot from source object current position to
    /// target object current position.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    /// <param name="gravity">Gravity.</param>
    /// <param name="delta_h">The maximum height of the shoot starting from
    /// target y postion.</param>
    public static void DrawPath(Transform source, Transform target, 
                                float gravity, float delta_h)
    {
        ShootData shootData = Kinematic.CalculateShoot(source,
                                                       target,
                                                       gravity,
                                                       delta_h);
        Vector3 previousDrawPoint = source.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = (i / (float)resolution) * shootData.timeToTarget;
            Vector3 displacement = shootData.initialVelocity * simulationTime
                                   + Vector3.up * gravity
                                   * simulationTime * simulationTime /
                                   2f;
            Vector3 drawPoint = source.position + displacement;

            // TODO Find a way to draw trajectory not only in debug mode (to be
            //      able to visualizing it in the actual scene, not only in Scene tab)
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }
}
