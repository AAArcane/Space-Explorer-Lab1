using UnityEngine;

/// <summary>
/// Calls the loader callback function once during the first update of the game.
/// </summary>
public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true; // Flag to check if this is the first update

    /// <summary>
    /// Updates once per frame. Calls the loader callback on the first update.
    /// </summary>
    private void Update()
    {
        if (isFirstUpdate) // Check if it's the first update
        {
            isFirstUpdate = false; // Set the flag to false for subsequent updates
            Loader.LoaderCallBack(); // Call the loader callback function
        }
    }
}