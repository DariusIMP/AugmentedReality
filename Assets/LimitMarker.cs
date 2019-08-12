using UnityEngine;
using UnityEngine.UI;


public class LimitMarker : MonoBehaviour
{

    public Text SpeedText;
    public Text DistanceText;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetSpeed(double v)
    {
        int rounded = (int)(v * 100);
        SpeedText.text = string.Format("{0}m/s", (rounded/100.0).ToString("0.##"));
    }
    
    public void SetDistance(double d)
    {
        DistanceText.text = string.Format("{0}m", (int)d);
    }
}
