using UnityEngine;

public class LockCursor : MonoBehaviour
{
    void Start()
    {
        Lock();
    }

    void Update()
    {
        // Press Esc to release the cursor for debugging
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Unlock();
        }
        // Re-lock with Left-Click
        if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked)
        {
            Lock();
        }
    }

    void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;   // Keeps it centred & inside the window
        Cursor.visible = false;                   // Hide cursor (remove this line if you still want to see it)
    }

    void Unlock()
    {
        Cursor.lockState = CursorLockMode.None;     // Frees the cursor
        Cursor.visible = true;
    }
}
