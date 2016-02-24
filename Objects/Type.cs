using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter
{
    public class Type
    {
      private int _id;
      private string _name;

      public Type(string Name, int Id = 0)
      {
        _id = Id;
        _name = Name;
      }

      public override bool Equals(System.Object otherType)
      {
        if(!(otherType is Type))
        {
          return false;
        }
        else
        {
          Type newType = (Type) otherType;
          bool idEquality = this.GetId() == newType.GetId();
          bool nameEquality = this.GetName() == newType.GetName();
          return (idEquality && nameEquality);
        }
      }

      public int GetId()
      {
        return _id;
      }

      public string GetName()
      {
        return _name;
      }


      public static List<Type> GetAll()
      {
        List<Type> allTypes = new List<Type>{};

        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM type;", conn);
        rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          int TypeId = rdr.GetInt32(0);
          string TypeName = rdr.GetString(1);
          Type newType = new Type(TypeName, TypeId);
          allTypes.Add(newType);
        }
        if (rdr != null) {
          rdr.Close();
        }
        if (conn != null) {
          conn.Close();
        }
        return allTypes;
      }

      public void Save()
      {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr;
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO type (name) OUTPUT INSERTED.id VALUES (@TypeName);", conn);
        SqlParameter nameParameter = new SqlParameter();
        nameParameter.ParameterName = "@TypeName";
        nameParameter.Value = this.GetName();
        cmd.Parameters.Add(nameParameter);
        rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._id = rdr.GetInt32(0);
        }
        if (rdr != null) {
          rdr.Close();
        }
        if (conn != null) {
          conn.Close();
        }
      }

      public static void DeleteAll()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();
        SqlCommand cmd = new SqlCommand("DELETE FROM type;", conn);
        cmd.ExecuteNonQuery();
      }

      public static Type Find(int id)
      {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM type WHERE id = @TypeId;", conn);
        SqlParameter typeIdParameter = new SqlParameter();
        typeIdParameter.ParameterName = "@TypeId";
        typeIdParameter.Value = id;
        cmd.Parameters.Add(typeIdParameter);
        rdr = cmd.ExecuteReader();

        int foundTypeId = 0;
        string foundTypeDescription = null;

        while(rdr.Read())
        {
          foundTypeId = rdr.GetInt32(0);
          foundTypeDescription = rdr.GetString(1);
        }
        Type foundType = new Type(foundTypeDescription, foundTypeId);

        if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundType;

      }

      public List<Animal> GetAnimals()
      {
        SqlConnection conn = DB.Connection();
        SqlDataReader rdr = null;
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM animal WHERE type_id = @TypeId;", conn);
        SqlParameter typeIdParameter = new SqlParameter();
        typeIdParameter.ParameterName = "@TypeId";
        typeIdParameter.Value = this.GetId();
        cmd.Parameters.Add(typeIdParameter);
        rdr = cmd.ExecuteReader();

        List<Animal> animals = new List<Animal> {};
        while(rdr.Read())
        {
          int AnimalId = rdr.GetInt32(0);
          string AnimalName = rdr.GetString(1);
          string AnimalGender = rdr.GetString(2);
          string AnimalBreed = rdr.GetString(3);
          int AnimalTypeId = rdr.GetInt32(4);
          DateTime AnimalDateOfAdmittance = rdr.GetDateTime(5);
          Animal newAnimal = new Animal(AnimalName, AnimalGender, AnimalDateOfAdmittance, AnimalBreed, AnimalTypeId, AnimalId);
          animals.Add(newAnimal);
        }
        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return animals;
        }
    }
}
