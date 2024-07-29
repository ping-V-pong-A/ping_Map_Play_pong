namespace ping_Map_Play_pong.Service;

public class ServiceMethods : IServiceMethods
{
    public void UpdateProperties(object request, object target)
    {
        var requestType = request.GetType();
        var targetType = target.GetType();

        foreach (var requestProperty in requestType.GetProperties())
        {
            var targetProperty = targetType.GetProperty(requestProperty.Name);
            
            if (targetProperty != null && targetProperty.CanWrite)
            {
                var requestValue = requestProperty.GetValue(request);
                if (requestValue != null)
                {
                    targetProperty.SetValue(target, requestValue);
                }
            }
        }
    }
}