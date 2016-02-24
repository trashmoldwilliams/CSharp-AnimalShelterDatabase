using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class AnimalTest : IDisposable
  {
    public AnimalTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_shelter_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      Animal firstAnimal = new Animal("Jynx", "Female", new DateTime(2016, 2, 18), "Calico", 1, 1);
      Animal secondAnimal = new Animal("Jynx", "Female", new DateTime(2016, 2, 18), "Calico", 1, 1);
      // Console.WriteLine(secondAnimal.GetDate());

      Assert.Equal(firstAnimal, secondAnimal);
    }
    [Fact]
    public void Test_Save()
    {
      Animal testAnimal = new Animal("Jynx", "Female", new DateTime(2016, 2, 18), "Calico", 1, 1);
      testAnimal.Save();


      List<Animal> testList = new List<Animal>{testAnimal};
      List<Animal> result = Animal.GetAll();

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      Animal testAnimal = new Animal("Jynx", "Female", new DateTime(2016, 2, 18), "Calico", 1, 1);
      testAnimal.Save();

      Animal savedAnimal = Animal.GetAll()[0];

      int result = savedAnimal.GetId();
      int testId = testAnimal.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindAnimalInDatabase()
    {
      Animal testAnimal = new Animal("Jynx", "Female", new DateTime(2017, 2, 18), "Calico", 1, 1);
      testAnimal.Save();

      Animal foundAnimal = Animal.Find(testAnimal.GetId());

      Assert.Equal(testAnimal, foundAnimal);
    }


    public void Dispose()
    {
      Type.DeleteAll();
      Animal.DeleteAll();
    }
  }
}
