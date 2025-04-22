using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private Vector3 targetWaypoint;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float waitTime = .3f;
        [SerializeField] private float rotationSpeed = 7;

        public void Set(List<Vector3> path)
        {
            StartCoroutine(FollowPath(path));
        }
        
        private IEnumerator FollowPath(List<Vector3> waypoints)
        {
            transform.position = waypoints[0];
            int targetWaypointIndex = 1;
            targetWaypoint = waypoints[targetWaypointIndex];

            while (targetWaypointIndex != waypoints.Count)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
                if (transform.position == targetWaypoint && targetWaypointIndex != waypoints.Count - 1)
                {
                    targetWaypointIndex = targetWaypointIndex + 1;
                    targetWaypoint = waypoints[targetWaypointIndex];

                    yield return new WaitForSeconds(waitTime);
                }

                yield return null;
            }
        }
        
        private void Update()
        {
            //rotation
            Vector3 diff = targetWaypoint - transform.position;
            diff.Normalize();
            float targetAngle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
}