public class BusinessPair
{
    public BusinessPair(Business business, UIBusiness uIBusiness)
    {
        Business = business;
        UIBusiness = uIBusiness;
    }

    public Business Business { get; private set; }
    public UIBusiness UIBusiness { get; private set; }
}
