using GraphPlotter;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.OsciloscopioScripts
{
    public class PlotController : MonoBehaviour
    {

        private RawImage _rawImage;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _rawImage = gameObject.GetComponent<RawImage>();
        }

        public void Plot(float variation)
        {
            Debug.Log(variation);
            MathPlotter.ColorFormula[] formulas = new[] { 
                new MathPlotter.ColorFormula(new Color(0, 1, 0.9130526f), "sin(" + variation + " * A)", 0)
            };

            float[] variables = new[] { 0f };		// just one variable, biggest index is 0, everything is ok

            _rawImage.texture = MathPlotter.PlotTexture(formulas, variables, new Vector2(-15, -15),	// plottet area min
                new Vector2(15, 15),					// plottet area max
                1024,									// Texture width  and hight
                Color.black,							// Background color
                Color.white,							// Axis color
                Color.gray,								// grid color
                Vector2.one);							// grid size, you can pass null, if you want no grid
        }
    }
}
