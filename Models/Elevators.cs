using System;



public class Elevators
{
    public long Id { get; set; }
    public long Column_id { get; set; }
    public long Serial_number { get; set; }
    public string Model {get; set;}
    public string Building_type {get; set;}
    public string Status {get; set;}
    public DateTime Date_Service_Since {get; set;}
    public DateTime Date_Last_Inspection {get; set;}
    public string Inspection_Certificate {get; set;}
    public string Information {get; set;}
    public string Notes {get; set;}
    public DateTime Created_at {get; set;}
    public DateTime Updated_at {get; set;}

}