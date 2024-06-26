using UnityEngine;

public class ChangeCursor : Singleton<ChangeCursor>
{
    [SerializeField]
    private Texture2D defaultCursorTexture; 

    [SerializeField]
    private Texture2D clickedCursorTexture; 

    [SerializeField]
    private Vector2 hotSpot = Vector2.zero; 

    private void Start()
    {
        Cursor.SetCursor(defaultCursorTexture, hotSpot, CursorMode.Auto);
    }

    private void Update()
    {
    
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(clickedCursorTexture, hotSpot, CursorMode.Auto);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(defaultCursorTexture, hotSpot, CursorMode.Auto);
        }
    }
}
