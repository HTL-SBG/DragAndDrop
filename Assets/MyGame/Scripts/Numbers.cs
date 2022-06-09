using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Numbers : MonoBehaviour, IDragHandler
{
    //assigned in Inspector
    public int representingNumber;
    public bool plainNumber;
    public bool dragable;

    //assigned at runtime in Awake
    private SoNumberImages imagesPointSet;

    //assigned at runtime in Start
    private RectTransform myDragRectTransform;
    private Vector3 origPos; 
    private Canvas myParentCanvas;
    private GameObject shownNbrText;

    private void Awake()
    {
        imagesPointSet = Resources.Load<SoNumberImages>("PointSet");
    }

    private void Start()
    {
        myDragRectTransform = GetComponent<RectTransform>();
        origPos = gameObject.transform.position;

        //Get the parent Canvas Obj, for dragging mechanics - needed for scalefactor in differenct screen spaces! 
        GameObject tempCanvasItem = gameObject; //start with gameobject
        while (tempCanvasItem.GetComponent<Canvas>() == null)
        {
            tempCanvasItem = tempCanvasItem.transform.parent.gameObject;
        }
        myParentCanvas = tempCanvasItem.GetComponent<Canvas>();

        //Add Collider via Script
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().size = gameObject.GetComponent<RectTransform>().sizeDelta;

        //create acces to gameobject with Textmesh pro component, inclusive when it is inactive
        shownNbrText = gameObject.GetComponentInChildren<TMP_Text>(true).gameObject;

        SetupNumberRepresentation();
    }

    public void SetupNumberRepresentation()
    {
        if (!plainNumber)
        {
            gameObject.GetComponent<Image>().sprite = imagesPointSet.setPoints[representingNumber];
            if (shownNbrText != null)
            {
                shownNbrText.SetActive(false);
            }
        }
    }

    public void Reset(GameObject parent)
    {
        gameObject.transform.position = origPos;
        gameObject.transform.parent = parent.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragable)
        {
            myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor;
        }
    }
}
