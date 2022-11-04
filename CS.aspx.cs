using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    [System.Web.Services.WebMethod]
    public static List<Country> GetCountries()
    {
        List<Country> countries = new List<Country>();
        Country country = new Country();

        country.Name = "India";
        country.Id = "1";
        countries.Add(country);

        country = new Country();
        country.Name = "USA";
        country.Id = "2";
        countries.Add(country);

        country = new Country();
        country.Name = "Canada";
        country.Id = "3";
        countries.Add(country);

        return countries;
    }
}

public class Country
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private string _id;
    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }
}