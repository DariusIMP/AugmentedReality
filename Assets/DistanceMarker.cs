using UnityEngine;
using UnityEngine.UI;

public class DistanceMarker : MonoBehaviour
{

    public Text uiText;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    
    public void SetDistance(double d)
    {
        uiText.text = string.Format("{0}m", (int)d);
    }
}
