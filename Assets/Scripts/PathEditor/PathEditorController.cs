using System.Collections.Generic;
using UnityEngine;

namespace PathEditor
{
    [ExecuteAlways]
    public class PathEditorController : MonoBehaviour
    {
        public List<Transform> transforms;
        public List<Vector3> waypointPositions; 

        private void OnDrawGizmos()
        {
            if (transforms == null || transforms.Count < 2)
                return;

            Gizmos.color = Color.cyan;

            for (int i = 0; i < transforms.Count - 1; i++)
            {
                if (transforms[i] != null && transforms[i + 1] != null)
                {
                    Gizmos.DrawLine(transforms[i].position, transforms[i + 1].position);
                    Gizmos.DrawSphere(transforms[i].position, 0.1f);
                }
            }

            // Draw last point sphere
            if (transforms[transforms.Count - 1] != null)
            {
                Gizmos.DrawSphere(transforms[transforms.Count - 1].position, 0.1f);
            }
        }

    }
}
