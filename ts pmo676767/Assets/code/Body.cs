using UnityEngine;

public class Body : MonoBehaviour 
{ 
    public bool photographed = false;
    public Sprite[] possibleSprites;
    void Awake() 
    { 
        BodyRegistry.allBodies.Add(this); 
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
            transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y+ 0.5f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }
}