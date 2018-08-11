using System;
using MISC;
using UnityEngine;

namespace PERSON
{
    public class Elevator : MonoBehaviour
    {
        private PersonSpawner _personSpawner;
        
        private bool _shouldSpawnPerson;

        private enum GridStates
        {
            Empty,
            RightPersonBottom,
            RightPersonTop,
            LeftPersonBottom,
            LeftPersonTop, 
            UpPersonBottom,
            UpPersonTop,
            DownPersonBottom,
            DownPersonTop
        }
        
        private GridStates[,] _people;

        private int _droppingX, _droppingY,
                    _droppingX2, _droppingY2;

        private int _lastDroppingX, _lastDroppingY;
        
        private void Awake()
        {
            _personSpawner = GetComponentInChildren<PersonSpawner>();
            _people = new GridStates[16, 16];
        }

        private void Start()
        {
            _shouldSpawnPerson = true;
            InvokeRepeating(nameof(DropPerson), 1, 1);
        }
        
        private void Update()
        {
            for (int i = 0; i < _people.GetLength(0); i++)
            {
 
            }
            
            if (_shouldSpawnPerson)
            {
                _shouldSpawnPerson = false;
                _droppingX = _people.GetLength(0) / 2;
                _droppingX2 = _people.GetLength(0) / 2;
                _lastDroppingX = _droppingX;
                _droppingY = 0;
                _droppingY2 = 1;
                _lastDroppingY = _droppingY;
                _personSpawner.SpawnPerson(0, 3.73f);
                _people[_droppingX, _droppingY] = GridStates.UpPersonTop;
                _people[_droppingX2, _droppingY2] = GridStates.UpPersonBottom;
            }

            RotatePerson();
        }

        private void RotatePerson()
        {
            var right = Input.GetKeyDown(KeyCode.D);
            var left = Input.GetKeyDown(KeyCode.A);

            if (right)
            {
                if (_people[_droppingX, _droppingY] == GridStates.UpPersonBottom)
                {
                    _people[_droppingX, _droppingY - 1] = GridStates.Empty;
                    _people[_droppingX, _droppingY] = GridStates.RightPersonBottom;
                    _people[_droppingX + 1, _droppingY] = GridStates.RightPersonTop;
                    _personSpawner.DroppingPerson.transform.Rotate(0, 0, -90);
                }
                else if (_people[_droppingX, _droppingY] == GridStates.RightPersonBottom)
                {
                    _people[_droppingX + 1, _droppingY] = GridStates.Empty;
                    _people[_droppingX, _droppingY] = GridStates.DownPersonBottom;
                    _people[_droppingX, _droppingY + 1] = GridStates.DownPersonTop;
                    _personSpawner.DroppingPerson.transform.Rotate(0, 0, -90);
                }
                else if (_people[_droppingX, _droppingY] == GridStates.DownPersonBottom)
                {
                    _people[_droppingX, _droppingY + 1] = GridStates.Empty;
                    _people[_droppingX, _droppingY] = GridStates.LeftPersonBottom;
                    _people[_droppingX - 1, _droppingY] = GridStates.LeftPersonTop;
                    _personSpawner.DroppingPerson.transform.Rotate(0, 0, -90);
                }
                else if (_people[_droppingX, _droppingY] == GridStates.LeftPersonBottom)
                {
                    _people[_droppingX - 1, _droppingY] = GridStates.Empty;
                    _people[_droppingX, _droppingY] = GridStates.UpPersonBottom;
                    _people[_droppingX, _droppingY - 1] = GridStates.UpPersonTop;
                    _personSpawner.DroppingPerson.transform.Rotate(0, 0, -90);
                }
            }
            else if (left)
            {
                if (_people[_droppingX, _droppingY] == GridStates.UpPersonBottom)
                {
                    _people[_droppingX, _droppingY - 1] = GridStates.Empty;
                    _people[_droppingX, _droppingY] = GridStates.LeftPersonBottom;
                    _people[_droppingX - 1, _droppingY] = GridStates.LeftPersonTop;
                    _personSpawner.DroppingPerson.transform.Rotate(0, 0, 90);
                }
            }
        }
        
        private void DropPerson()
        {
            if (_droppingX >= _people.GetLength(0) || _droppingY >= _people.GetLength(1) || _droppingX < 0 || _droppingY < 0) return;
            Debug.Log($"dropX: {_droppingX}");
            Debug.Log($"dropY: {_droppingY}");

            for (int i = 0; i < _people.GetLength(0); i++)
            {
                for (int j = 0; j < _people.GetLength(1); j++)
                {
                   if (_people[i, j] != GridStates.Empty)
                       Debug.Log($"grid space [{i}, {j}] contains {_people[i, j]}");
                }
            }
            switch (_people[_droppingX, _droppingY])
            {
                    case GridStates.UpPersonBottom:
                        if (_droppingY + 1 > _people.GetLength(1)) return;
                        if (_people[_droppingX, _droppingY + 1] != GridStates.Empty)
                        {
                            _shouldSpawnPerson = true;
                             return;
                        }
                        
                        _people[_droppingX, _droppingY - 1] = GridStates.Empty;
                        _people[_droppingX, _droppingY] = GridStates.UpPersonTop;
                        _people[_droppingX, _droppingY + 1] = GridStates.UpPersonBottom;
                        break;
                    case GridStates.DownPersonBottom:
                        if (_droppingY + 2 > _people.GetLength(1)) return;
                        if (_people[_droppingX, _droppingY + 2] != GridStates.Empty)
                        {
                            _shouldSpawnPerson = true;
                             return;
                        }
                        
                        _people[_droppingX, _droppingY] = GridStates.Empty;
                        _people[_droppingX, _droppingY + 1] = GridStates.DownPersonBottom;
                        _people[_droppingX, _droppingY + 1] = GridStates.DownPersonTop;
                        break;
                    case GridStates.LeftPersonBottom:
                        _people[_droppingX, _droppingY] = GridStates.Empty;
                        _people[_droppingX - 1, _droppingY] = GridStates.Empty;
                        _people[_droppingX, _droppingY + 1] = GridStates.LeftPersonBottom;
                        _people[_droppingX - 1, _droppingY + 1] = GridStates.LeftPersonTop;
                        break;
                    case GridStates.RightPersonBottom:
                        _people[_droppingX, _droppingY] = GridStates.Empty;
                        _people[_droppingX + 1, _droppingY] = GridStates.Empty;
                        _people[_droppingX, _droppingY + 1] = GridStates.RightPersonBottom;
                        _people[_droppingX + 1, _droppingY + 1] = GridStates.RightPersonTop;
                        break;

                    default:
                        Debug.Log(_people[_droppingX, _droppingY]);
                        break;
            }
            
            if (_droppingX != _lastDroppingX)
                _personSpawner.DroppingPerson.transform.Translate(-.56f, 0, 0);
            if (_droppingY != _lastDroppingY)
                _personSpawner.DroppingPerson.transform.Translate(0, -.56f, 0);
            //var pos = _personSpawner.DroppingPerson.transform.position;
            //pos.x = _droppingX * -.16f;
            //pos.y = _droppingY * - .16f;
            //_personSpawner.DroppingPerson.transform.position = pos;
            Debug.Log($"xpos: {_personSpawner.DroppingPerson.transform.position.x}");

            _lastDroppingX = _droppingX;
            _lastDroppingY = _droppingY;
            
            _droppingY += 1;
            _droppingY2 += 1;
        }    
    }
}