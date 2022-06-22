namespace UsersCache.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string Suite { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public GeoLocation Geo { get; set; }
}

public class GeoLocation
{
    public string Lat { get; set; }
    public string Lng { get; set; }
}