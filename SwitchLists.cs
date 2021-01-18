using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwitchLists : MonoBehaviour
{
    public GameObject organisationsParent;
    public GameObject imagesParent;

    private List<GameObject> organisations = new List<GameObject>();
    private List<GameObject> images = new List<GameObject>();

    private GameObject currentlyViewedOrganisation;
    private GameObject currentlyViewedImage;

    void Start()
    {
        foreach (Transform item in organisationsParent.transform)
        {
            organisations.Add(item.gameObject);
        }

        foreach (Transform item in imagesParent.transform)
        {
            images.Add(item.gameObject);
        }

        currentlyViewedOrganisation = organisations.First();
        currentlyViewedImage = images.First();
    }

    // Elements - currently displayed image and organisation
    public void SwitchToNext()
    {
        SetCurrentElementsActiveState(false);

        SetElementsToNext();

        SetCurrentElementsActiveState(true);
    }

    public void SwitchToPrevious()
    {
        SetCurrentElementsActiveState(false);

        SetElementsToPrevious();

        SetCurrentElementsActiveState(true);
    }

    private void SetElementsToNext()
    {
        currentlyViewedOrganisation = organisations.SkipWhile(x => x != currentlyViewedOrganisation)
            .Skip(1)
            .DefaultIfEmpty(organisations[0])
            .FirstOrDefault();

        currentlyViewedImage = images.SkipWhile(x => x != currentlyViewedImage)
            .Skip(1)
            .DefaultIfEmpty(images[0])
            .FirstOrDefault();
    }

    private void SetElementsToPrevious()
    {
        currentlyViewedOrganisation = organisations.TakeWhile(x => x != currentlyViewedOrganisation)
            .DefaultIfEmpty(organisations[organisations.Count - 1])
            .LastOrDefault();

        currentlyViewedImage = images.TakeWhile(x => x != currentlyViewedImage)
            .DefaultIfEmpty(images[images.Count - 1])
            .LastOrDefault();
    }

    private void SetCurrentElementsActiveState(bool stateToSet)
    {
        currentlyViewedOrganisation.SetActive(stateToSet);
        currentlyViewedImage.SetActive(stateToSet);
    }
}