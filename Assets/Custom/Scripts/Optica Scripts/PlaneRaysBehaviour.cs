using UnityEngine;


public class PlaneRaysBehaviour : RaysBehaviour
{

    private GameObject CenterRay, TopRay, BottomRay;
    private GameObject Target;
    private PlaneMirrorBehaviour Mirror;
    

    /*
     *  TODO: Refactor to implement abstract class RaysBehaviour
     * */
    public void Initialize(GameObject target, PlaneMirrorBehaviour mirror)
    {
        this.Target = target;
        this.Mirror = mirror;

        CenterRay = CreateRay("Center Ray");
        TopRay = CreateRay("Top Ray");
        BottomRay = CreateRay("Bottom Ray");

        PositionRays();
    }

    public void Update()
    {
        PositionRays();
    }

    public void PositionRays()
    {
        Vector3 reflectedPoint = 2 * Mirror.transform.position - Target.transform.position;
        CenterRay.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
            Target.transform.position,
            Mirror.transform.position,
            reflectedPoint
        });

        float yValue = 0.125f; // Target.transform.localScale.y / 2;
        Vector3 offset = new Vector3(0, yValue, 0);
        reflectedPoint = 2 * Mirror.transform.position - Target.transform.position + offset;
        TopRay.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
            Target.transform.position + offset,
            Mirror.transform.position + offset,
            reflectedPoint
        });

        yValue = -0.125f;
        offset = new Vector3(0, yValue, 0);
        reflectedPoint = 2 * Mirror.transform.position - Target.transform.position + offset;
        BottomRay.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
            Target.transform.position + offset,
            Mirror.transform.position + offset,
            reflectedPoint
        });
    }

}
