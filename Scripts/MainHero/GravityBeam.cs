using UnityEngine;

public class GravityBeam : MonoBehaviour
{
    public Transform originPoint; // Точка, откуда будет выпущен луч
    public float raycastDistance = 10f; // Дальность луча
    [SerializeField] GameObject selectedObject;
    GameObject tempObj;
    public LineRenderer gunPointLine;
    bool drawLine = false;
    public float rotationSpeed = 1.0f;
    public int segments = 10; // Количество сегментов линии
    public float lineWidth = 0.1f; // Ширина линии
    public float lineJitter = 0.1f; // Величина колебания линии

    private Vector3[] linePositions; // Позиции сегментов линии
    private Vector3[] lineSegments; // Сегменты линии


    float timer2RotateObject;
    float randomRotateZ;

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
            
                if (hit.collider.gameObject.CompareTag("Block"))
                {
                   
                    selectedObject = hit.collider.gameObject;
                    tempObj = selectedObject;
                    CreateBorber();
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            drawLine = true;

        }
        if (selectedObject != null && Input.GetKeyDown(KeyCode.Q))
        {
            CancelGravityBeam();
        }
        if (selectedObject != null && drawLine)
        {
            gunPointLine.enabled = true;
            selectedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            selectedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            selectedObject.GetComponent<Rigidbody2D>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Генерация ломающейся линии
            GenerateBrokenLine();
            
            gunPointLine.positionCount = segments;
            gunPointLine.SetPositions(linePositions);
            gunPointLine.startWidth = lineWidth;
            gunPointLine.endWidth = lineWidth;

            timer2RotateObject += Time.deltaTime;
            if (timer2RotateObject > 2f)
            {
                randomRotateZ = Random.Range(0, 180);
                timer2RotateObject = 0; 
                
            }
            selectedObject.transform.Rotate(new Vector3(0f, 0f, randomRotateZ * Time.deltaTime));
        }
        if (selectedObject != null && Input.GetKeyDown(KeyCode.F))
        {
            selectedObject.isStatic = true;
            CancelGravityBeam();
            tempObj.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void GenerateBrokenLine()
    {
        linePositions = new Vector3[segments];
        lineSegments = new Vector3[segments - 1];

        // Используем позицию выбранного объекта и положение указателя
        Vector3 startPos = originPoint.position;
        Vector3 endPos = selectedObject.transform.position;

        // Рассчитываем смещение между точками
        Vector3 lineOffset = (endPos - startPos) / segments;

        // Заполняем позиции сегментов линии с колебанием
        for (int i = 0; i < segments; i++)
        {
            linePositions[i] = startPos + lineOffset * i + Random.insideUnitSphere * lineJitter;
            if (i > 0)
            {
                lineSegments[i - 1] = linePositions[i] - linePositions[i - 1];
            }
        }

        // Выравниваем первую и последнюю точки с объектами
        linePositions[0] = startPos;
        linePositions[segments - 1] = endPos;
    }

    void CancelGravityBeam()
    {
        if (tempObj != null)
        {
            border.SetActive(false);
            drawLine = false;
            selectedObject = null;
            drawLine = false;
            gunPointLine.SetPosition(0, transform.position);
            gunPointLine.SetPosition(1, transform.position);
            gunPointLine.enabled = false;
            tempObj.GetComponent<SpriteRenderer>().color = Color.white;
            tempObj.GetComponent<SpriteRenderer>().color = Color.white;
            tempObj.GetComponent<Rigidbody2D>().gravityScale = 1;
            tempObj.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }




    [SerializeField] GameObject border;
    void CreateBorber()
    {
        border.SetActive(true);
        border.transform.SetParent(selectedObject.transform);
        SpriteRenderer targetSpriteRenderer = selectedObject.GetComponent<SpriteRenderer>();
        Vector2 targetSize = targetSpriteRenderer.bounds.size;
        
        border.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, -2f);
        border.transform.rotation = selectedObject.transform.rotation;
        
        border.transform.localScale = new Vector3(targetSize.x, targetSize.y, transform.localScale.z);
    }
   
}
