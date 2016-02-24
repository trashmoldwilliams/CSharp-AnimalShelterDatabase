using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

  namespace AnimalShelter {
      public class HomeModule : NancyModule {
        public HomeModule() {

        Get["/"] = _ => {
          List<Type> AllTypes = Type.GetAll();
          return View["index.cshtml", AllTypes];
        };

        Get["/animals"] = _ => {
          List<Animal> AllAnimals = Animal.GetAll();
          return View["animals.cshtml", AllAnimals];
        };

        Get["/types"] = _ => {
        List<Type> AllTypes = Type.GetAll();
        return View["types.cshtml", AllTypes];
        };

        Get["types/new"] = _ => {
          return View["types_form.cshtml"];
        };

        Post["types/new"] = _ => {
          Type newType = new Type(Request.Form["type-name"]);
          newType.Save();
          return View["success.cshtml"];
        };

        Get["/animals/new"] = _ => {
          List<Type> AllTypes = Type.GetAll();
          return View["animals_form.cshtml", AllTypes];
        };

        Post["/animals/new"] = _ => {
          Animal newAnimal = new Animal(Request.Form["name"], Request.Form["gender"], Request.Form["date_of_admittance"], Request.Form["breed"], Request.Form["type-id"]);
          newAnimal.Save();
          return View["success.cshtml"];
        };

        Post["/animals/delete"] = _ => {
          Animal.DeleteAll();
          return View["success.cshtml"];
        };

        Get["/types/{id}"] = parameters => {
          Dictionary<string, object> model = new Dictionary<string, object>();
          var SelectedType = Type.Find(parameters.id);
          var TypeAnimals = SelectedType.GetAnimals();
          model.Add("type", SelectedType);
          model.Add("animals", TypeAnimals);
          return View["type.cshtml", model];
        };
      }
   }
}
