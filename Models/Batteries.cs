using System;



public class Batteries
{
    public long Id { get; set; }
    public long Building_Id { get; set; }
    public long Employee_Id  { get; set; }
    public string building_type {get; set;}
    public string Status {get; set;}
    public DateTime Date_Service_Since {get; set;}
    public DateTime Date_Last_Inspection {get; set;}
    public string Operations_Certificate {get; set;}
    public string Information {get; set;}
    public string Notes {get; set;}


}