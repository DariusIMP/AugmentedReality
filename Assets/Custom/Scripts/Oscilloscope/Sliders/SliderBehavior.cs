using System;
using Custom.Scripts.Oscilloscope.Plotter;
using plib.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Oscilloscope.Sliders
{
    /**
     * Class to manage the behavior of the sliders in the oscilloscope scene.
     *
     * It is a plotter listener which will respond differently depending on the
     * value of {@param sliderType}, in order to be coordinated with other sliders
     * of the same type. This allows that a same parameter on the plot of the
     * oscilloscope to be modified from multiple sliders on the scene.
     *
     * It also communicates to the plot manager any change that might happen
     * on the slider, in order for it to broadcast the change and replicate it
     * to other sliders with the same type.
     */
    public class SliderBehavior : MonoBehaviour, PlotterListener
    {
        public Slider slider;
        public Text label;

        public SlidersType sliderType;
        public PlotManager plotManager;
        public CustomPlotter plotter;

        public void setSlider(float sliderValue)
        {
            int percentage;
            switch (sliderType)
            {
                case SlidersType.VERTICAL_SCALE:
                    plotter.VaryVerticalScale(sliderValue);
                    plotManager.BroadcastVerticalScaleVariation((int) sliderValue);
                    break;
                case SlidersType.HORIZONTAL_DISPLACEMENT:
                    plotter.VaryHorizontalDisplacement(sliderValue);
                    percentage = (int) (sliderValue / slider.maxValue * 100);
                    plotManager.BroadcastHorizontalDisplacementVariation(sliderValue, percentage);
                    break;
                case SlidersType.TIMEBASE_SCALE:
                    plotter.SetTimeBaseScale(sliderValue);
                    plotManager.BroadcastTimeBaseScaleVariation((int) sliderValue);
                    break;
                case SlidersType.TRIGGER_VOLTAGE:
                    plotter.VaryTriggerLevel(sliderValue);
                    plotManager.BroadcastTriggerVariation(sliderValue);
                    break;
                case SlidersType.VERTICAL_DISPLACEMENT:
                    plotter.VaryVerticalDisplacement(sliderValue);
                    percentage = (int) (sliderValue / slider.maxValue * 100);
                    plotManager.BroadcastVerticalDisplacementVariation(sliderValue, percentage);
                    break;
                case SlidersType.SMOOTH_VERTICAL_SCALE:
                    plotter.VaryVerticalScaleSmoothly(sliderValue);
                    plotManager.BroadcastPreciseVerticalScaleVariation(sliderValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /** Callback to be called when the plot's vertical scale has been modified. */
        public void OnVerticalScaleVariation(float sliderValue, float variation)
        {
            if (sliderType != SlidersType.VERTICAL_SCALE) return;
            slider.value = sliderValue;
            label.text = variation + "V";
        }

        /** Callback to be called when the plot's precise vertical scale has been modified. */
        public void OnPreciseVerticalScaleVariation(float sliderValue, float variation)
        {
            if (sliderType != SlidersType.SMOOTH_VERTICAL_SCALE) return;
            slider.value = sliderValue;
            label.text = "x" + Math.Round(variation, 2);
        }

        /** Callback to be called when the plot's time scale has been modified. */

        public void OnTimeBaseScaleVariation(float sliderValue, float variation)
        {
            if (sliderType != SlidersType.TIMEBASE_SCALE) return;
            slider.value = sliderValue;
            label.text = variation + "s"; 
        }

        /** Callback to be called when the plot's trigger position has been modified. */
        public void OnTriggerVariation(float sliderValue, float variation)
        {
            if (sliderType != SlidersType.TRIGGER_VOLTAGE) return;
            slider.value = sliderValue;
            label.text = variation + "V"; 
        }

        /** Callback to be called when the plot's horizontal displacement has been modified. */
        public void OnHorizontalDisplacementVariation(float sliderValue, float variation)
        {
            if (sliderType != SlidersType.HORIZONTAL_DISPLACEMENT) return;
            slider.value = sliderValue;
            label.text = variation + "%"; 
        }

        /** Callback to be called when the plot's precise vertical scale has been modified. */
        public void OnVerticalDisplacementVariation(float sliderValue, float variation)
        {
            if (sliderType != SlidersType.VERTICAL_DISPLACEMENT) return;
            slider.value = sliderValue;
            label.text = variation + "%"; 
        }
    }
}