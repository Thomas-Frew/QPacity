using System;

namespace QPacity
{
    /// <summary>
    /// A custom object to store guests' data
    /// </summary>
    public class Guest
    {
        // The guest's name
        public string Name { get; set; }

        // The guest's phone number
        public string PhoneNumber { get; set; }

        // A function to equate whether a passed guest value is equal to this object
        public bool Equals(Guest value)
        {
            // If all properties of the passed value are the same, the two objects are equal
            return (value.Name == Name && value.PhoneNumber == PhoneNumber);
        }
    }
}
