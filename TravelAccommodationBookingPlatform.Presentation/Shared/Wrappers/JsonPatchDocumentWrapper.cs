using Microsoft.AspNetCore.JsonPatch;
using TravelAccommodationBookingPlatform.Application.Interfaces.IO;

namespace TravelAccommodationBookingPlatform.Presentation.Shared.Wrappers;

public class JsonPatchDocumentWrapper<T> : IPatchDocument<T>
    where T : class
{
    private readonly JsonPatchDocument<T> _patchDocument;

    public JsonPatchDocumentWrapper(JsonPatchDocument<T> patchDocument)
    {
        _patchDocument = patchDocument;
    }

    public void ApplyTo(T model)
    {
        _patchDocument.ApplyTo(model);
    }
}