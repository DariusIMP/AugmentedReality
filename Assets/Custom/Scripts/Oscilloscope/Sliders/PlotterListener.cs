namespace Custom.Scripts.Oscilloscope.Sliders
{
    /** Interface to handle modifications on the oscilloscope's plot. */
    public interface PlotterListener
    {
        void OnVerticalScaleVariation(float sliderValue, float variation);

        void OnPreciseVerticalScaleVariation(float sliderValue, float variation);
        
        void OnTimeBaseScaleVariation(float sliderValue, float variation);

        void OnTriggerVariation(float sliderValue, float variation);

        void OnHorizontalDisplacementVariation(float sliderValue, float variation);

        void OnVerticalDisplacementVariation(float sliderValue, float variation);
    }
}