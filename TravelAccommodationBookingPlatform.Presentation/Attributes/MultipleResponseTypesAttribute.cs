namespace TravelAccommodationBookingPlatform.Presentation.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class MultipleResponseTypesAttribute : Attribute
{
    public Type[] ResponseTypes { get; }
    public int StatusCode { get; }

    public MultipleResponseTypesAttribute(int statusCode, params Type[] responseTypes)
    {
        StatusCode = statusCode;
        ResponseTypes = responseTypes;
    }
}