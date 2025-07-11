namespace ApiPetShop.Domain
{
    public class CustumerModel : ModelBase
    {
        public string Email { get; set; } = string.Empty;

        public CustumerModel(string email)
        {
            Email = email;
        }
        
        public CustumerModel() { }

        public void RemoveCustumerFromList()
        {
            Delete();
        }
    }
}