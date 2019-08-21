using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidesController : MonoBehaviour
{

    public List<GameObject> Slides;

    public GameObject nextButton;
    public GameObject previousButton;

    public GameObject TitleObject;


    private int currentIndex;
    private bool isPlaying;


    void Start()
    {
        foreach (GameObject slide in Slides)
            slide.GetComponent<ConstructionSlide>().Hide();

        isPlaying = false;
        currentIndex = 0;
        ConstructionSlide currentSlide = Slides[currentIndex].GetComponent<ConstructionSlide>();
        currentSlide.Show();

        previousButton.GetComponent<Button>().interactable = false;

        TitleObject.GetComponent<Text>().text = currentSlide.Title;
    }

    public void NextSlide()
    {
        if (currentIndex + 1 < Slides.Count)
        {
            // Slides management
            Slides[currentIndex].GetComponent<ConstructionSlide>().Hide();
            Slides[currentIndex].GetComponent<ConstructionSlide>().Stop();
            ConstructionSlide currentSlide = Slides[++currentIndex].GetComponent<ConstructionSlide>();
            currentSlide.Show();

            // Arrows management
            if (currentIndex == Slides.Count - 1)
                nextButton.GetComponent<Button>().interactable = false;
            previousButton.GetComponent<Button>().interactable = true;

            // Title management
            TitleObject.GetComponent<Text>().text = currentSlide.Title;
        }
    }

    public void PreviousSlide()
    {
        if (currentIndex > 0)
        {
            // Slides management
            Slides[currentIndex].GetComponent<ConstructionSlide>().Hide();
            Slides[currentIndex].GetComponent<ConstructionSlide>().Stop();
            ConstructionSlide currentSlide = Slides[--currentIndex].GetComponent<ConstructionSlide>();
            currentSlide.Show();

            // Arrows management
            if (currentIndex == 0)
                previousButton.GetComponent<Button>().interactable = false;
            nextButton.GetComponent<Button>().interactable = true;

            // Title management
            TitleObject.GetComponent<Text>().text = currentSlide.Title;
        }
    }

    public void TogglePlay()
    {
        if (isPlaying)
        {
            Slides[currentIndex].GetComponent<ConstructionSlide>().Stop();
        }
        else
        {
            Slides[currentIndex].GetComponent<ConstructionSlide>().Play();
        }

        isPlaying = !isPlaying;
    }
}
