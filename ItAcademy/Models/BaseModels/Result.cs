using System.Net;

namespace ItAcademy.Models.BaseModels;

[Serializable]
public record Result(HttpStatusCode Status, string? Message);