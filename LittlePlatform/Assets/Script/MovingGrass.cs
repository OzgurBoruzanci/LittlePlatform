using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovingGrass : MonoBehaviour
{
    GameObject[] Destinations;
    bool GetTheDistanceOnce = true;
    bool BackAndForth = true;
    Vector3 DistanceBetween;
    int DistanceBetweenCounter = 0;
    void Start()
    {
        Destinations = new GameObject[transform.childCount];
        for (int i = 0; i < Destinations.Length; i++)
        {
            Destinations[i] = transform.GetChild(0).gameObject;
            Destinations[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        GoToPoints();
    }
    void GoToPoints()
    {
        if (GetTheDistanceOnce)
        {
            DistanceBetween = (Destinations[DistanceBetweenCounter].transform.position - transform.position).normalized;
            GetTheDistanceOnce = false;
        }
        float distance = Vector3.Distance(transform.position, Destinations[DistanceBetweenCounter].transform.position);
        transform.position += DistanceBetween * Time.deltaTime * 10;
        if (distance < 0.5f)
        {
            GetTheDistanceOnce = true;
            if (DistanceBetweenCounter == Destinations.Length - 1)
            {
                BackAndForth = false;
            }
            else if (DistanceBetweenCounter == 0)
            {
                BackAndForth = true;
            }


            if (BackAndForth)
            {
                DistanceBetweenCounter++;
            }
            else
            {
                DistanceBetweenCounter--;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);

        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(MovingGrass))]
[System.Serializable]
class MovingGrassEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MovingGrass script = (MovingGrass)target;
        if (GUILayout.Button("Produce", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject newObject = new GameObject();
            newObject.transform.parent = script.transform;
            newObject.transform.position = script.transform.position;
            newObject.name = script.transform.childCount.ToString();
        }
    }
}
#endif