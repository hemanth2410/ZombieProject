using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper01 : MonoBehaviour,IDragHandler,IEndDragHandler
{
    private Vector3 panelLocation;
    [SerializeField] private float percentageThreshold = 0.2f;
    [SerializeField] private float easing = 0.5f;

    private int totalPages = 3;  // number of treePanels
    private int currentPage = 1;
    void Start()
    {
        panelLocation = transform.position;
    }


    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float Percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if(Mathf.Abs(Percentage) >= percentageThreshold)
        {
            Vector3 newLocation = panelLocation;
            if(Percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if(Percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;

        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startPos,Vector3 endPos,float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(1, 0, t));
            yield return null;
        }
    }
}
