
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Rest;


var settings = new FhirClientSettings
{
    PreferredFormat = ResourceFormat.Json,
    VerifyFhirVersion = true,
    ReturnPreference = ReturnPreference.Minimal
};

var client = new FhirClient("http://server.fire.ly", settings);

//Bundle results = client.Search<Patient>(new string[] { "family:exact=Smith" });
var q = new SearchParams()
           .Where("name:exact=Smith")
           .OrderBy("birthdate", SortOrder.Descending)
           .SummaryOnly().Include("Patient:organization")
           .LimitTo(5);

Bundle? results = await client.SearchAsync<Patient>(q);


// Process the results;
if (results != null)
{
    foreach (var entry in results.Entry)
    {
        var patient = entry.Resource as Patient;
        if (patient != null)
        {
            Console.WriteLine($"Patient Name: {patient.Name[0].ToString()}, Birthdate: {patient.BirthDate}");
        }
    }
}

