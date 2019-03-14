
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
}
