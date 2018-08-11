using UnityEngine;

namespace PERSON
{
    public class PersonSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _personPrefab;

        public Person DroppingPerson { get; set; }
        
        private struct PersonData
        {
            public PersonType Type;
        }
        
        public void SpawnPerson(float x, float y)
        {
            var personData = DecideNextPerson();
            
            DroppingPerson = Instantiate(_personPrefab, new Vector3(x, y, 0), Quaternion.identity).GetComponentInChildren<Person>();
            DroppingPerson.Type = personData.Type;
        }

        private PersonData DecideNextPerson()
        {
            return new PersonData { Type = PersonType.GUY };
        }
    }
}