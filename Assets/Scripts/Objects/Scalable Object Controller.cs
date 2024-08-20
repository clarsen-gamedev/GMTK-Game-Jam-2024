// Name: Scalable Object Controller.cs
// Author: Connor Larsen
// Date: 08/16/2024
// Description: 

using Unity.Burst.CompilerServices;
using UnityEngine;

public class ScalableObjectController : MonoBehaviour
{
    #region Inspector Variables
    public enum ScaleType { GREEN, RED, NONE };
    [Header("Scale Variables")]
    public ScaleType scaleType;
    public float maxScaleSize;
    public float minScaleSize;

    [Header("Audio Source")]
    public AudioSource audioSource;
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public AudioClip hit4;
    public AudioClip hit5;
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

    #region Collision Functions
    private void OnCollisionEnter()
    {
        int num = Random.Range(1, 5);

        if (num == 1)
        {
            GetComponent<AudioSource>().clip = hit1;
        }
        else if (num == 2)
        {
            GetComponent<AudioSource>().clip = hit2;
        }
        else if (num == 3)
        {
            GetComponent<AudioSource>().clip = hit3;
        }
        else if (num == 4)
        {
            GetComponent<AudioSource>().clip = hit4;
        }
        else if (num == 5)
        {
            GetComponent<AudioSource>().clip = hit5;
        }

        GetComponent<AudioSource>().Play();
    }
    #endregion
}