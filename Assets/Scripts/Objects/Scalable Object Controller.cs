// Name: Scalable Object Controller.cs
// Author: Connor Larsen
// Date: 08/16/2024
// Description: 

using UnityEngine;

public class ScalableObjectController : MonoBehaviour
{
    #region Public and Serialized Variables
    public enum ScaleType { GREEN, RED, NONE };
    [Header("Scale Variables")]
    public ScaleType scaleType;
    public float maxScaleSize;
    public float minScaleSize;
    #endregion

    #region Functions
    public void GrowObject()
    {
        // Scale the object up
        Vector3 newScale = gameObject.transform.localScale;

        if (newScale.x <= 1)
        {
            newScale.x = newScale.x * 2f;
            newScale.y = newScale.y * 2f;
            newScale.z = newScale.z * 2f;
            
        }
        else
        {
            newScale += new Vector3(1, 1, 1);
        }
        
        // If the new scale is outside the size bounds, don't scale
        if (newScale.x <= maxScaleSize)
        {
            gameObject.transform.localScale = newScale;
        }
    }

    public void ShrinkObject()
    {
        // Scale the object up
        Vector3 newScale = gameObject.transform.localScale;

        if (newScale.x <= 1)
        {
            newScale.x = newScale.x * 0.5f;
            newScale.y = newScale.y * 0.5f;
            newScale.z = newScale.z * 0.5f;
        }
        else
        {
            newScale -= new Vector3(1, 1, 1);
        }

        // If the new scale is outside the size bounds, don't scale
        if (newScale.x >= minScaleSize)
        {
            gameObject.transform.localScale = newScale;
        }
    }
    #endregion
}