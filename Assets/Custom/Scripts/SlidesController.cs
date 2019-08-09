using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidesController : MonoBehaviour
{
    private List<ConstructionSlide> slides;
    private int currentIndex;

    public GameObject nextButton;
    public GameObject previousButton;

    public GameObject SlabsLoads;
    public GameObject Momentums;
    public GameObject TransparentWalls;
    public GameObject Columns;
    public GameObject MiniColumns;
    public GameObject Slabs;
    public GameObject Walls;
    public GameObject Beams;
    public GameObject MiniBeams;
    public GameObject SlabsDistribution;

    public GameObject TitleObject;


    // Start is called before the first frame update
    void Start()
    {
        slides = new List<ConstructionSlide>()
        {
            new ConstructionSlide("Estructura y muros", Columns, Slabs, Walls, Beams),
            new ConstructionSlide("Vigas y columnas", Columns, Beams),
            new ConstructionSlide("Carga en losas", Columns, Slabs, SlabsLoads, Beams),
            new ConstructionSlide("Método de los trapecios", Columns, Slabs, Beams, SlabsDistribution),
            new ConstructionSlide("Cargas en vigas", TransparentWalls, MiniColumns, Beams),
            new ConstructionSlide("Diagramas de momentos", Columns, Momentums, MiniBeams)
        };

        foreach (ConstructionSlide slide in slides)
            slide.Hide();

        currentIndex = 0;
        slides[currentIndex].Show();

        previousButton.SetActive(false);

        TitleObject.GetComponent<Text>().text = slides[currentIndex].Title;
    }

    public void NextSlide()
    {
        if (currentIndex + 1 < slides.Count)
        {
            // Slides management
            slides[currentIndex].Hide();
            slides[++currentIndex].Show();

            // Arrows management
            if (currentIndex == slides.Count - 1)
                nextButton.SetActive(false);
            previousButton.SetActive(true);

            // Title management
            TitleObject.GetComponent<Text>().text = slides[currentIndex].Title;
        }
    }

    public void PreviousSlide()
    {
        if (currentIndex > 0)
        {
            // Slides management
            slides[currentIndex].Hide();
            slides[--currentIndex].Show();

            // Arrows management
            if (currentIndex == 0)
                previousButton.SetActive(false);
            nextButton.SetActive(true);

            // Title management
            TitleObject.GetComponent<Text>().text = slides[currentIndex].Title;
        }
    }
}
