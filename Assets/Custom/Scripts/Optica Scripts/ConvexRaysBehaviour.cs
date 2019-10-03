using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexRaysBehaviour : RaysBehaviour
{

    public GameObject VirtualImage;

    private GameObject parallelRay, centerRay, focalRay;
    private GameObject Target;
    private ConvexMirrorBehaviour Mirror;


    public void Initialize(GameObject target, ConvexMirrorBehaviour mirror)
    {
        this.Target = target;
        this.Mirror = mirror;

        parallelRay = CreateRay();
        centerRay = CreateRay(2);
        focalRay = CreateRay();

        PositionRaysAndImage();        
    }

    private void PositionRaysAndImage()
    {
        // We will only show rays of the top point

        // We will asume the bottom point of 'target' is over the axis
        // We call 'targetPoint' to the point belonging to the target from which we will show the rays
        //Vector3 targetPoint = new Vector3(0, Target.transform.localScale.y, 0);
        float targetHeight = 0.25f;
        Vector3 targetPoint = new Vector3(0, Mirror.transform.position.y + targetHeight, 0);

        // Here we calculate where a ray parallel to the axis meets the mirror
        Vector3 mirrorCenter = Mirror.GetCenter();
        float mirrorRadius = mirrorCenter.z - Mirror.transform.position.z;
        Vector3 parallelHit = mirrorCenter;
        parallelHit += new Vector3(0, targetHeight, -mirrorRadius * Mathf.Cos(Mathf.Asin(targetHeight / mirrorRadius)));

        parallelRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, parallelHit, Mirror.GetFocalPoint() }
        );

        centerRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, mirrorCenter }
        );

        // Here we calculate the intersection between the rays so as to place the virtual image
        Vector3 intersection = CalculateIntersection(
            parallelHit, Mirror.GetFocalPoint(), targetPoint, mirrorCenter
        );
        float virtualImgHeight = intersection.y - mirrorCenter.y;

        // And now we calculate the points for the ray projecting to the focal point
        Vector3 focalHit = GetCircleIntersection(mirrorRadius, mirrorCenter, targetPoint, Mirror.GetFocalPoint());
        focalRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, focalHit, intersection }
        );

        VirtualImage.transform.position = new Vector3(0, mirrorCenter.y + virtualImgHeight / 2, intersection.z);
        VirtualImage.transform.localScale = new Vector3(virtualImgHeight, virtualImgHeight, virtualImgHeight);
    }

    /**
     *  Calculates intersection between given circle and given segment (start, finish)
     *  We assume everything is contained on plane x=0
     */ 
    private Vector3 GetCircleIntersection(float radius, Vector3 center, Vector3 start, Vector3 finish)
    {
        Vector3 p1 = start - center;
        Vector3 p2 = finish - center;

        // Defining the segment with the equation `y = m * x + b`, we calculate m and b
        float m = (p2.y - p1.y) / (p2.z - p1.z);
        float b = p1.y - m * p1.z;

        // Knowing that the equation for a circle is r^2 = x^2 + y^2, we calculate the intersection
        float m2 = m * m, b2 = b * b;
        float intersectionZ = (-2 * m * b - Mathf.Sqrt(4 * m2 * b2 - 4 * (m2 + 1) * (b2 - radius * radius))) / (2 * (m2 + 1));
        float intersectionY = m * intersectionZ + b;

        return new Vector3(0, intersectionY, intersectionZ) + center;
    }

}
