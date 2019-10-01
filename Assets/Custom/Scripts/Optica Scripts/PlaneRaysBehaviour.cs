using UnityEngine;


public class PlaneRaysBehaviour : MonoBehaviour
{
    public Material RayMaterial;

    private GameObject centerRay, topRay, bottomRay;
    private GameObject target, reflection;

    private const float RAY_WIDTH = 0.005f;


    public void Initiate(GameObject target, GameObject reflection)
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


    private GameObject CreateRay()
    {
        GameObject ray = new GameObject();
        LineRenderer renderer = ray.AddComponent<LineRenderer>();
        renderer.material = RayMaterial;
        renderer.startWidth = RAY_WIDTH;
        return ray;
    }

}
