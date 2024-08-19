// Name: Scalable Object Controller.cs
// Author: Connor Larsen
// Date: 08/16/2024
// Description: 

using UnityEngine;

public class ScalableObjectController : MonoBehaviour
{
    #region Inspector Variables
    public enum ScaleType { GREEN, RED, NONE };
    [Header("Scale Variables")]
    public ScaleType scaleType;
    public float maxScaleSize;
    public float minScaleSize;
    #endregion

    #region Hidden Variables
    private GameManager gameManager;
    #endregion

    #region Functions
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

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

            // Play scaling up sound
            gameManager.PlaySoundEffect(gameManager.normalPlayer.GetComponent<AudioSource>(), gameManager.scalingUpSound);
            gameManager.PlaySoundEffect(gameManager.smallPlayer.GetComponent<AudioSource>(), gameManager.scalingUpSound);
        }
        else
        {
            // Play max scale sound
            gameManager.PlaySoundEffect(gameManager.normalPlayer.GetComponent<AudioSource>(), gameManager.scalingLimitMax);
            gameManager.PlaySoundEffect(gameManager.smallPlayer.GetComponent<AudioSource>(), gameManager.scalingLimitMax);
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

            // Play scaling down sound
            gameManager.PlaySoundEffect(gameManager.normalPlayer.GetComponent<AudioSource>(), gameManager.scalingDownSound);
            gameManager.PlaySoundEffect(gameManager.smallPlayer.GetComponent<AudioSource>(), gameManager.scalingDownSound);
        }
        else
        {
            // Play max scale sound
            gameManager.PlaySoundEffect(gameManager.normalPlayer.GetComponent<AudioSource>(), gameManager.scalingLimitMax);
            gameManager.PlaySoundEffect(gameManager.smallPlayer.GetComponent<AudioSource>(), gameManager.scalingLimitMax);
        }
    }
    #endregion
}