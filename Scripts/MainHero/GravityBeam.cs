using UnityEngine;

public class GravityBeam : MonoBehaviour
{
    public Transform originPoint; // Точка, откуда будет выпущен луч
    public float raycastDistance = 10f; // Дальность луча
    [SerializeField] GameObject selectedObject;
    GameObject tempObj;
    public LineRenderer gunPointLine;
    bool drawLine =false ;
    Quaternion targetRotation;
    public float rotationSpeed = 1.0f;
    private void Start()
    {
        gunPointLine = GameObject.Find("GunPoint").GetComponent<LineRenderer>();
    }
    void Update()
    {
       

        // Проверяем нажатие кнопки мыши
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(originPoint.position, mousePosition - (Vector2)originPoint.position, raycastDistance);
            Debug.DrawRay(originPoint.position, (mousePosition - (Vector2)originPoint.position).normalized * raycastDistance, Color.red);
            if (hit.collider != null)
            {
                //Debug.Log("Object hit: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Block")){
                    //Debug.Log("Block");
                    selectedObject = hit.collider.gameObject;
                    tempObj = selectedObject;
                    selectedObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else
            {
                CancelGravityBeam();
                Debug.Log("No object hit");
            }
        }
        CreateGravityBeam();
    }


    void CreateGravityBeam()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            drawLine = true;
            
        }
        if (selectedObject != null&& Input.GetKeyDown(KeyCode.Q))
        {
            CancelGravityBeam();
        }
        if (selectedObject != null && drawLine)
        {
            selectedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            selectedObject.GetComponent<Rigidbody2D>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gunPointLine.SetPosition(0, originPoint.position);
            gunPointLine.SetPosition(1, selectedObject.transform.position);
        }
    }
    void CancelGravityBeam()
    {
        if(tempObj != null) { 
            drawLine = false;
            selectedObject = null;
            drawLine = false;
            gunPointLine.SetPosition(0, transform.position);
            gunPointLine.SetPosition(1, transform.position);
            tempObj.GetComponent<SpriteRenderer>().color = Color.white;
            tempObj.GetComponent<SpriteRenderer>().color = Color.white;
            tempObj.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    
}
