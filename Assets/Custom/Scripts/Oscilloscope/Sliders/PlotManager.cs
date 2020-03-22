using System;
using System.Net;
using Custom.Scripts.Oscilloscope.Plotter;
using UnityEngine;
using UnityEngine.UI;


namespace Custom.Scripts.Oscilloscope.Sliders
{
    public enum SlidersType
    {
        VERTICAL_SCALE,
        TIMEBASE_SCALE,
        TRIGGER_VOLTAGE,
        VERTICAL_DISPLACEMENT,
        HORIZONTAL_DISPLACEMENT,
        SMOOTH_VERTICAL_SCALE
    }
    
    /**
     * Class to synchronize two or more sliders, if one changes, the other change equally
     * in the way set by the internal function SetSlider.
     *
     * This script must be placed alongside with the custom plotter.
     */
    public class PlotManager : MonoBehaviour
    {
        /** Sliders that will be synchronized. */
        public GameObject[] sliders;

        private CustomPlotter plotter;
        
        public void Start()
        {
            plotter = gameObject.GetComponent<CustomPlotter>();
        }

        /** Communicates to all the plot listeners a vertical scale variation event happened. */
        public void BroadcastVerticalScaleVariation(int sliderValue)
        {
            var variation = plotter.GetVerticalScale();
            foreach (var slider in sliders)
            {
                slider.GetComponent<PlotterListener>().OnVerticalScaleVariation(sliderValue, variation);
            }
        }

        public void BroadcastPreciseVerticalScaleVariation(float sliderValue)
        {
            var variation = plotter.GetSmoothVerticalScale();
            foreach (var slider in sliders)
            {
                slider.GetComponent<PlotterListener>().OnPreciseVerticalScaleVariation(sliderValue, variation);
            }
        }

        public void BroadcastTimeBaseScaleVariation(int sliderValue)
        {
            var variation = plotter.GetTimeBaseScale();
            foreach (var slider in sliders)
            {
                slider.GetComponent<PlotterListener>().OnTimeBaseScaleVariation(sliderValue, variation);
            }
        }

        public void BroadcastTriggerVariation(float sliderValue)
        {
            var variation = plotter.GetTriggerLevelVoltage();
            foreach (var slider in sliders)
            {
                slider.GetComponent<PlotterListener>().OnTriggerVariation(sliderValue, variation);
            }
        }

        public void BroadcastHorizontalDisplacementVariation(float sliderValue, int percentage)
        {    
            foreach (var slider in sliders)
            {
                slider.GetComponent<PlotterListener>().OnHorizontalDisplacementVariation(sliderValue, percentage);
            }
        }

        public void BroadcastVerticalDisplacementVariation(float sliderValue, int percentage)
        {    
            foreach (var slider in sliders)
            {
                slider.GetComponent<PlotterListener>().OnVerticalDisplacementVariation(sliderValue, percentage);
            }
        }
    }
}
