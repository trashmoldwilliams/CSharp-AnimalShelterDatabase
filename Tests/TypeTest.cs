using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class TypeTest : IDisposable
  {
    public TypeTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_shelter_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_TypesEmptyAtFirst()
    {
      int result = Type.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Type firstType = new Type("Cat");
      Type secondType = new Type("Cat");

      //Assert
      Assert.Equal(firstType, secondType);
    }

    [Fact]
    public void Test_Save_SavesTypeToDataBase()
    {
      //Arrange
      Type testType = new Type("Dog");
      testType.Save();

      //Act
      List<Type> result = Type.GetAll();
      List<Type> testList = new List<Type>{testType};
      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdTypeObject()
    {
      Type testType = new Type("Dog");
      testType.Save();
      Type savedType = Type.GetAll()[0];

      int result = savedType.GetId();
      int testId = testType.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsTypeInDatabase()
    {
      Type testType = new Type("Cat");
      testType.Save();

      Type foundType = Type.Find(testType.GetId());

      Assert.Equal(testType, foundType);
    }

    [Fact]
    public void Test_GetAnimals_RetrieveAllAnimalsWithType()
    {
      Type testType = new Type("Lizzard");
      testType.Save();

      Animal firstAnimal = new Animal("Jynx", "Female", new DateTime(2017, 2, 18), "Calico", testType.GetId(), 1);
      firstAnimal.Save();

      Animal secondAnimal = new Animal("John", "Female", new DateTime(2017, 2, 18), "Calico", testType.GetId(), 2);
      secondAnimal.Save();

      List<Animal> testAnimalList = new List<Animal> {firstAnimal, secondAnimal};
      List<Animal> resultAnimalList = testType.GetAnimals();

      Assert.Equal(testAnimalList, resultAnimalList);
    }

    public void Dispose()
    {
      Type.DeleteAll();
      Animal.DeleteAll();
    }
  }
}
