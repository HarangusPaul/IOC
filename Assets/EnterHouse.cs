using System.Collections.Generic;
using UnityEngine;


public class EnterHouse: MonoBehaviour
{
    public GameObject actionTitleMap;
    public GameObject triggerObject;
    public GameObject insides;
    public float proximityThreshold = 2f;
    public bool reverse = false;

    
    private Transform child;
    private int closeIndex = -1;
    private Vector3 first = new Vector3(0,0,0);
    
    
    private List<Transform> childrenList = new List<Transform>();
    private List<Transform> insideChildrenList = new List<Transform>();
    
    private void Start()
    {
        for (int i = 0; i < triggerObject.transform.childCount; i++)
        {
            Transform childTransform = triggerObject.transform.GetChild(i);
            childrenList.Add(childTransform);
        }
        for (int i = 0; i < insides.transform.childCount; i++)
        {
            Transform childTransform = insides.transform.GetChild(i);
            insideChildrenList.Add(childTransform);
        }
    }
    
    private void Update()
    {
        if (IsCloseToAnyChild())
        {
            // If the taskTitleMap is within proximity, show it
            if (actionTitleMap != null)
            {
                actionTitleMap.SetActive(true);
            }

            // Check for clicks while the taskTitleMap is active
            if (Input.GetMouseButtonDown(0)) // Left mouse button or tap
            {
                HandleClick();
            }
        }
        else
        {
            // If the taskTitleMap is too far, hide it
            if (actionTitleMap != null)
            {
                actionTitleMap.SetActive(false);
            }
        }
    }

    private bool IsCloseToAnyChild()
    {
        // Get the position of the taskTitleMap (this GameObject)
        Vector2 taskTitleMapPosition = transform.position;

        int i=0;
        // Loop through the children stored in the list
        foreach (Transform childTransform in childrenList)
        {
            // Get the position of the current child
            Vector2 childPosition = childTransform.position;

            // Calculate the distance between the taskTitleMap and this child object
            float distance = Vector2.Distance(taskTitleMapPosition, childPosition);
            
            // If any child is within the proximity threshold, return true
            if (distance <= proximityThreshold)
            {
                child = childTransform;
                closeIndex = i;
                return true;
            }

            i++;
        }

        // If none of the children are close enough, return false
        return false;
    }
    private void HandleClick()
    {
        if (closeIndex != -1)
        {
            if (first == new Vector3(0,0,0))
            {
                first = transform.position;
                transform.position = insideChildrenList[closeIndex].position;
            }
            else
            {
                Debug.Log(first);
                Vector3 pos = first;
                first = new Vector3(0,0,0);
                transform.position = pos;
            }
        }
    }
}
