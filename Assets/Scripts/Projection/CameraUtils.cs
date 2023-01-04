using UnityEngine;

namespace Utils
{
    public static class CameraUtils
    {
        public static void DrawFrustum(Camera cam, Color color)
        {
            Vector3[] nearCorners = new Vector3[4]; //Approx'd nearplane corners
            Vector3[] farCorners = new Vector3[4]; //Approx'd farplane corners
            Plane[] camPlanes = GeometryUtility.CalculateFrustumPlanes(cam); //get planes from matrix
            Plane temp = camPlanes[1]; camPlanes[1] = camPlanes[2]; camPlanes[2] = temp; //swap [1] and [2] so the order is better for the loop

            for (int i = 0; i < 4; i++)
            {
                nearCorners[i] = Plane3Intersect(camPlanes[4], camPlanes[i], camPlanes[(i + 1) % 4]); //near corners on the created projection matrix
                farCorners[i] = Plane3Intersect(camPlanes[5], camPlanes[i], camPlanes[(i + 1) % 4]); //far corners on the created projection matrix
            }

            for (int i = 0; i < 4; i++)
            {
                Debug.DrawLine(nearCorners[i], nearCorners[(i + 1) % 4], color, Time.deltaTime, true); //near corners on the created projection matrix
                Debug.DrawLine(farCorners[i], farCorners[(i + 1) % 4], color, Time.deltaTime, true); //far corners on the created projection matrix
                Debug.DrawLine(nearCorners[i], farCorners[i], color, Time.deltaTime, true); //sides of the created projection matrix
            }
        }

        /// <returns>The intersection points of p1, p2, and p3</returns>
        private static Vector3 Plane3Intersect(Plane p1, Plane p2, Plane p3)
        {
            return ((-p1.distance * Vector3.Cross(p2.normal, p3.normal)) +
                    (-p2.distance * Vector3.Cross(p3.normal, p1.normal)) +
                    (-p3.distance * Vector3.Cross(p1.normal, p2.normal))) /
                (Vector3.Dot(p1.normal, Vector3.Cross(p2.normal, p3.normal)));
        }
    }
}