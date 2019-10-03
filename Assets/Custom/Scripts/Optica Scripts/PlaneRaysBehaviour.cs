using UnityEngine;


public class PlaneRaysBehaviour : RaysBehaviour
{

    private GameObject centerRay, topRay, bottomRay;
    private GameObject target, reflection;
    

    /*
     *  TODO: Refactor to implement abstract class RaysBehaviour
     * */
    public void Initialize(GameObject target, GameObject reflection)
    {
        float distance = reflection.transform.position.z - target.transform.position.z;
        this.target = target;
        this.reflection = reflection;
        Vector3 reflectedPoint;

        centerRay = CreateRay();
        float yValue = 0;
        reflectedPoint = new Vector3(0, yValue, 2 * distance);
        centerRay.GetComponent<LineRenderer>().SetPositions(new Vector3[]{
            new Vector3(0, 0, 0), reflectedPoint
        });

        topRay = CreateRay();
        yValue = reflection.transform.localScale.y / 2;
        reflectedPoint = new Vector3(0, yValue, 2 * distance);
        topRay.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
            new Vector3(0, yValue, 0), reflectedPoint
        });

        bottomRay = CreateRay();
        yValue = - reflection.transform.localScale.y / 2;
        reflectedPoint = new Vector3(0, yValue, 2 * distance);
        bottomRay.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
            new Vector3(0, yValue, 0), reflectedPoint
        });
    }

    public void Update()
    {
        LineRenderer renderer = centerRay.GetComponent<LineRenderer>();
        renderer.SetPosition(0, target.transform.position);
        renderer.SetPosition(1, reflection.transform.position);

        float yValue = reflection.transform.localScale.y / 2;
        renderer = topRay.GetComponent<LineRenderer>();
        renderer.SetPosition(0, target.transform.position + new Vector3(0, yValue, 0));
        renderer.SetPosition(1, reflection.transform.position + new Vector3(0, yValue, 0));

        yValue = - reflection.transform.localScale.y / 2;
        renderer = bottomRay.GetComponent<LineRenderer>();
        renderer.SetPosition(0, target.transform.position + new Vector3(0, yValue, 0));
        renderer.SetPosition(1, reflection.transform.position + new Vector3(0, yValue, 0));
    }

}
