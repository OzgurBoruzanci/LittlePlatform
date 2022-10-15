using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MaceControl : MonoBehaviour
{
    GameObject[] Destinations;
    bool GetTheDistanceOnce = true;
    bool BackAndForth = true;
    Vector3 DistanceBetween;
    int DistanceBetweenCounter = 0;
    int speed = 5;
    GameObject Character;
    RaycastHit2D ray;
    float ShootTime = 0;
    public LayerMask layermask;
    public Sprite BackSide;
    public Sprite FrontSide;
    public GameObject Bullet;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        Destinations = new GameObject[transform.childCount];
        Character = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < Destinations.Length; i++)
        {
            Destinations[i] = transform.GetChild(0).gameObject;
            Destinations[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        MaceSee();
        if (ray.collider.tag=="Player")
        {
            speed = 8;
            spriteRenderer.sprite = FrontSide;
            ShootIt();
        }
        else
        {
            speed = 4;
            spriteRenderer.sprite = BackSide;
        }
        GoToPoints();
    }

    void ShootIt()
    {
        ShootTime += Time.deltaTime;
        if (ShootTime > Random.Range(0.2f, 1))
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            ShootTime = 0;
        }
    }

    void MaceSee()
    {
        Vector3 CourseDirection = Character.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, CourseDirection, 1000, layermask);
    }

    void GoToPoints()
    {
        if (GetTheDistanceOnce)
        {
            DistanceBetween = (Destinations[DistanceBetweenCounter].transform.position - transform.position).normalized;
            GetTheDistanceOnce = false;
        }
        float distance = Vector3.Distance(transform.position, Destinations[DistanceBetweenCounter].transform.position);
        transform.position += DistanceBetween * Time.deltaTime * speed;
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
    public Vector2 BulletDirection()
    {
        return (Character.transform.position - transform.position).normalized;
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
[CustomEditor(typeof(MaceControl))]
[System.Serializable]
class MaceControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MaceControl script = (MaceControl)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("Produce", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject newObject = new GameObject();
            newObject.transform.parent = script.transform;
            newObject.transform.position = script.transform.position;
            newObject.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("FrontSide"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("BackSide"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Bullet"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif