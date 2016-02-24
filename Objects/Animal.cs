using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter
{
  public class Animal
  {
    private int _id;
    private string _name;
    private string _gender;
    private string _breed;
    private int _type_id;
    private DateTime _date_of_admittance;

  public Animal(string Name, string Gender, DateTime Date_Of_Admittance, string Breed, int Type_Id, int Id = 0 )
  {
    _id = Id;
    _name = Name;
    _gender = Gender;
    _date_of_admittance = Date_Of_Admittance;
    _breed = Breed;
    _type_id = Type_Id;
  }

  public int GetId()
  {
    return _id;
  }

  public string GetName()
  {
    return _name;
  }

  public string GetGender()
  {
    return _gender;
  }

  public DateTime GetDate()
  {
    return _date_of_admittance;
  }

  public string GetBreed()
  {
    return _breed;
  }

  public int GetTypeId()
  {
    return _type_id;
  }

  public override bool Equals(System.Object otherAnimal)
  {
    if (!(otherAnimal is Animal)) {
      return false;
    }
    else{
      Animal newAnimal = (Animal) otherAnimal;
      bool idEquality = this.GetId() == newAnimal.GetId();
      bool nameEquality = this.GetName() == newAnimal.GetName();
      bool genderEquality = this.GetGender() == newAnimal.GetGender();
      bool dateEquality = this.GetDate() == newAnimal.GetDate();
      bool breedEquality = this.GetBreed() == newAnimal.GetBreed();
      bool typeIdEquality = this.GetTypeId() == newAnimal.GetTypeId();

      return (idEquality && nameEquality && genderEquality && dateEquality && breedEquality && typeIdEquality);
    }
  }

  public static List<Animal> GetAll()
  {
    List<Animal> AllAnimals = new List<Animal>{};

    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM animal;", conn);
    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int AnimalId = rdr.GetInt32(0);
      string AnimalName = rdr.GetString(1);
      string AnimalGender = rdr.GetString(2);
      string AnimalBreed = rdr.GetString(3);
      int AnimalTypeId = rdr.GetInt32(4);
      DateTime AnimalDateOfAdmittance = rdr.GetDateTime(5);
      Animal newAnimal = new Animal(AnimalName, AnimalGender, AnimalDateOfAdmittance, AnimalBreed, AnimalTypeId, AnimalId);
      AllAnimals.Add(newAnimal);
    }

    if (rdr != null) {
      rdr.Close();
    }
    if (conn != null) {
      conn.Close();
    }
    return AllAnimals;
  }

  public void Save()
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr;
    conn.Open();

    SqlCommand cmd = new SqlCommand("INSERT INTO animal (name, gender, breed, type_id, date_of_admittance) OUTPUT INSERTED.id VALUES(@AnimalName, @AnimalGender, @AnimalBreed, @AnimalTypeId, @AnimalDateOfAdmittance);", conn);

    SqlParameter nameParameter = new SqlParameter();
    nameParameter.ParameterName = "@AnimalName";
    nameParameter.Value = this.GetName();

    SqlParameter genderParameter = new SqlParameter();
    genderParameter.ParameterName = "@AnimalGender";
    genderParameter.Value = this.GetGender();

    SqlParameter breedParameter = new SqlParameter();
    breedParameter.ParameterName = "@AnimalBreed";
    breedParameter.Value = this.GetBreed();

    SqlParameter typeIdParameter = new SqlParameter();
    typeIdParameter.ParameterName = "@AnimalTypeId";
    typeIdParameter.Value = this.GetTypeId();

    SqlParameter dateParameter = new SqlParameter();
    dateParameter.ParameterName = "@AnimalDateOfAdmittance";
    dateParameter.Value = this.GetDate();

    cmd.Parameters.Add(nameParameter);
    cmd.Parameters.Add(genderParameter);
    cmd.Parameters.Add(breedParameter);
    cmd.Parameters.Add(typeIdParameter);
    cmd.Parameters.Add(dateParameter);

    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._id = rdr.GetInt32(0);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
  }

  public static Animal Find(int id)
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM animal WHERE id = @AnimalId;", conn);
    SqlParameter animalIdParameter = new SqlParameter();
    animalIdParameter.ParameterName = "@AnimalId";
    animalIdParameter.Value = id.ToString();
    cmd.Parameters.Add(animalIdParameter);
    rdr = cmd.ExecuteReader();

    int foundAnimalId = 0;
    string foundAnimalName = null;
    string foundAnimalGender = null;
    string foundAnimalBreed = null;
    int foundAnimalTypeId = 0;
    DateTime foundAnimalDateOfAdmittance = new DateTime(1,1,1);

    while(rdr.Read())
    {
      foundAnimalId = rdr.GetInt32(0);
      foundAnimalName = rdr.GetString(1);
      foundAnimalGender = rdr.GetString(2);
      foundAnimalBreed = rdr.GetString(3);
      foundAnimalTypeId = rdr.GetInt32(4);
      foundAnimalDateOfAdmittance = rdr.GetDateTime(5);
    }
    Animal foundAnimal = new Animal(foundAnimalName, foundAnimalGender, foundAnimalDateOfAdmittance, foundAnimalBreed, foundAnimalTypeId, foundAnimalId);

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return foundAnimal;
  }

  public static void DeleteAll()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlCommand cmd = new SqlCommand("DELETE FROM animal;", conn);
    cmd.ExecuteNonQuery();
  }

}
}
