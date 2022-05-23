using System;
using System.Collections.Generic;
using Bogus;

namespace GeneratorCore
{
    public class Generator
    {
        Faker Faker;

        public Generator()
        {
            Faker = new Faker();
        }

        public Generator(string language)
        {
            Faker = new Faker(language);
        }

        public Bogus.Person GetPerson()
        {
            var person = Faker.Person;

            return person;
        }

        public Vehicle GetVehicle()
        {
            var vehicle = new Faker<Vehicle>()
                .RuleFor(v => v.Vin, Faker => Faker.Vehicle.Vin())
                .RuleFor(v => v.Manufacturer, Faker => Faker.Vehicle.Manufacturer())
                .RuleFor(v => v.Model, Faker => Faker.Vehicle.Model())
                .RuleFor(v => v.Type, Faker => Faker.Vehicle.Type())
                .RuleFor(v => v.Fuel, Faker => Faker.Vehicle.Fuel());

            return vehicle;
        }

        public List<string> GetImages()
        {

            var images = new List<string>();

            for (int i = 0; i < 30; i++)
            {
                images.Add(Faker.Image.PicsumUrl());
            }

            return images;
        }
    }


    public class Vehicle
    {
        public string Vin { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Fuel { get; set; }

        public override string ToString()
        {
            return $"{Vin} {Manufacturer} {Model} {Type} {Fuel}";
        }
    }
}
