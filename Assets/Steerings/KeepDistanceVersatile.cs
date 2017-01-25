using UnityEngine;
using System.Collections;

namespace Steerings
{

    public class KeepDistanceVersatile : SteeringBehaviour
    {
        public GameObject target;
        public float requiredDistance;
        public static float alpha = 125f;
        public static GameObject surrogateTarget = null;

        public override SteeringOutput GetSteering()
        {

            // no KS? get it
            if (this.ownKS == null) this.ownKS = GetComponent<KinematicState>();

            return KeepDistance.GetSteering(this.ownKS, this.target, this.requiredDistance);
        }

        public static SteeringOutput GetSteering(KinematicState ownKS, GameObject target, float requieredDistance)
        {

            Vector3 directionFromTarget = (ownKS.position - target.transform.position).normalized;
            Vector3 desiredPosition = target.transform.position + directionFromTarget * requieredDistance;
            desiredPosition = Quaternion.Euler(0, 0, alpha) * desiredPosition;
       

            if (KeepDistance.surrogateTarget == null)
            {
                surrogateTarget = new GameObject("surrogateee");
            }
            surrogateTarget.transform.position = desiredPosition;

            return Seek.GetSteering(ownKS, surrogateTarget);
        }

        public static float GetAngleBetween(Vector2 v1, Vector2 v2)
        {
            float AngleRad = Mathf.Atan2(v1.y - v2.y, v1.x - v2.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            return AngleDeg;
        }
    }
}
