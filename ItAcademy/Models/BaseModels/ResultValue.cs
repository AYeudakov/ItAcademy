using System.Net;

namespace ItAcademy.Models.BaseModels;

[Serializable]
public record ResultValue<T>(HttpStatusCode Status, string? Message, T? Value) 
    : Result(Status, Message) where T: class;