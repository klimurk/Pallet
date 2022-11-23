namespace Pallet.Services.Base;

public interface IRest
{
    virtual object Get() => throw new NotImplementedException();
    virtual void Post(object item) => throw new NotImplementedException();
    virtual void Put() => throw new NotImplementedException();
    virtual void Remove(object item) => throw new NotImplementedException();
}
