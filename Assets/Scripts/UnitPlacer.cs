using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPlacer : MonoBehaviour
{
    public static UnitPlacer instance;

    public LayerMask groundLayerMask;

    private GameObject unitPrefab;
    private GameObject toPlace;

    private Camera _mainCamera;
    private bool isPlacingUnit = false;

    private bool clickedUnit;
    private bool preparingUnit;

    public Vector3 mousePos;

    private void Awake()
    {
        instance = this;
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isPlacingUnit)
        {
            HandleInput();
            if(preparingUnit)
            toPlace.transform.position = mousePos;
        }
      
    }
    public void SetUnitPrefab(GameObject _prefab)
    {
        unitPrefab = _prefab;
        isPlacingUnit = true;
        clickedUnit = true;
    }

    private void HandleInput()
    {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, groundLayerMask)) 
            {
                if (toPlace == null && unitPrefab != null) 
                {
                    clickedUnit = true;
                    _PrepareUnit(hit.point);
                }
            }
        
        if (clickedUnit)
        {
            Ray ray2 = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 1000f, groundLayerMask))
            {
                mousePos = hit2.point;
                toPlace.transform.position = mousePos;
            }
        }
        if (Input.GetMouseButtonDown(0) && toPlace != null) 
        {
            UnitValidate validator = toPlace.GetComponent<UnitValidate>();
            preparingUnit = false;
            if (validator != null && validator.hasValidPlacement) 
            {
                validator.SetPlacementMode(PlacementMode.Fixed); 
                isPlacingUnit = false;
                validator.setUnitOnGround = true;
                toPlace = null;
                unitPrefab = null;
             
            }
            else
            {
                Destroy(toPlace);
                isPlacingUnit = false;
                toPlace = null;
                unitPrefab = null;
            }
        }
    }

    private void _PrepareUnit(Vector3 position)
    {
        toPlace = Instantiate(unitPrefab, position, Quaternion.identity); // Tworzenie obiektu do umieszczenia na podstawie prefabrykatu jednostki
        preparingUnit = true;
        UnitValidate validator = toPlace.GetComponent<UnitValidate>(); // Pobranie komponentu odpowiedzialnego za walidacjê jednostki
        if (validator != null)
        {
            validator.hasValidPlacement = true; // Ustawienie flagi prawid³owego umieszczenia na true
            validator.SetPlacementMode(PlacementMode.Valid); // Ustawienie trybu umieszczenia na prawid³owy
        }
    }
}
