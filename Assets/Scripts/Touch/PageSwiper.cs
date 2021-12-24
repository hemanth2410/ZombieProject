using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageSwiper : MonoBehaviour
{
    private Vector3 panelLocation;
    [SerializeField] private GameObject GalleryTreePrefab;
    [SerializeField] private TreeTypes[] treetype;
    private Vector3 xScreenWidth;
   

    
    private int totalPages;
    private int currentPage = 1;
    private void Awake()
    {
        xScreenWidth = new Vector3(Screen.width, 0, 0);
        
        for (int i = 0; i < treetype.Length; i++)
        {
           Instantiate(GalleryTreePrefab, transform.position + (xScreenWidth*i), Quaternion.identity,transform);
        }

        totalPages = treetype.Length;

        TouchEvents.OnSwipe += SwipeUI;
    }
    private void Start()
    {
        panelLocation = transform.position; 
    }
    private void Update()
    {
        Debug.Log(panelLocation);
        Debug.Log(currentPage);

        if (Input.GetKeyDown(KeyCode.D) && currentPage < totalPages)
        {
            currentPage++;
           
            Vector3 newLocation = panelLocation;
            newLocation += -xScreenWidth;
            transform.position = Vector3.Lerp(transform.position, newLocation, 0.3f);
            panelLocation = newLocation;


        }
        if (Input.GetKeyDown(KeyCode.A) && currentPage > 1)
        {
            currentPage--;
         
            Vector3 newLocation = panelLocation;
            newLocation += xScreenWidth;
            transform.position = Vector3.Lerp(transform.position, newLocation, 0.3f);
            panelLocation = newLocation;
        }


    }

    private void SwipeUI(SwipeData data)
    {
        if(data.Direction == SwipeDirection.Left && currentPage < totalPages)
        {
            currentPage++;
            Vector3 newLocation = panelLocation;
            newLocation += -xScreenWidth;
            transform.position =  Vector3.Lerp(transform.position, newLocation,0.3f);
            panelLocation = newLocation;
            
        }
        if (data.Direction == SwipeDirection.Right && currentPage > 1)
        {
            currentPage--;
            Vector3 newLocation = panelLocation;
            newLocation += xScreenWidth;
            transform.position = Vector3.Lerp(transform.position, newLocation, 0.3f);
            panelLocation = newLocation;
        }
    }

    




}
