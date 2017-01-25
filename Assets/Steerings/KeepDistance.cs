using UnityEngine;
using System.Collections;

namespace Steerings
{

	public class KeepDistance : SteeringBehaviour
	{
        public GameObject target;
        public float requiredDistance;
		public static  GameObject surrogateTarget = null;
	
		public override SteeringOutput GetSteering () {

			// no KS? get it
			if (this.ownKS==null) this.ownKS = GetComponent<KinematicState>();
			
			return KeepDistance.GetSteering (this.ownKS, this.target, this.requiredDistance);
		}

		public static SteeringOutput GetSteering (KinematicState ownKS, GameObject target, float requieredDistance) {

            Vector3 directionFromTarget = (ownKS.position - target.transform.position).normalized;
            Vector3 desiredPosition = target.transform.position + directionFromTarget * requieredDistance;


            if (KeepDistance.surrogateTarget == null)
            {
                surrogateTarget = new GameObject("surrogateee");
            }
            surrogateTarget.transform.position = desiredPosition;

            return Seek.GetSteering (ownKS, surrogateTarget);
		}
	}
}
