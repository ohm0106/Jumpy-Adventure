using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StarUIController : MonoBehaviour
{
    [SerializeField]
    GameObject starPrefab; 

    [SerializeField]
    RectTransform starContainer; 

    [SerializeField]
    float starSpacing = 50f; 


    [SerializeField]
    Sprite activeStarSprite; 

    List<Star> stars = new List<Star>(); 

    int currentIndex;

    private void Start()
    {
        SetupStars();
    }

    void SetupStars()
    {
        GameObject[] starsInMap = GameObject.FindGameObjectsWithTag("Star");

        foreach (Transform child in starContainer)
        {
            Destroy(child.gameObject);
        }

        stars.Clear(); 

        for (int i = 0; i < starsInMap.Length; i++)
        {
            GameObject starUI = Instantiate(starPrefab, starContainer);
            RectTransform rectTransform = starUI.GetComponent<RectTransform>();

            float position = CalculatePosition(i, starsInMap.Length);
            rectTransform.anchoredPosition = new Vector2(position, 0); 

            Star starScript = starUI.GetComponent<Star>();
            stars.Add(starScript);
        }

        currentIndex = stars.Count - 1;
    }

    float CalculatePosition(int index, int totalCount)
    {
        float offset = starSpacing * ((totalCount - 1) / 2.0f);
        return index * starSpacing - offset;
    }

    public void ActivateStar()
    {
        if (currentIndex >= 0)
        {
            stars[currentIndex].SetIsActive(activeStarSprite);
            currentIndex--;
        }
    }
}

