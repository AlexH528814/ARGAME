using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SetNavigationTarget : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField navigationInputField;

    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    private NavMeshPath path;
    private LineRenderer line;
    private Vector3 targetPosition = Vector3.zero;

    private bool lineToggle = false;

    
    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToggle;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineToggle && targetPosition != Vector3.zero)
        {
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            
        }
    }

    public void SetCurrentNavigationTarget(string enteredText)
    {
        targetPosition = Vector3.zero;
        enteredText = navigationInputField.text;
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.Equals(enteredText));
        if (currentTarget != null)
        {
            targetPosition = currentTarget.PositionObject.transform.position;

        }
    }

    public void ToggleVisibility()
    {
        lineToggle = !lineToggle;
        line.enabled = lineToggle;
    }
}
