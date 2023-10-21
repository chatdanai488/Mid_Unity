using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{

    private float _startingPos_x, //This is the starting position of the sprites.
            _lengthOfSprite_x; //This is the length of the sprites.
    private float _startingPos_y, //This is the starting position of the sprites.
            _lengthOfSprite_y; //This is the length of the sprites.
    public float AmountOfParallax; //This is amount of parallax scroll. 
    public Camera MainCamera; //Reference of the camera.
    // Start is called before the first frame update
    void Start()
    {
        //Getting the starting X position of sprite.
        _startingPos_x = transform.position.x;
        //Getting the length of the sprites.
        _lengthOfSprite_x = GetComponent<SpriteRenderer>().bounds.size.x;


        //Getting the starting X position of sprite.
        _startingPos_y = transform.position.y;
        //Getting the length of the sprites.
        _lengthOfSprite_y = GetComponent<SpriteRenderer>().bounds.size.y;
    }

 

    //Update is called once per frame
    private void Update()
    {
        //Vector3 Position = MainCamera.transform.position;
        //float Temp_x = Position.x * (1 - AmountOfParallax);
        //float Distance_x = Position.x * AmountOfParallax;

        //float Temp_y = Position.y * (1 - AmountOfParallax);
        //float Distance_y = Position.y * AmountOfParallax;

        //Vector3 NewPosition = new Vector3(_startingPos_x + Distance_x, _startingPos_y + Distance_y, transform.position.z);

        //transform.position = NewPosition;

        //if (Temp_x > _startingPos_x + (_lengthOfSprite_x / 2))
        //{
        //    _startingPos_x += _lengthOfSprite_x;
        //}
        //else if (Temp_x < _startingPos_x - (_lengthOfSprite_x / 2))
        //{
        //    _startingPos_x -= _lengthOfSprite_x;
        //}


        //if (Temp_y > _startingPos_y + (_lengthOfSprite_y / 2))
        //{
        //    _startingPos_y += _lengthOfSprite_y;
        //}
        //else if (Temp_y < _startingPos_y - (_lengthOfSprite_y / 2))
        //{
        //    _startingPos_y -= _lengthOfSprite_y;
        //}

        float temp = (MainCamera.transform.position.x + (1 - AmountOfParallax));
        float dist = (MainCamera.transform.position.x * AmountOfParallax);

        transform.position = new Vector3(_startingPos_x + dist, transform.position.y, transform.position.z);

        if(temp > _startingPos_x+_lengthOfSprite_x) { _startingPos_x += _lengthOfSprite_x; }
        else if( temp< _startingPos_x-_lengthOfSprite_x) { _startingPos_x -= _lengthOfSprite_x; }


    }

}
