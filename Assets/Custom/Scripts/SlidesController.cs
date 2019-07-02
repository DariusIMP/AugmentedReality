using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidesController : MonoBehaviour
{
    private List<ConstructionSlide> slides;
    private int currentIndex = 0;

    public GameObject nextButton;
    public GameObject previousButton;

    public GameObject SlabsLoads;
    public GameObject Momentums;
    public GameObject TransparentWalls;
    public GameObject Columns;
    public GameObject Slabs;
    public GameObject Walls;
    public GameObject Beams;
    public GameObject MiniBeams;
    public GameObject SlabsDistribution;


    // Start is called before the first frame update
    void Start()
    {
        slides = new List<ConstructionSlide>()
        {
            new ConstructionSlide("Edificio", Columns, Slabs, Walls),
            new ConstructionSlide("Estructura", Columns, Beams),
            new ConstructionSlide("Carga de losas", Columns, Slabs, SlabsLoads, Beams),
            new ConstructionSlide("Distribución de cargas", Columns, Slabs, Beams, SlabsDistribution),
            new ConstructionSlide("Distribución sobre columnas", TransparentWalls),
            new ConstructionSlide("Momentos", Columns, Momentums, MiniBeams)
        };

        foreach (ConstructionSlide slide in slides)
            slide.Hide();

        slides[0].Show();

        previousButton.SetActive(false);
    }

    public void NextSlide()
    {
        if (currentIndex + 1 < slides.Count)
        {
            slides[currentIndex].Hide();
            slides[++currentIndex].Show();

            if (currentIndex == slides.Count - 1)
                nextButton.SetActive(false);
            previousButton.SetActive(true);
        }
    }

    public void PreviousSlide()
    {
        if (currentIndex > 0)
        {
            slides[currentIndex].Hide();
            slides[--currentIndex].Show();

            if (currentIndex == 0)
                previousButton.SetActive(false);
            nextButton.SetActive(true);
        }
    }
}
