using AutoFixture;

namespace  UnitTests.AutoFixure;
public class UriCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Uri>(x => x.FromFactory(() => new Uri("http://www.unittest/rest/4")));
    }
}